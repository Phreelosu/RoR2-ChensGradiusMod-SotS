﻿using Chen.GradiusMod.Items.GradiusOption.Components;
using Chen.Helpers.MathHelpers;
using Chen.Helpers.UnityHelpers;
using EntityStates;
using EntityStates.BeetleGuardMonster;
using EntityStates.Drone.DroneWeapon;
using EntityStates.EngiTurret.EngiTurretWeapon;
using EntityStates.Squid.SquidWeapon;
using EntityStates.TitanMonster;
using RoR2;
using RoR2.Orbs;
using RoR2.Projectile;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static Chen.GradiusMod.GradiusModPlugin;
using MageWeapon = EntityStates.Mage.Weapon;
using Object = UnityEngine.Object;

namespace Chen.GradiusMod.Items.GradiusOption
{
    public partial class GradiusOption
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

        public override void Install()
        {
            base.Install();
            On.RoR2.CharacterBody.OnInventoryChanged += CharacterBody_OnInventoryChanged;
            CharacterBody.onBodyStartGlobal += CharacterBody_onBodyStartGlobal;
            On.EntityStates.Drone.DroneWeapon.FireGatling.OnEnter += FireGatling_OnEnter;
            On.EntityStates.Drone.DroneWeapon.FireTurret.OnEnter += FireTurret_OnEnter;
            On.EntityStates.Drone.DroneWeapon.FireMegaTurret.FireBullet += FireMegaTurret_FireBullet;
            On.EntityStates.Drone.DroneWeapon.FireMissileBarrage.FireMissile += FireMissileBarrage_FireMissile;
            On.EntityStates.Drone.DroneWeapon.FireTwinRocket.FireProjectile += FireTwinRocket_FireProjectile;
            On.EntityStates.Mage.Weapon.Flamethrower.FireGauntlet += Flamethrower_FireGauntlet;
            On.EntityStates.Mage.Weapon.Flamethrower.OnExit += Flamethrower_OnExit;
            On.EntityStates.Mage.Weapon.Flamethrower.FixedUpdate += Flamethrower_FixedUpdate;
            On.EntityStates.Drone.DroneWeapon.HealBeam.OnEnter += HealBeam_OnEnter;
            On.EntityStates.Drone.DroneWeapon.HealBeam.OnExit += HealBeam_OnExit;
            On.EntityStates.Drone.DroneWeapon.StartHealBeam.OnEnter += StartHealBeam_OnEnter;
            On.EntityStates.TitanMonster.ChargeMegaLaser.OnEnter += ChargeMegaLaser_OnEnter;
            On.EntityStates.TitanMonster.ChargeMegaLaser.OnExit += ChargeMegaLaser_OnExit;
            On.EntityStates.TitanMonster.FireMegaLaser.OnEnter += FireMegaLaser_OnEnter;
            On.EntityStates.TitanMonster.FireMegaLaser.OnExit += FireMegaLaser_OnExit;
            On.EntityStates.TitanMonster.FireGoldMegaLaser.FixedUpdate += FireGoldMegaLaser_FixedUpdate;
            On.EntityStates.TitanMonster.FireGoldFist.PlacePredictedAttack += FireGoldFist_PlacePredictedAttack;
            On.EntityStates.TitanMonster.FireFist.OnEnter += FireFist_OnEnter;
            On.EntityStates.TitanMonster.FireFist.OnExit += FireFist_OnExit;
            On.RoR2.TitanRockController.Start += TitanRockController_Start;
            On.EntityStates.Squid.SquidWeapon.FireSpine.FireOrbArrow += FireSpine_FireOrbArrow;
            On.EntityStates.BeetleGuardMonster.FireSunder.OnEnter += FireSunder_OnEnter;
            On.EntityStates.BeetleGuardMonster.FireSunder.OnExit += FireSunder_OnExit;
            On.EntityStates.BeetleGuardMonster.FireSunder.FixedUpdate += FireSunder_FixedUpdate;
            On.EntityStates.BeetleGuardMonster.GroundSlam.OnEnter += GroundSlam_OnEnter;
            On.RoR2.EquipmentSlot.PerformEquipmentAction += EquipmentSlot_PerformEquipmentAction;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.FixedUpdate += FireBeam_FixedUpdate;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.OnEnter += FireBeam_OnEnter;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.OnExit += FireBeam_OnExit;
        }

