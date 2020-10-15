﻿using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static Chen.GradiusMod.SpawnOptionsForClients;

namespace Chen.GradiusMod
{
    public class OptionBehavior : MonoBehaviour
    {
        public GameObject owner;
        public int numbering = 0;
        public GameObject flamethrower;
        public HealBeamController healBeamController;
        public GameObject laserChargeEffect;
        public GameObject laserFireEffect;
        public LineRenderer laserLineEffect;
        public ChildLocator laserChildLocator;
        public Transform laserFireEffectEnd;
        public GameObject fistChargeEffect;
        public GameObject rockChargeEffect;
        public GameObject target;
        public OptionTracker ownerOt;

        private Transform t;
        private Transform ownerT;
        private InputBankTest ownerIbt;
        private bool init = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Awake()
        {
            t = gameObject.transform;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Update()
        {
            if (!PauseScreenController.paused && !init)
            {
                if (owner && ownerOt)
                {
                    if (Helper.IsRotateUser(owner.name))
                    {
                        float multiplier = Helper.RotateMultiplier(owner.name);
                        Vector3 newPosition = ownerT.position + DecidePosition(ownerOt.currentOptionAngle) * ownerOt.distanceAxis * multiplier;
                        t.position = Vector3.Lerp(t.position, newPosition, ownerOt.optionLookRate);
                    }
                    else t.position = ownerOt.flightPath[numbering * ownerOt.distanceInterval - 1];
                    if (GradiusOption.instance.includeModelInsideOrb)
                    {
                        Vector3 direction;
                        if (target) direction = (target.transform.position - t.position).normalized;
                        else if (ownerIbt) direction = ownerIbt.aimDirection;
                        else direction = (ownerT.position - t.position).normalized;
                        t.rotation = Quaternion.Lerp(t.rotation, Util.QuaternionSafeLookRotation(direction), ownerOt.optionLookRate);
                    }
                }
                else
                {
                    GradiusModPlugin._logger.LogWarning($"OptionBehavior.Update: Lost owner or ownerOt. Destroying this Option. numbering = {numbering}");
                    Destroy(gameObject);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void FixedUpdate()
        {
            if (!PauseScreenController.paused && init && owner)
            {
                init = false;
                ownerT = owner.transform;
                ownerIbt = owner.GetComponent<InputBankTest>();
                ownerOt = owner.GetComponent<OptionTracker>();
            }
        }

        public Vector3 DecidePosition(float baseAngle)
        {
            Vector3 relativePosition = Quaternion.AngleAxis(baseAngle, ownerT.up) * -ownerT.forward;
            float angleDifference = 360f / ownerOt.existingOptions.Count;
            relativePosition = Quaternion.AngleAxis(angleDifference * numbering, ownerT.up) * relativePosition;
            return relativePosition.normalized;
        }
    }

    public class OptionTracker : MonoBehaviour
    {
        public List<Vector3> flightPath { get; private set; } = new List<Vector3>();
        public List<GameObject> existingOptions { get; private set; } = new List<GameObject>();
        public int distanceInterval { get; private set; } = 20;
        public float distanceAxis { get; private set; } = 1.2f;
        public float rotateOptionAngleSpeed { get; private set; } = 2f;
        public float currentOptionAngle { get; private set; } = 0f;
        public float optionLookRate { get; private set; } = .15f;
        public CharacterMaster masterCharacterMaster { get; private set; }
        public OptionMasterTracker masterOptionTracker { get; private set; }
        public CharacterMaster characterMaster { get; private set; }
        public CharacterBody characterBody { get; private set; }

        public List<Tuple<SyncFlamethrowerEffectForClients.MessageType, NetworkInstanceId, short, float, Vector3>> flameNetIds { get; private set; } =
            new List<Tuple<SyncFlamethrowerEffectForClients.MessageType, NetworkInstanceId, short, float, Vector3>>();

        public List<Tuple<GameObjectType, NetworkInstanceId, short, NetworkInstanceId>> targetIds { get; private set; } =
            new List<Tuple<GameObjectType, NetworkInstanceId, short, NetworkInstanceId>>();

        public List<Tuple<SyncAurelioniteEffectsForClients.MessageType, NetworkInstanceId, short, float, Vector3, float>> aurelioniteNetIds { get; private set; } =
            new List<Tuple<SyncAurelioniteEffectsForClients.MessageType, NetworkInstanceId, short, float, Vector3, float>>();

        private Vector3 previousPosition = new Vector3();
        private bool init = true;
        private int previousOptionItemCount = 0;
        private Transform t;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Awake()
        {
            t = gameObject.transform;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Update()
        {
            if (PauseScreenController.paused) return;
            if (!init && masterOptionTracker && characterMaster)
            {
                if (Helper.IsRotateUser(characterMaster.name) && masterOptionTracker.optionItemCount > 0)
                {
                    if (masterOptionTracker.optionItemCount % 2 == 0) currentOptionAngle += rotateOptionAngleSpeed;
                    else currentOptionAngle -= rotateOptionAngleSpeed;
                    if (currentOptionAngle >= 360f) currentOptionAngle = 360f - currentOptionAngle;
                    else if (currentOptionAngle <= 0f) currentOptionAngle += 360f;
                }
                else if (previousPosition != t.position)
                {
                    flightPath.Insert(0, t.position);
                    if (flightPath.Count > masterOptionTracker.optionItemCount * distanceInterval) flightPath.RemoveAt(flightPath.Count - 1);
                }
                previousPosition = t.position;
            }
            SyncFlamethrowerEffects();
            SyncAurelioniteEffects();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void FixedUpdate()
        {
            if (PauseScreenController.paused) return;
            if (!masterOptionTracker)
            {
                characterBody = gameObject.GetComponent<CharacterBody>();
                if (!characterBody)
                {
                    GradiusModPlugin._logger.LogWarning("OptionTracker Initialization: characterBody does not exist!");
                    return;
                }
                characterMaster = characterBody.master;
                if (!characterMaster)
                {
                    GradiusModPlugin._logger.LogWarning("OptionTracker Initialization: characterMaster does not exist!");
                    return;
                }
                masterCharacterMaster = characterMaster.minionOwnership.ownerMaster;
                if (!masterCharacterMaster)
                {
                    GradiusModPlugin._logger.LogWarning("OptionTracker Initialization: masterCharacterMaster does not exist!");
                    return;
                }
                masterOptionTracker = masterCharacterMaster.gameObject.GetComponent<OptionMasterTracker>();
                if (!masterOptionTracker)
                {
                    GradiusModPlugin._logger.LogWarning("OptionTracker Initialization: masterOptionTracker is null.");
                    return;
                }
            }
            if (init && masterOptionTracker.optionItemCount > 0)
            {
                init = false;
                previousPosition = t.position;
                ManageFlightPath(masterOptionTracker.optionItemCount);
            }
            else if (!init && masterOptionTracker.optionItemCount > 0)
            {
                int diff = masterOptionTracker.optionItemCount - previousOptionItemCount;
                if (diff > 0 || diff < 0)
                {
                    previousOptionItemCount = masterOptionTracker.optionItemCount;
                    ManageFlightPath(diff);
                }
            }
            else if (!init && masterOptionTracker.optionItemCount <= 0)
            {
                init = true;
                flightPath.Clear();
                previousOptionItemCount = 0;
            }
            SyncTargets();
        }

        private void SyncFlamethrowerEffects()
        {
            if (NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady() && flameNetIds.Count > 0)
            {
                Tuple<SyncFlamethrowerEffectForClients.MessageType, NetworkInstanceId, short, float, Vector3>[] listCopy =
                    new Tuple<SyncFlamethrowerEffectForClients.MessageType, NetworkInstanceId, short, float, Vector3>[flameNetIds.Count];
                flameNetIds.CopyTo(listCopy);
                flameNetIds.Clear();
                for (int i = 0; i < listCopy.Length; i++)
                {
                    SyncFlamethrowerEffectForClients.MessageType messageType = listCopy[i].Item1;
                    NetworkInstanceId netId = listCopy[i].Item2;
                    short numbering = listCopy[i].Item3;
                    float duration = listCopy[i].Item4;
                    Vector3 direction = listCopy[i].Item5;
                    new SyncFlamethrowerEffectForClients(messageType, netId, numbering, duration, direction).Send(NetworkDestination.Clients);
                }
            }
        }

        private void SyncAurelioniteEffects()
        {
            if (NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady() && aurelioniteNetIds.Count > 0)
            {
                Tuple<SyncAurelioniteEffectsForClients.MessageType, NetworkInstanceId, short, float, Vector3, float>[] listCopy =
                    new Tuple<SyncAurelioniteEffectsForClients.MessageType, NetworkInstanceId, short, float, Vector3, float>[aurelioniteNetIds.Count];
                aurelioniteNetIds.CopyTo(listCopy);
                aurelioniteNetIds.Clear();
                for (int i = 0; i < listCopy.Length; i++)
                {
                    SyncAurelioniteEffectsForClients.MessageType messageType = listCopy[i].Item1;
                    NetworkInstanceId netId = listCopy[i].Item2;
                    short numbering = listCopy[i].Item3;
                    float duration = listCopy[i].Item4;
                    Vector3 point = listCopy[i].Item5;
                    float indicatorSize = listCopy[i].Item6;
                    new SyncAurelioniteEffectsForClients(messageType, netId, numbering, duration, point, indicatorSize).Send(NetworkDestination.Clients);
                }
            }
        }

        private void SyncTargets()
        {
            if (GradiusOption.instance.includeModelInsideOrb && NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady() && targetIds.Count > 0)
            {
                Tuple<GameObjectType, NetworkInstanceId, short, NetworkInstanceId>[] listCopy = new Tuple<GameObjectType, NetworkInstanceId, short, NetworkInstanceId>[targetIds.Count];
                targetIds.CopyTo(listCopy);
                targetIds.Clear();
                for (int i = 0; i < listCopy.Length; i++)
                {
                    GameObjectType gameObjectType = listCopy[i].Item1;
                    NetworkInstanceId netId = listCopy[i].Item2;
                    short numbering = listCopy[i].Item3;
                    NetworkInstanceId targetId = listCopy[i].Item4;
                    new SyncOptionTargetForClients(gameObjectType, netId, numbering, targetId).Send(NetworkDestination.Clients);
                }
            }
        }

        private void ManageFlightPath(int difference)
        {
            if (Helper.IsRotateUser(characterMaster.name)) return;
            int flightPathCap = masterOptionTracker.optionItemCount * distanceInterval;
            if (difference > 0) while (flightPath.Count < flightPathCap) flightPath.Add(t.position);
            else if (difference < 0) while (flightPath.Count >= flightPathCap) flightPath.RemoveAt(flightPath.Count - 1);
        }

        public static OptionTracker GetOrCreateComponent(GameObject me)
        {
            OptionTracker tracker = me.GetComponent<OptionTracker>() ?? me.AddComponent<OptionTracker>();
            return tracker;
        }

        public static OptionTracker GetOrCreateComponent(CharacterBody me)
        {
            return GetOrCreateComponent(me.gameObject);
        }
    }

    public class OptionMasterTracker : MonoBehaviour
    {
        public int optionItemCount = 0;
        public List<Tuple<GameObjectType, NetworkInstanceId, short>> netIds { get; private set; } = new List<Tuple<GameObjectType, NetworkInstanceId, short>>();
        public Tuple<NetworkInstanceId, NetworkInstanceId> aurelioniteOwner { get; set; } = null;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void FixedUpdate()
        {
            if (!PauseScreenController.paused && NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady())
            {
                if (netIds.Count > 0)
                {
                    Tuple<GameObjectType, NetworkInstanceId, short>[] listCopy = new Tuple<GameObjectType, NetworkInstanceId, short>[netIds.Count];
                    netIds.CopyTo(listCopy);
                    netIds.Clear();
                    for (int i = 0; i < listCopy.Length; i++)
                    {
                        GameObjectType bodyOrMaster = listCopy[i].Item1;
                        NetworkInstanceId netId = listCopy[i].Item2;
                        short numbering = listCopy[i].Item3;
                        StartCoroutine(SpawnOptionForClient(bodyOrMaster, netId, numbering));
                    }
                }
                if (aurelioniteOwner != null)
                {
                    NetworkInstanceId masterId = aurelioniteOwner.Item1;
                    NetworkInstanceId goldId = aurelioniteOwner.Item2;
                    StartCoroutine(SyncAurelioniteForClient(masterId, goldId));
                }
            }
        }

        private IEnumerator SpawnOptionForClient(GameObjectType bodyOrMaster, NetworkInstanceId netId, short numbering)
        {
            yield return new WaitForSeconds(GradiusOption.instance.spawnSyncSeconds);
            new SpawnOptionsForClients(bodyOrMaster, netId, numbering).Send(NetworkDestination.Clients);
        }

        private IEnumerator SyncAurelioniteForClient(NetworkInstanceId masterId, NetworkInstanceId goldId)
        {
            aurelioniteOwner = null;
            yield return new WaitForSeconds(GradiusOption.instance.spawnSyncSeconds);
            new SyncAurelioniteOwner(masterId, goldId).Send(NetworkDestination.Clients);
        }

        public static OptionMasterTracker GetOrCreateComponent(CharacterMaster me)
        {
            return GetOrCreateComponent(me.gameObject);
        }

        public static OptionMasterTracker GetOrCreateComponent(GameObject me)
        {
            OptionMasterTracker tracker = me.GetComponent<OptionMasterTracker>() ?? me.AddComponent<OptionMasterTracker>();
            return tracker;
        }

        public static void SpawnOption(GameObject owner, int itemCount)
        {
            OptionTracker ownerOptionTracker = OptionTracker.GetOrCreateComponent(owner);
            GameObject option = Instantiate(GradiusOption.gradiusOptionPrefab, owner.transform.position, owner.transform.rotation);
            OptionBehavior behavior = option.GetComponent<OptionBehavior>();
            behavior.owner = owner;
            behavior.numbering = itemCount;
            ownerOptionTracker.existingOptions.Add(option);
        }

        public static void DestroyOption(OptionTracker optionTracker, int optionNumber)
        {
            int index = optionNumber - 1;
            GameObject option = optionTracker.existingOptions[index];
            optionTracker.existingOptions.RemoveAt(index);
            Destroy(option);
        }
    }

    public class Flicker : MonoBehaviour
    {
        // Child Objects in Order:
        // 0. sphere1:     Light
        // 1. sphere2:     Light
        // 2. sphere3:     Light
        // 3. sphere4:     MeshRenderer, MeshFilter (only in OptionOrb)
        // 4. sphere5:     MeshRenderer, MeshFilter (only in OptionOrbWithModel)
        // 5. OptionModel: The option model (only in OptionOrbWithModel)

        private readonly float baseValue = 1f;
        private readonly float amplitude = .25f;
        private readonly float phase = 0f;
        private readonly float frequency = 1f;

        private readonly Light[] lightObjects = new Light[3];
        private readonly float[] originalRange = new float[3];
        private readonly float[] ampMultiplier = new float[4] { 1.2f, 1f, .8f, .4f };
        private Vector3 originalLocalScale;
        private GameObject meshObject;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                Light childLight = child.GetComponent<Light>();
                switch (child.name)
                {
                    case "sphere1":
                        originalRange[0] = childLight.range;
                        lightObjects[0] = childLight;
                        break;

                    case "sphere2":
                        originalRange[1] = childLight.range;
                        lightObjects[1] = childLight;
                        break;

                    case "sphere3":
                        originalRange[2] = childLight.range;
                        lightObjects[2] = childLight;
                        break;

                    case "sphere4":
                        originalLocalScale = child.transform.localScale;
                        meshObject = child;
                        break;

                    case "sphere5":
                        child.transform.localScale *= 1.5f;
                        break;

                    case "option":
                        child.transform.localScale = new Vector3(2f, 2f, 2f);
                        break;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Used by UnityEngine")]
        private void Update()
        {
            if (PauseScreenController.paused) return;
            for (int i = 0; i < lightObjects.Length; i++)
            {
                lightObjects[i].range = originalRange[i] * Wave(ampMultiplier[i]);
            }
            if (meshObject && originalLocalScale != null) meshObject.transform.localScale = originalLocalScale * Wave(ampMultiplier[3]);
        }

        private float Wave(float ampMultiplier)
        {
            float x = (Time.time + phase) * frequency;
            x -= Mathf.Floor(x);
            float y = Mathf.Sin(x * 2 * Mathf.PI);

            return (y * amplitude * ampMultiplier) + baseValue;
        }
    }

    public static class Helper
    {
        public static bool IsRotateUser(string name)
        {
            return name.Contains("Turret1") || name.Contains("TitanGold");
        }

        public static float RotateMultiplier(string name)
        {
            float multiplier = 1f;
            if (name.Contains("TitanGold")) multiplier *= 12f;
            return multiplier;
        }

        public static void Log(object data) => _.LogMessage(data);

        public static BepInEx.Logging.ManualLogSource _ => GradiusModPlugin._logger;
    }
}