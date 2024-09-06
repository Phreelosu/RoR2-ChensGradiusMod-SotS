﻿using Aetherium;
using RoR2;
using Aetherium.Items.Tier2;
using Aetherium.Items.Tier3;
using static Aetherium.Items.Tier2.EngineersToolbelt;
using static Chen.GradiusMod.GradiusModPlugin;

namespace Chen.GradiusMod.Compatibility
{
    /// <summary>
    /// A static class that contains compatibility features from Aetherium mod.
    /// </summary>
    public static class Aetherium
    {
        internal static bool hasSetup = false;

        private static bool? _enabled;

        /// <summary>
        /// The check to use if Aetherium mod is enabled.
        /// Always do an if check before invoking methods in this class or else unexpected errors will happen in-game.
        /// </summary>
        public static bool enabled
        {
            get
            {
                if (_enabled == null) _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AetheriumPlugin.ModGuid);
                return (bool)_enabled;
            }
        }

        internal static void Setup()
        {
            if (hasSetup)
            {
                Log.Warning("Aetherium.Setup: Already performed. Skipping.");
                return;
            }
            if (generalCfg.equipmentDroneInspire)
            {
                Log.Debug("Aetherium.Setup: Applying equipmentDroneInspire.");
                AddInspiredCustomDrone("EquipmentDrone");
            }
            hasSetup = true;
        }

        /// <summary>
        /// Allows the Inspiring Drone to inspire the custom drone.
        /// </summary>
        /// <param name="masterName">The Character Master name of the drone to add.</param>
        public static void AddInspiredCustomDrone(string masterName)
        {
            InspiringDrone.instance.AddCustomDrone(masterName);
        }

        /// <summary>
        /// Allows the Engineers Toolbelt to revive and duplicate the custom drone.
        /// </summary>
        /// <param name="masterName">The Character Master name of the drone to add.</param>
        public static void AddEngineersToolbeltCustomDrone(string masterName)
        {
            EngineersToolbelt.instance.AddCustomDrone(masterName);
        }

        /// <summary>
        /// Determines if the CharacterBody is revived by Engineers Toolbelt.
        /// </summary>
        /// <param name="body">CharacterBody to check on.</param>
        /// <returns>True if revived by the item's effect. False if not.</returns>
        public static bool RevivedByEngineersToolbelt(CharacterBody body)
        {
            return body.gameObject.GetComponent<EngineersToolbeltRevivalFlag>();
        }
    }
}