        public override void Uninstall()
        {
            base.Uninstall();
            On.RoR2.CharacterBody.OnInventoryChanged -= CharacterBody_OnInventoryChanged;
            CharacterBody.onBodyStartGlobal -= CharacterBody_onBodyStartGlobal;
            On.EntityStates.Drone.DroneWeapon.FireGatling.OnEnter -= FireGatling_OnEnter;
            On.EntityStates.Drone.DroneWeapon.FireTurret.OnEnter -= FireTurret_OnEnter;
            On.EntityStates.Drone.DroneWeapon.FireMegaTurret.FireBullet -= FireMegaTurret_FireBullet;
            On.EntityStates.Drone.DroneWeapon.FireMissileBarrage.FireMissile -= FireMissileBarrage_FireMissile;
            On.EntityStates.Drone.DroneWeapon.FireTwinRocket.FireProjectile -= FireTwinRocket_FireProjectile;
            On.EntityStates.Mage.Weapon.Flamethrower.FireGauntlet -= Flamethrower_FireGauntlet;
            On.EntityStates.Mage.Weapon.Flamethrower.OnExit -= Flamethrower_OnExit;
            On.EntityStates.Mage.Weapon.Flamethrower.FixedUpdate -= Flamethrower_FixedUpdate;
            On.EntityStates.Drone.DroneWeapon.HealBeam.OnEnter -= HealBeam_OnEnter;
            On.EntityStates.Drone.DroneWeapon.HealBeam.OnExit -= HealBeam_OnExit;
            On.EntityStates.Drone.DroneWeapon.StartHealBeam.OnEnter -= StartHealBeam_OnEnter;
            On.EntityStates.TitanMonster.ChargeMegaLaser.OnEnter -= ChargeMegaLaser_OnEnter;
            On.EntityStates.TitanMonster.ChargeMegaLaser.OnExit -= ChargeMegaLaser_OnExit;
            On.EntityStates.TitanMonster.FireMegaLaser.OnEnter -= FireMegaLaser_OnEnter;
            On.EntityStates.TitanMonster.FireMegaLaser.OnExit -= FireMegaLaser_OnExit;
            On.EntityStates.TitanMonster.FireGoldMegaLaser.FixedUpdate -= FireGoldMegaLaser_FixedUpdate;
            On.EntityStates.TitanMonster.FireGoldFist.PlacePredictedAttack -= FireGoldFist_PlacePredictedAttack;
            On.EntityStates.TitanMonster.FireFist.OnEnter -= FireFist_OnEnter;
            On.EntityStates.TitanMonster.FireFist.OnExit -= FireFist_OnExit;
            On.RoR2.TitanRockController.Start -= TitanRockController_Start;
            On.EntityStates.Squid.SquidWeapon.FireSpine.FireOrbArrow -= FireSpine_FireOrbArrow;
            On.EntityStates.BeetleGuardMonster.FireSunder.OnEnter -= FireSunder_OnEnter;
            On.EntityStates.BeetleGuardMonster.FireSunder.OnExit -= FireSunder_OnExit;
            On.EntityStates.BeetleGuardMonster.FireSunder.FixedUpdate -= FireSunder_FixedUpdate;
            On.EntityStates.BeetleGuardMonster.GroundSlam.OnEnter -= GroundSlam_OnEnter;
            On.RoR2.EquipmentSlot.PerformEquipmentAction -= EquipmentSlot_PerformEquipmentAction;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.FixedUpdate -= FireBeam_FixedUpdate;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.OnEnter -= FireBeam_OnEnter;
            On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.OnExit -= FireBeam_OnExit;
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        private void CharacterBody_onBodyStartGlobal(CharacterBody obj)
        {
            // This hook runs on Client and on Server
            CharacterMaster master = obj.master;
            if (FilterMinions(master) && master.minionOwnership)
            {
                AssignAurelioniteOwner(master);
                CharacterMaster masterMaster = master.minionOwnership.ownerMaster;
                if (masterMaster && GetCount(masterMaster) > 0)
                {
                    OptionMasterTracker masterTracker = masterMaster.GetOrAddComponent<OptionMasterTracker>();
                    Log.Message($"OnBodyStartGlobal: Minion: {master.name}, Master: {masterMaster.name}, Options: {masterTracker.optionItemCount}");
                    OptionMasterTracker.SpawnOptions(obj.gameObject, 0, masterTracker.optionItemCount);
                }
            }
        }

        private void CharacterBody_OnInventoryChanged(On.RoR2.CharacterBody.orig_OnInventoryChanged orig, CharacterBody self)
        {
            // This hook runs on Client and on Server
            orig(self);
            CharacterMaster master = self.master;
            if (!master) return;
            MinionOwnership minionOwnership = master.minionOwnership;
            if (!minionOwnership || minionOwnership.ownerMaster || FilterMinions(master)) return;
            OptionMasterTracker masterTracker = master.GetOrAddComponent<OptionMasterTracker>();
            int newCount = GetCount(self);
            int oldCount = masterTracker.optionItemCount;
            int diff = newCount - oldCount;
            if (diff != 0)
            {
                masterTracker.optionItemCount = newCount;
                Log.Message($"OnInventoryChanged: Master: {master.name}, OldCount: {oldCount}, NewCount: {newCount}, Difference: {diff}");
                if (diff > 0) LoopAllMinions(master, minion => OptionMasterTracker.SpawnOptions(minion, oldCount, newCount));
                else LoopAllMinions(master, minion => OptionMasterTracker.DestroyOptions(minion, oldCount, newCount));
            }
        }

        private void FireBeam_FixedUpdate(On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.orig_FixedUpdate orig, FireBeam self)
        {
            float optionFireTimer = self.fireTimer + Time.fixedDeltaTime;
            orig(self);
            if (DroneNameCheck(self.characterBody, "RoboBallGreenBuddy") || DroneNameCheck(self.characterBody, "RoboBallRedBuddy"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, target, direction) =>
                {
                    Ray laserRay = new Ray(option.transform.position, direction);
                    float rate = self.fireFrequency * self.characterBody.attackSpeed;
                    float fraction = 1f / rate;
                    if (optionFireTimer > fraction)
                    {
                        if (self.effectPrefab) option.MuzzleEffect(self.effectPrefab, false);
                        if (self.isAuthority)
                        {
                            BulletAttack bulletAttack = new BulletAttack()
                            {
                                owner = self.gameObject,
                                weapon = option,
                                origin = laserRay.origin,
                                aimVector = laserRay.direction,
                                minSpread = self.minSpread,
                                maxSpread = self.maxSpread,
                                bulletCount = 1U,
                                damage = self.damageCoefficient * self.damageStat / self.fireFrequency * damageMultiplier,
                                procCoefficient = self.procCoefficient / self.fireFrequency,
                                force = self.force * damageMultiplier,
                                muzzleName = self.muzzleString,
                                hitEffectPrefab = self.hitEffectPrefab,
                                isCrit = self.characterBody.RollCrit(),
                                HitEffectNormal = false,
                                radius = 0f,
                                maxDistance = self.maxDistance
                            };
                            self.ModifyBullet(bulletAttack);
                            if (!empathyCoresOptionSyncEffect) bulletAttack.tracerEffectPrefab = FireGatling.tracerEffectPrefab;
                            bulletAttack.Fire();
                        }
                    }
                    if (empathyCoresOptionSyncEffect && behavior.U.SafeCheck("laserEffectInstance") && behavior.U.SafeCheck("laserEffectInstanceEndTransform"))
                    {
                        Vector3 position = ((GameObject)behavior.U["laserEffectInstance"]).transform.parent.position;
                        Vector3 point = laserRay.GetPoint(self.maxDistance);
                        if (Physics.Raycast(laserRay.origin, laserRay.direction, out RaycastHit raycastHit, self.maxDistance,
                                            LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
                        {
                            point = raycastHit.point;
                        }
                        ((Transform)behavior.U["laserEffectInstanceEndTransform"]).position = point;
                    }
                });
            }
        }

        private void FireBeam_OnExit(On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.orig_OnExit orig, FireBeam self)
        {
            orig(self);
            if (!empathyCoresOptionSyncEffect) return;
            if (DroneNameCheck(self.characterBody, "RoboBallGreenBuddy") || DroneNameCheck(self.characterBody, "RoboBallRedBuddy"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, target, direction) =>
                {
                    if (behavior.U.SafeCheck("laserEffectInstance")) EntityState.Destroy(behavior.U["laserEffectInstance"]);
                    if (behavior.U.SafeCheck("laserEffectInstanceEndTransform")) EntityState.Destroy(behavior.U["laserEffectInstanceEndTransform"]);
                });
            }
        }

        private void FireBeam_OnEnter(On.EntityStates.EngiTurret.EngiTurretWeapon.FireBeam.orig_OnEnter orig, FireBeam self)
        {
            orig(self);
            if (!empathyCoresOptionSyncEffect) return;
            if (DroneNameCheck(self.characterBody, "RoboBallGreenBuddy") || DroneNameCheck(self.characterBody, "RoboBallRedBuddy"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, target, direction) =>
                {
                    Util.PlaySound(self.attackSoundString, option);
                    if (self.laserPrefab)
                    {
                        if (behavior.U.SafeCheck("laserEffectInstance")) EntityState.Destroy(behavior.U["laserEffectInstance"]);
                        if (behavior.U.SafeCheck("laserEffectInstanceEndTransform")) EntityState.Destroy(behavior.U["laserEffectInstanceEndTransform"]);
                        Transform transform = option.transform;
                        behavior.U["laserEffectInstance"] = Object.Instantiate(self.laserPrefab, transform.position, transform.rotation);
                        ((GameObject)behavior.U["laserEffectInstance"]).transform.parent = transform;
                        behavior.U["laserEffectInstanceEndTransform"] = ((GameObject)behavior.U["laserEffectInstance"]).GetComponent<ChildLocator>().FindChild("LaserEnd");
                    }
                });
            }
        }

