
# CustomHitSounds
This mod will replace Muse Dash Audio files with yours.

# How to Install
Install the [Muse Dash Mod Loader](https://github.com/mo10/MuseDashModLoader/). If the ModLoader it's broken on the Mod Loader github releases page, get the latest release on the [Muse Dash Mod Community Discord Server](https://discord.gg/mdmc)

Download CustomHitSounds.dll on the [releases page](https://github.com/domgjntoki/CustomHitSounds/releases/tag/Release) and add it to the folder `Muse Dash/Mods `

# How to use it
To use it, download it on the releases page, and add on the Custom_Sounds folder inside the Muse Dash folder your custom sounds.

For example,
```
- MuseDash
    CustomHitSounds.json
    - Custom_Sounds
        sfx_mezzo_1.wav
        sfx_score.wav
        char_common_empty_atk.mp3
```
would replace the sfx_mezzo1, sfx_score and char_common_empty_atk assets from the game with your custom sounds.

The supported file extensions are [.aiff, .mp3, .ogg, .wav] (working on other types of assets)

Some filenames that I found on the game and its explanations:

```yaml
Battle Sounds:
  char_common_empty_atk: Hitsound when you do a empty attack on ground
  char_common_empty_jump: Hitsound for empty no-hit attacs on air
  sfx_mezzo_1: Hitsound when successfully hitting small enemies, or geminis.
  sfx_mezzo_3: Ghost hitsound
  sfx_ghost_gc: Groove coaster ghost noise
  sfx_forte_2: Low pitch sound, usually bigger enemy
  sfx_forte_3: Hammers sounds
  sfx_piano_2: Raider sound.
  sfx_block: Noise Yume makes when breaking a gear
  hitsound_000, hitsound_001, ..., hitsound_015: Hitsound for Mash Enemies,  you advance on each audio the more you hit.
  sfx_score: Hitsound for music notes
  sfx_hp: Hitsound for HP notes
  sfx_press_top: Hitsound for Sliders (Stars)
  sfx_press: Sound the hold makes while you're holding it
  sfx_jump: Sound when you jump
  char_common_fever: The "Fever!" girl sound
  sfx_readygo: Music... Ready... GO!
  sfx_readygo_gc: Let's... Groove Coaster!
  sfx_full_combo: Full Combo!
Other sounds:
  
  VoiceMarijaHurt05, VoiceMarijaHurt06, VoiceMarijaHurt09: Sounds that Marija makes when hurt 
```

# Debug Mode
The Debug Mode will show on the Muse Dash ModLoader Console each and every audio asset that the game is using, so you can identify what audio you need to modify.

You can activate the debug mode by setting this option on the CustomHitSounds.json file.
To do this, set the ShouldDebug option to true,
and set the file extensions that you want shown on the DebugMode.

Example:
```json
{
  "ShouldDebug": true,
  "DebugFileExtensions": [
    ".aiff", ".mp3", ".ogg", ".wav", ".json", ".prefab",
    ".asset"
  ]
}
```

One way of using the debug mode is setting your game display as window mode, and recording you playing while showing the console window, so you can see after playing all the notes and audio names.

Image showing the Debug Mode activated:
![Using debug mode](readme_resources/DebugExample.png)
![Debug Window](readme_resources/DebugLog.png)
