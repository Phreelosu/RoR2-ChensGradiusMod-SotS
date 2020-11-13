﻿using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chen.GradiusMod
{
    public static class DroneCatalog
    {
        internal static List<DroneInfo> allInstances = new List<DroneInfo>();
        internal static List<DroneInfo> enabledInstances = new List<DroneInfo>();

        /// <summary>
        /// Generates a list of data containing the custom drones of the mod that called this method.
        /// </summary>
        /// <param name="modGuid">The mod GUID</param>
        /// <param name="configFile">The file where the mod's custom drones will bind their configs</param>
        /// <returns>A list of DroneInfos from the mod that called this method.</returns>
        public static List<DroneInfo> Initialize(string modGuid, ConfigFile configFile)
        {
            List<DroneInfo> droneInfos = new List<DroneInfo>();
            bool filter(Type t) => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Drone));
            foreach (Type type in Assembly.GetCallingAssembly().GetTypes().Where(filter))
            {
                Drone droneInstance = (Drone)Activator.CreateInstance(type);
                DroneInfo newDroneInfo = new DroneInfo(modGuid, droneInstance, configFile);
                droneInfos.Add(newDroneInfo);
                if (!allInstances.Exists(droneInfo => newDroneInfo.mod == droneInfo.mod && newDroneInfo.instance.name == droneInfo.instance.name))
                {
                    allInstances.Add(newDroneInfo);
                }
            }
            return droneInfos;
        }

        /// <summary>
        /// Sets all the custom drones contained in the list up. Mod creators may make their own if they have a sophisticated logic.
        /// </summary>
        /// <param name="droneInfos">List of DroneInfos generated by Initialize</param>
        public static void SetupAll(List<DroneInfo> droneInfos)
        {
            foreach (DroneInfo droneInfo in droneInfos)
            {
                var droneInstance = droneInfo.instance;
                if (droneInstance.alreadySetup)
                {
                    Log.Warning($"{droneInfo.mod}:{droneInstance.name} was already set up. Skipping.");
                    continue;
                }
                if (droneInstance.SetupFirstPhase())
                {
                    droneInstance.SetupSecondPhase();
                    enabledInstances.Add(droneInfo);
                }
            }
        }
    }

    /// <summary>
    /// A structure that stores data of custom drones as well as where they originated from.
    /// </summary>
    public class DroneInfo
    {
        public string mod;
        public Drone instance;

        public DroneInfo(string mod, Drone instance, ConfigFile config)
        {
            this.mod = mod;
            this.instance = instance;
            this.instance.BindConfig(config);
        }
    }
}