
This mod will replace Muse Dash Audio files with yours.

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

The supported file extensions are [.aiff, .mp3, .ogg, .wav]

Some filenames that I found on the game and its explanations:

```yaml
Hitsounds:
  char_common_empty_atk: Hitsound when you do a empty attack on ground
  sfx_mezzo_1: Hitsound when successfully hitting small enemies, or geminis.
  sfx_forte_2: Low pitch sound, usually bigger enemy
  sfx_forte_3: Hammers sounds
  sfx_piano_1, sfx_piano_2: Bigger enemies, hollow sound (Blue enemies)
  hitsound_000, hitsound_001, ..., hitsound_015: Hitsound for Mash Enemies,  you advance on each audio the more you hit.
  sfx_score: Hitsound for music notes
  sfx_hp: Hitsound for HP notes
  sfx_press_top: Hitsound for Sliders (Stars)
Other sounds:
  char_common_fever: The "Fever!" girl sound
  VoiceMarijaHurt05, VoiceMarijaHurt06, VoiceMarijaHurt09: Sounds that Marija makes when hurt 
```

# Debug Mode
The Debug Mode will show on the Muse Dash ModLoader Console each and every audio asset that the game is using, so you can identify what audio you need to modify.

You can activate the debug mode by setting this option on the CustomHitSounds.json file.
To do this, set the Debug Mode option to true like shown:
```json
{
  "debug_mode": true
}
```

One way of using the debug mode is setting your game display as window mode, and recording you playing while showing the console window, so you can see after playing all the notes and audio names.