        private void HealBeam_OnEnter(On.EntityStates.Drone.DroneWeapon.HealBeam.orig_OnEnter orig, HealBeam self)
        {
            orig(self);
            if (!NetworkServer.active) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                float healRate = (HealBeam.healCoefficient * self.damageStat / self.duration) * damageMultiplier;
                Transform transform = option.transform;
                if (transform && self.target)
                {
                    if (behavior.U.SafeCheck("healBeamController"))
                    {
                        ((HealBeamController)behavior.U["healBeamController"]).BreakServer();
                    }
                    GameObject gameObject = Object.Instantiate(HealBeam.healBeamPrefab, transform);
                    HealBeamController hbc = gameObject.GetComponent<HealBeamController>();
                    behavior.U["healBeamController"] = hbc;
                    hbc.healRate = healRate;
                    hbc.target = self.target;
                    hbc.ownership.ownerObject = option;
                    NetworkServer.Spawn(gameObject);
                }
            });
        }

        private void HealBeam_OnExit(On.EntityStates.Drone.DroneWeapon.HealBeam.orig_OnExit orig, HealBeam self)
        {
            orig(self);
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("healBeamController"))
                {
                    ((HealBeamController)behavior.U["healBeamController"]).BreakServer();
                }
            });
        }

        private void StartHealBeam_OnEnter(On.EntityStates.Drone.DroneWeapon.StartHealBeam.orig_OnEnter orig, StartHealBeam self)
        {
            orig(self);
            if (!NetworkServer.active) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (HealBeamController.GetHealBeamCountForOwner(self.gameObject) < self.maxSimultaneousBeams && self.targetHurtBox)
                {
                    Transform transform = option.transform;
                    if (transform)
                    {
                        GameObject gameObject = Object.Instantiate(self.healBeamPrefab, transform);
                        HealBeamController hbc = gameObject.GetComponent<HealBeamController>();
                        hbc.healRate = self.healRateCoefficient * self.damageStat * self.attackSpeedStat * damageMultiplier;
                        hbc.target = self.targetHurtBox;
                        hbc.ownership.ownerObject = option;
                        gameObject.AddComponent<DestroyOnTimer>().duration = self.duration;
                        NetworkServer.Spawn(gameObject);
                    }
                }
            });
        }

        private void Flamethrower_OnExit(On.EntityStates.Mage.Weapon.Flamethrower.orig_OnExit orig, MageWeapon.Flamethrower self)
        {
            orig(self);
            if (flamethrowerOptionSyncEffect && DroneNameCheck(self.characterBody, "FlameDrone"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
                {
                    if (behavior.U.SafeCheck("flamethrower"))
                    {
                        EntityState.Destroy(behavior.U["flamethrower"]);
                    }
                });
            }
        }

        private void Flamethrower_FixedUpdate(On.EntityStates.Mage.Weapon.Flamethrower.orig_FixedUpdate orig, MageWeapon.Flamethrower self)
        {
            bool oldBegunFlamethrower = self.hasBegunFlamethrower;
            orig(self);
            if (flamethrowerOptionSyncEffect && DroneNameCheck(self.characterBody, "FlameDrone"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, target, direction) =>
                {
                    bool perMinionOldBegunFlamethrower = oldBegunFlamethrower;
                    if (self.stopwatch >= self.entryDuration && !perMinionOldBegunFlamethrower)
                    {
                        perMinionOldBegunFlamethrower = true;
                        if (behavior.U.SafeCheck("flamethrower"))
                        {
                            EntityState.Destroy(behavior.U["flamethrower"]);
                        }
                        behavior.U["flamethrower"] = Object.Instantiate(self.flamethrowerEffectPrefab, option.transform);
                        ((GameObject)behavior.U["flamethrower"]).GetComponent<ScaleParticleSystemDuration>().newDuration = self.flamethrowerDuration;
                    }
                    if (perMinionOldBegunFlamethrower && behavior.U["flamethrower"] && target)
                    {
                        ((GameObject)behavior.U["flamethrower"]).transform.forward = direction;
                    }
                });
            }
        }

        private void Flamethrower_FireGauntlet(On.EntityStates.Mage.Weapon.Flamethrower.orig_FireGauntlet orig, MageWeapon.Flamethrower self, string muzzleString)
        {
            orig(self, muzzleString);
            if (DroneNameCheck(self.characterBody, "FlameDrone"))
            {
                FireForAllOptions(self.characterBody, (option, behavior, _t, direction) =>
                {
                    if (self.isAuthority)
                    {
                        BulletAttack attack = new BulletAttack
                        {
                            owner = self.gameObject,
                            weapon = option,
                            origin = option.transform.position,
                            aimVector = direction,
                            minSpread = 0f,
                            damage = self.tickDamageCoefficient * self.damageStat * damageMultiplier,
                            force = MageWeapon.Flamethrower.force * damageMultiplier,
                            muzzleName = muzzleString,
                            hitEffectPrefab = MageWeapon.Flamethrower.impactEffectPrefab,
                            isCrit = self.isCrit,
                            radius = MageWeapon.Flamethrower.radius,
                            falloffModel = BulletAttack.FalloffModel.None,
                            stopperMask = LayerIndex.world.mask,
                            procCoefficient = MageWeapon.Flamethrower.procCoefficientPerTick,
                            maxDistance = self.maxDistance,
                            damageType = (Util.CheckRoll(MageWeapon.Flamethrower.ignitePercentChance, self.characterBody.master) ? DamageType.IgniteOnHit : DamageType.Generic),
                        };
                        if (!flamethrowerOptionSyncEffect) attack.tracerEffectPrefab = FireGatling.tracerEffectPrefab;
                        attack.Fire();
                    }
                });
            }
        }

        private void FireGatling_OnEnter(On.EntityStates.Drone.DroneWeapon.FireGatling.orig_OnEnter orig, FireGatling self)
        {
            orig(self);
            FireForAllOptions(self.characterBody, (option, behavior, _t, direction) =>
            {
                if (FireGatling.effectPrefab) option.MuzzleEffect(FireGatling.effectPrefab, false);
                if (self.isAuthority)
                {
                    new BulletAttack
                    {
                        owner = self.gameObject,
                        weapon = option,
                        origin = option.transform.position,
                        aimVector = direction,
                        minSpread = FireGatling.minSpread,
                        maxSpread = FireGatling.maxSpread,
                        damage = FireGatling.damageCoefficient * self.damageStat * damageMultiplier,
                        force = FireGatling.force * damageMultiplier,
                        tracerEffectPrefab = FireGatling.tracerEffectPrefab,
                        muzzleName = "Muzzle",
                        hitEffectPrefab = FireGatling.hitEffectPrefab,
                        isCrit = Util.CheckRoll(self.critStat, self.characterBody.master)
                    }.Fire();
                }
            });
        }

        private void FireTurret_OnEnter(On.EntityStates.Drone.DroneWeapon.FireTurret.orig_OnEnter orig, FireTurret self)
        {
            orig(self);
            FireForAllOptions(self.characterBody, (option, behavior, _t, direction) =>
            {
                if (FireTurret.effectPrefab) option.MuzzleEffect(FireTurret.effectPrefab, false);
                if (self.isAuthority)
                {
                    new BulletAttack
                    {
                        owner = self.gameObject,
                        weapon = option,
                        origin = option.transform.position,
                        aimVector = direction,
                        minSpread = FireTurret.minSpread,
                        maxSpread = FireTurret.maxSpread,
                        damage = FireTurret.damageCoefficient * self.damageStat * damageMultiplier,
                        force = FireTurret.force * damageMultiplier,
                        tracerEffectPrefab = FireTurret.tracerEffectPrefab,
                        muzzleName = "Muzzle",
                        hitEffectPrefab = FireTurret.hitEffectPrefab,
                        isCrit = Util.CheckRoll(self.critStat, self.characterBody.master)
                    }.Fire();
                }
            });
        }

        private void FireMegaTurret_FireBullet(On.EntityStates.Drone.DroneWeapon.FireMegaTurret.orig_FireBullet orig, FireMegaTurret self, string muzzleString)
        {
            orig(self, muzzleString);
            FireForAllOptions(self.characterBody, (option, behavior, _t, direction) =>
            {
                if (FireMegaTurret.effectPrefab) option.MuzzleEffect(FireMegaTurret.effectPrefab, false);
                if (self.isAuthority)
                {
                    new BulletAttack
                    {
                        owner = self.gameObject,
                        weapon = option,
                        origin = option.transform.position,
                        aimVector = direction,
                        minSpread = FireMegaTurret.minSpread,
                        maxSpread = FireMegaTurret.maxSpread,
                        damage = FireMegaTurret.damageCoefficient * self.damageStat * damageMultiplier,
                        force = FireMegaTurret.force * damageMultiplier,
                        tracerEffectPrefab = FireMegaTurret.tracerEffectPrefab,
                        muzzleName = muzzleString,
                        hitEffectPrefab = FireMegaTurret.hitEffectPrefab,
                        isCrit = Util.CheckRoll(self.critStat, self.characterBody.master)
                    }.Fire();
                }
            });
        }

        private void FireMissileBarrage_FireMissile(On.EntityStates.Drone.DroneWeapon.FireMissileBarrage.orig_FireMissile orig, FireMissileBarrage self, string targetMuzzle)
        {
            orig(self, targetMuzzle);
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (FireMissileBarrage.effectPrefab) option.MuzzleEffect(FireMissileBarrage.effectPrefab, false);
                if (self.isAuthority)
                {
                    Ray aimRay = self.GetAimRay();
                    float x = Random.Range(FireMissileBarrage.minSpread, FireMissileBarrage.maxSpread);
                    float z = Random.Range(0f, 360f);
                    Vector3 up = Vector3.up;
                    Vector3 axis = Vector3.Cross(up, aimRay.direction);
                    Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
                    float y = vector.y;
                    vector.y = 0f;
                    float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
                    float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f;
                    Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * aimRay.direction);
                    ProjectileManager.instance.FireProjectile(FireMissileBarrage.projectilePrefab, option.transform.position,
                                                              Util.QuaternionSafeLookRotation(forward), self.gameObject,
                                                              self.damageStat * FireMissileBarrage.damageCoefficient * damageMultiplier,
                                                              0f, Util.CheckRoll(self.critStat, self.characterBody.master),
                                                              DamageColorIndex.Default, null, -1f);
                }
            });
        }

        private void FireTwinRocket_FireProjectile(On.EntityStates.Drone.DroneWeapon.FireTwinRocket.orig_FireProjectile orig, FireTwinRocket self, string muzzleString)
        {
            orig(self, muzzleString);
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (FireTwinRocket.muzzleEffectPrefab) option.MuzzleEffect(FireTwinRocket.muzzleEffectPrefab, false);
                if (self.isAuthority && FireTwinRocket.projectilePrefab != null)
                {
                    float maxDistance = 1000f;
                    Vector3 forward = self.GetAimRay().direction;
                    Vector3 position = option.transform.position;
                    if (Physics.Raycast(position, forward, out RaycastHit raycastHit, maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask))
                    {
                        forward = (raycastHit.point - position).normalized;
                    }
                    ProjectileManager.instance.FireProjectile(FireTwinRocket.projectilePrefab, position,
                                                              Util.QuaternionSafeLookRotation(forward),
                                                              self.gameObject, self.damageStat * FireTwinRocket.damageCoefficient * damageMultiplier,
                                                              FireTwinRocket.force * damageMultiplier,
                                                              Util.CheckRoll(self.critStat, self.characterBody.master),
                                                              DamageColorIndex.Default, null, -1f);
                }
            });
        }

        private void ChargeMegaLaser_OnEnter(On.EntityStates.TitanMonster.ChargeMegaLaser.orig_OnEnter orig, ChargeMegaLaser self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                Transform transform = option.transform;
                if (self.effectPrefab)
                {
                    if (behavior.U.SafeCheck("laserChargeEffect"))
                    {
                        EntityState.Destroy(behavior.U["laserChargeEffect"]);
                    }
                    behavior.U["laserChargeEffect"] = Object.Instantiate(self.effectPrefab, transform.position, transform.rotation);
                    ((GameObject)behavior.U["laserChargeEffect"]).transform.parent = transform;
                    var component = ((GameObject)behavior.U["laserChargeEffect"]).GetComponent<ScaleParticleSystemDuration>();
                    if (component) component.newDuration = self.duration;
                }
            });
        }

        private void ChargeMegaLaser_OnExit(On.EntityStates.TitanMonster.ChargeMegaLaser.orig_OnExit orig, ChargeMegaLaser self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("laserChargeEffect")) EntityState.Destroy(behavior.U["laserChargeEffect"]);
            });
        }

        private void FireMegaLaser_OnEnter(On.EntityStates.TitanMonster.FireMegaLaser.orig_OnEnter orig, FireMegaLaser self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect || !self.laserPrefab) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (self.laserPrefab)
                {
                    if (behavior.U.SafeCheck("laserFire")) EntityState.Destroy(behavior.U["laserFire"]);
                    if (behavior.U.SafeCheck("laserChildLocator")) EntityState.Destroy(behavior.U["laserChildLocator"]);
                    if (behavior.U.SafeCheck("laserFireEnd")) EntityState.Destroy(behavior.U["laserFireEnd"]);
                    Transform transform = option.transform;
                    behavior.U["laserFire"] = Object.Instantiate(self.laserPrefab, transform.position, transform.rotation);
                    ((GameObject)behavior.U["laserFire"]).transform.parent = transform;
                    behavior.U["laserChildLocator"] = ((GameObject)behavior.U["laserFire"]).GetComponent<ChildLocator>();
                    behavior.U["laserFireEnd"] = ((ChildLocator)behavior.U["laserChildLocator"]).FindChild("LaserEnd");
                }
            });
        }

        private void FireMegaLaser_OnExit(On.EntityStates.TitanMonster.FireMegaLaser.orig_OnExit orig, FireMegaLaser self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("laserFire")) EntityState.Destroy(behavior.U["laserFire"]);
                if (behavior.U.SafeCheck("laserChildLocator")) EntityState.Destroy(behavior.U["laserChildLocator"]);
                if (behavior.U.SafeCheck("laserFireEnd")) EntityState.Destroy(behavior.U["laserFireEnd"]);
            });
        }

        private void FireGoldMegaLaser_FixedUpdate(On.EntityStates.TitanMonster.FireGoldMegaLaser.orig_FixedUpdate orig, FireGoldMegaLaser self)
        {
            float oldFireStopwatch = self.fireStopwatch + Time.fixedDeltaTime;
            float oldProjectileStopwatch = self.projectileStopwatch + Time.fixedDeltaTime * self.attackSpeedStat;
            orig(self);
            if (self.isAuthority)
            {
                if (!self.lockedOnHurtBox && self.foundAnyTarget) return;
                if ((!self.inputBank || !self.inputBank.skill4.down) && self.stopwatch > FireMegaLaser.minimumDuration) return;
                if (self.stopwatch > FireMegaLaser.maximumDuration) return;
            }
            FireForAllOptions(self.characterBody, (option, behavior, target, direction) =>
            {
                Vector3 position = option.transform.position;
                if (!target && self.lockedOnHurtBox) direction = (self.lockedOnHurtBox.transform.position - position).normalized;

                Vector3 point = direction * FireMegaLaser.maxDistance;
                if (Physics.Raycast(position, point, out RaycastHit raycastHit1, FireMegaLaser.maxDistance,
                                    LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
                {
                    point = raycastHit1.point;
                }
                Ray ray = new Ray(position, point - position);
                bool flag = false;
                if (behavior.U.SafeCheck("laserFire") && behavior.U.SafeCheck("laserChildLocator"))
                {
                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit raycastHit2, ray.direction.magnitude,
                                        LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
                    {
                        point = raycastHit2.point;
                        if (Physics.Raycast(point - ray.direction * .1f, -ray.direction, out RaycastHit raycastHit3, raycastHit2.distance,
                                            LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
                        {
                            point = ray.GetPoint(0.1f);
                            flag = true;
                        }
                    }
                    ((GameObject)behavior.U["laserFire"]).transform.rotation = Util.QuaternionSafeLookRotation(point - position);
                    ((Transform)behavior.U["laserFireEnd"]).position = point;
                }

                if (oldFireStopwatch > 1f / FireMegaLaser.fireFrequency)
                {
                    if (!flag)
                    {
                        if (self.effectPrefab) option.MuzzleEffect(self.effectPrefab, false);
                        if (self.isAuthority)
                        {
                            BulletAttack bulletAttack = new BulletAttack
                            {
                                owner = self.gameObject,
                                weapon = option,
                                origin = option.transform.position,
                                aimVector = direction,
                                minSpread = FireMegaLaser.minSpread,
                                maxSpread = FireMegaLaser.maxSpread,
                                bulletCount = 1U,
                                damage = (FireMegaLaser.damageCoefficient * self.damageStat / FireMegaLaser.fireFrequency) * damageMultiplier,
                                force = FireMegaLaser.force,
                                muzzleName = "Muzzle",
                                hitEffectPrefab = self.hitEffectPrefab,
                                isCrit = Util.CheckRoll(self.critStat, self.characterBody.master),
                                procCoefficient = FireMegaLaser.procCoefficientPerTick,
                                HitEffectNormal = false,
                                radius = 0f,
                                maxDistance = (point - ray.origin).magnitude + .1f
                            };
                            if (!aurelioniteOptionSyncEffect) bulletAttack.tracerEffectPrefab = FireGatling.tracerEffectPrefab;
                            bulletAttack.Fire();
                        }
                    }
                }
                if (self.isAuthority && oldProjectileStopwatch >= 1f / FireGoldMegaLaser.projectileFireFrequency)
                {
                    direction = Util.ApplySpread(direction, FireGoldMegaLaser.projectileMinSpread, FireGoldMegaLaser.projectileMaxSpread, 1f, 1f, 0f, 0f);
                    ProjectileManager.instance.FireProjectile(FireGoldMegaLaser.projectilePrefab, position, Util.QuaternionSafeLookRotation(direction),
                                                              self.gameObject, self.damageStat * FireMegaLaser.damageCoefficient, 0f,
                                                              Util.CheckRoll(self.critStat, self.characterBody.master), DamageColorIndex.Default, null, -1f);
                }
            });
        }

        private void FireGoldFist_PlacePredictedAttack(On.EntityStates.TitanMonster.FireGoldFist.orig_PlacePredictedAttack orig, FireGoldFist self)
        {
            orig(self);
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                int fistNumber = 0;
                float multiplier = behavior.ownerOt.GetRotateMultiplier();
                Vector3 predictedTargetPosition = self.predictedTargetPosition + behavior.DecidePosition(behavior.ownerOt.currentOptionAngle) * multiplier;
                Vector3 a = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward;
                int halfCount = FireGoldFist.fistCount / 2;
                for (int i = -halfCount; i < halfCount; i++)
                {
                    Vector3 fistPosition = predictedTargetPosition + FireGoldFist.distanceBetweenFists * i * a;
                    if (Physics.Raycast(new Ray(fistPosition + Vector3.up * 30f, Vector3.down), out RaycastHit raycastHit, 60f,
                                        LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
                    {
                        fistPosition = raycastHit.point;
                    }
                    float delay = FireGoldFist.delayBetweenFists * fistNumber++;
                    if (self.isAuthority)
                    {
                        FireProjectileInfo fireProjectileInfo = new FireProjectileInfo()
                        {
                            projectilePrefab = self.fistProjectilePrefab,
                            position = fistPosition,
                            rotation = Quaternion.identity,
                            owner = self.gameObject,
                            damage = self.damageStat * FireFist.fistDamageCoefficient * damageMultiplier,
                            force = FireFist.fistForce * damageMultiplier,
                            crit = self.characterBody.RollCrit(),
                            fuseOverride = FireFist.entryDuration - FireFist.trackingDuration + delay
                        };
                        ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                    }
                }
            });
        }

        private void FireFist_OnEnter(On.EntityStates.TitanMonster.FireFist.orig_OnEnter orig, FireFist self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("fistChargeEffect")) EntityState.Destroy(behavior.U["fistChargeEffect"]);
                behavior.U["fistChargeEffect"] = Object.Instantiate(self.chargeEffectPrefab, option.transform);
            });
        }

        private void FireFist_OnExit(On.EntityStates.TitanMonster.FireFist.orig_OnExit orig, FireFist self)
        {
            orig(self);
            if (!aurelioniteOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("fistChargeEffect")) EntityState.Destroy(behavior.U["fistChargeEffect"]);
            });
        }

        private void TitanRockController_Start(On.RoR2.TitanRockController.orig_Start orig, TitanRockController self)
        {
            orig(self);
            OptionTracker tracker = self.ownerCharacterBody.GetComponent<OptionTracker>();
            if (tracker) self.fireInterval = self.fireInterval.SafeDivide((tracker.existingOptions.Count + 1) * damageMultiplier);
        }

        private void FireSpine_FireOrbArrow(On.EntityStates.Squid.SquidWeapon.FireSpine.orig_FireOrbArrow orig, FireSpine self)
        {
            bool oldFireArrow = self.hasFiredArrow;
            orig(self);
            if (oldFireArrow || !NetworkServer.active) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                HurtBox hurtBox = self.enemyFinder.GetResults().FirstOrDefault();
                SquidOrb squidOrb = new SquidOrb
                {
                    damageValue = self.characterBody.damage * FireSpine.damageCoefficient * damageMultiplier,
                    isCrit = Util.CheckRoll(self.characterBody.crit, self.characterBody.master),
                    teamIndex = TeamComponent.GetObjectTeam(self.gameObject),
                    attacker = self.gameObject,
                    procCoefficient = FireSpine.damageCoefficient,
                    origin = option.transform.position,
                    target = hurtBox
                };
                if (FireSpine.muzzleflashEffectPrefab) option.MuzzleEffect(FireSpine.muzzleflashEffectPrefab, false);
                OrbManager.instance.AddOrb(squidOrb);
            });
        }

        private void FireSunder_OnEnter(On.EntityStates.BeetleGuardMonster.FireSunder.orig_OnEnter orig, FireSunder self)
        {
            orig(self);
            if (!beetleGuardOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("sunderEffect")) EntityState.Destroy(behavior.U["sunderEffect"]);
                if (FireSunder.chargeEffectPrefab) behavior.U["sunderEffect"] = Object.Instantiate(FireSunder.chargeEffectPrefab, option.transform);
            });
        }

        private void FireSunder_OnExit(On.EntityStates.BeetleGuardMonster.FireSunder.orig_OnExit orig, FireSunder self)
        {
            orig(self);
            if (!beetleGuardOptionSyncEffect) return;
            FireForAllOptions(self.characterBody, (option, behavior, _t, _d) =>
            {
                if (behavior.U.SafeCheck("sunderEffect")) EntityState.Destroy(behavior.U["sunderEffect"]);
            });
        }

        private void FireSunder_FixedUpdate(On.EntityStates.BeetleGuardMonster.FireSunder.orig_FixedUpdate orig, FireSunder self)
        {
            bool oldHasAttacked = self.hasAttacked;
            orig(self);
            FireForAllOptions(self.characterBody, (option, behavior, _t, direction) =>
            {
                if (self.modelAnimator && self.modelAnimator.GetFloat("FireSunder.activate") > 0.5f && !oldHasAttacked)
                {
                    if (self.isAuthority && self.modelTransform && FireSunder.projectilePrefab)
                    {
                        Ray aimRay = new Ray(option.transform.position, direction);
                        ProjectileManager.instance.FireProjectile(
                            FireSunder.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction),
                            self.gameObject, self.damageStat * FireSunder.damageCoefficient * damageMultiplier,
                            FireSunder.forceMagnitude * damageMultiplier, Util.CheckRoll(self.critStat, self.characterBody.master),
                            DamageColorIndex.Default, null, -1f
                        );
                    }
                    if (beetleGuardOptionSyncEffect)
                    {
                        if (behavior.U.SafeCheck("sunderEffect")) EntityState.Destroy(behavior.U["sunderEffect"]);
                    }
                }
            });
        }

        private void GroundSlam_OnEnter(On.EntityStates.BeetleGuardMonster.GroundSlam.orig_OnEnter orig, GroundSlam self)
        {
            orig(self);
            OptionTracker tracker = self.characterBody.GetComponent<OptionTracker>();
            if (tracker)
            {
                float multiplier = (tracker.existingOptions.Count + 1) * damageMultiplier;
                self.attack.damage *= multiplier;
                self.attack.pushAwayForce *= multiplier;
            }
        }

        private bool EquipmentSlot_PerformEquipmentAction(On.RoR2.EquipmentSlot.orig_PerformEquipmentAction orig, EquipmentSlot self, EquipmentDef equipmentDef)
        {
            bool returnValue = orig(self, equipmentDef);
            if (!returnValue) return false;
            CharacterBody body = self.characterBody;
            if (DroneNameCheck(body, "EquipmentDrone"))
            {
                OptionTracker tracker = body.GetOrAddComponent<OptionTracker>();
                int numberOfTimes = Mathf.FloorToInt(tracker.existingOptions.Count * equipmentDuplicationMultiplier);
                for (int i = 0; i < numberOfTimes; i++)
                {
                    orig(self, equipmentDef);
                }
            }
            return true;
        }
    }
}