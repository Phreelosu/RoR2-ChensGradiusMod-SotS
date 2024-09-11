![Version](https://img.shields.io/badge/Version-3.5.4-orange)
[![GitHub issues](https://img.shields.io/github/issues/cheeeeeeeeeen/RoR2-ChensGradiusMod)](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/issues)
![Maintenance Status](https://img.shields.io/badge/Maintenance-Active-green)

![RoR2: Chen's Gradius Mod](https://i.imgur.com/yIMFu24.png)

## Description

This mod aims to implement features from the Gradius series as well as from other classic shoot-em-up games. The mod also changes some vanilla aspects of the game regarding drones.

It contains a fully functional Drone API. The documentation can be found in the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki).

## Installation

Use **[Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager)** to install this mod.

If one does not want to use a mod manager, then get the DLL from **[Thunderstore](https://thunderstore.io/package/Chen/ChensGradiusMod/)**.

## Features

![Gradius' Option](https://puu.sh/GBI6M.png)
**Gradius' Option**
- A new Red Item added to Risk of Rain 2. Upon receiving this item, all owned drones of the receiver will gain an Option/Multiple for each stack.
- Gradius is known for its feature of Options/Multiples where in these weapons are invulnerable to all damage, and are able to copy the full arsenal of the main ship.
- For this mod, the Options/Multiples will only copy the main attacks of the drone.
- All vanilla minions (both mechanical and organic) are supported.
- All drones from this mod are also supported.
- Options/Multiples are able to duplicate all attacks of Aurelionite. The Rock Turret will attack faster instead of being copied.
- Options/Multiples will only duplicate the ranged attack of Beetle Guards. Their melee attacks will have multiplied damage, however.
- Equipment Drones will use the attached equipment depending on the number of Options/Multiples it has. This is configurable.
- Documentation is available for other mod creators. Check the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki).

![Gradius' Option Seed](https://puu.sh/HLj6S.png)
**Gradius' Option Seed**
- A new Green Item added to Risk of Rain 2. Upon acquiring this item, 2 Option Seeds will spawn for the player. They will duplicate the survivor's attacks for a % of damage dealt.
- The Option Seed is a fragment of the completed version of it. While it is in its younger stage, it is more organic than mechanical.
- Only offensive skills of vanilla survivors can be duplicated.
- The API makes it possible to implement customized skill behavior, even with non-offensive ones.
- Documentation is available for other mod creators with modded survivors. Check the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki).

![Beam Drone](https://puu.sh/GQz08.png)
**Beam Drone**
- A drone powered by this mod's API. Check the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki) on how to add your own custom drone.
- This drone shoots a continuous beam on its target.
- The drone is accurate, however, it is weak at keeping its lock on consistently.
- The drone will appear in Stage 3 and onwards.
- The drone will spawn more in Sky Meadow.
- Options also copy this drone's attacks.

![Laser Drone](https://puu.sh/GS59f.png)
**Laser Drone**
- A drone powered by this mod's API. Check the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki).
- The drone charges for a short amount of time, and then unleashes a strong laser attack, dealing huge amount of damage in an AoE.
- The drone will appear in Stage 3 and onwards.
- The drone will spawn more in Sky Meadow.
- Options also copy this drone's attacks.

![Psy Drone Red](https://puu.sh/HUKs0.png) ![Psy Drone Green](https://puu.sh/HUKrZ.png)
**Psy Drones**
- A drone powered by this mod's API. Check the [wiki](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki).
- Each drone unleashes strong and unique laser attacks.
- The drones can be bought as one.
- If one of them is destroyed, the other will also be decommissioned.
- Upon being destroyed, the first one to make contact with the map will spawn the interactable again if it is configured to be repurchaseable.
- The drones may appear as soon as in Stage 1, but it is expensive.
- Options also copy each drone's attacks, although not completely.

![Artifact of Machines](https://puu.sh/HQkF4.png)
**Artifact of Machines**
- An artifact added to Risk of Rain 2. Survivors will have a TC-280 Prototype drone when they spawn. Enemies, however, will also get drones.
- The Bacterian essence has drifted towards the embrace of the Planet. The Bulwark deemed it dangerous, and thus keeping it sealed in an artifact.
- No more drones can be repurchased when this artifact is active.
- When the owner dies, the drones will be decommissioned as well.
- Enemy drones will also have Gradius' Option if the Artifact of Evolution gives the item to the enemy.
- Configure Gradius' Option so that it is not blacklisted.

#### Other Features

- Emergency Drone's Null Exception Reference fix. Configurable to be turned off.
- Allows all drones to be repurchaseable. Configurable each.
- Allows an Equipment Drone to have a chance to drop its equipment upon being destroyed. Configurable.
- Makes the Flame Drones spawn more in Abyssal Depths. Config options also offer to allow Flame Drones to spawn more in Scorched Acres.
- Changes vanilla drone behaviors to be smarter, eliminating the problem of them attacking their own owners as well as widens mod compatibility.
- Set category of Gunner Turrets as Drones instead of Miscellaneous so that the Director will not spawn too many Gunner Turrets. Configurable.

## Contact & Support

- Issue Page: https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/issues
- Email: `blancfaye7@gmail.com`
- Discord: `Chen#1218`
- RoR2 Modding Server: https://discord.com/invite/5MbXZvd
- Give a tip through Ko-fi: https://ko-fi.com/cheeeeeeeeeen

## More Information

**Phreelosu** continued to maintain the mod in working order for the latest updates (as of September 1, 2024).
- Contact: [GitHub Account](https://github.com/Phreelosu)

**Kirbsuke#0352** made the 3D model for the Option/Multiple, and later used for the icon.
- Contact: `kirbydamaster@gmail.com` or through Discord.

**manaboi#4887** helped me edit the icon to make it look like one with vanilla items. Also made Artifact of Machines icon.
- Contact: Through Discord.

**KomradeSpectre#8468** made the 3D model for the Beam Drone and Laser Drone, which was also used for their icons.
- Contact: Through Discord.

## Changelog

**3.6.0**
- Added Option Seed support for Railgunner, Void Fiend, Seeker, Chef and False Son

**3.5.5**
- Initial fixes for SotS, you can ignore the startup errors of TILER2, wont break anything ingame
- Temporarily disabled Artifact of Machines until TILER2's artifacts are fixed

**3.5.4**
- Recompile for TILER2's breaking changes.
- Remove itemstats and betterui compatibility.

**3.5.3**
- With the changes of how things spawn in a map, the code must be updated to allow drones to spawn as well.
- Implement changes of new DirectorAPI.

**3.5.2**
- Update references to fetch the new R2API and the new DirectorAPI changes.

**3.5.1**
- Get rid of deprecation.
- Remove tests to reduce complexity for new maintainers.

**3.5.0**
- Update the code so that it works with the latest version.
- The code still has deprecation warnings.

*For the full changelog, check this [wiki page](https://github.com/cheeeeeeeeeen/RoR2-ChensGradiusMod/wiki/Changelog).*
