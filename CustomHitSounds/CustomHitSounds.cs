using ModHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Assets.Scripts.GameCore.HostComponent;
using System.Reflection;
using System.IO;
using Assets.Scripts.PeroTools.Commons;
using GameLogic;
using FormulaBase;
using Assets.Scripts.PeroTools.Managers;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using Assets.Scripts.PeroTools.AssetBundles;
using RuntimeAudioClipLoader;
using Newtonsoft.Json;

namespace CustomHitSounds
{
    public class CustomHitSounds : IMod
    {
        private static String Custom_Sounds_Path = "Custom_Sounds";
        private static Dictionary<String, String> _audioFiles = new Dictionary<String, String>();
        private static Config _config = null;
        private static readonly string[] PossibleFileNames =
        {
            ".aiff", ".mp3", ".ogg", ".wav"
        };

        public string Name => "CustomHitSounds";

        public string Description => "Modify the game sounds.";

        public string Author => "Dom Gintoki";

        public string HomePage => "https://github.com/domgjntoki/CustomHitSounds";

        public void DoPatching()
        {
            Harmony harmony = new Harmony("com.domgintoki.customhitsounds");
            _config = ConfigLoader.GetConfig();
            _audioFiles = GetAllAudioFiles();
            harmony.PatchAll();
            ModLogger.Debug("CustomHitSounds loaded successfully");
        }

        private static string StripExtension(string filename)
        {
            if (filename.Contains("."))
                return filename.Substring(0, filename.LastIndexOf(".", StringComparison.Ordinal));
            else
                return filename;
        }
        private static Dictionary<string, string> GetAllAudioFiles()
        {
            var filesInfo = new Dictionary<string, string>();
            if (!Directory.Exists(Custom_Sounds_Path))
            {
                Directory.CreateDirectory(Custom_Sounds_Path);
                return filesInfo;
            }
            
            var d = new DirectoryInfo(Custom_Sounds_Path);
            
            var files = d.GetFiles();
            foreach(var file in files)
            {
                if (PossibleFileNames.Contains(file.Extension)) {
                    filesInfo[StripExtension(file.Name)] = file.FullName;
                }
            }
            return filesInfo;
        }

        [HarmonyPatch]
        class Patch
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(AssetBundle), "LoadAsset", new Type[] { typeof(string), typeof(Type) })]
            // ReSharper disable once InconsistentNaming
            public static bool LoadAssetPrefix(string name, Type type, ref UnityEngine.Object __result)
            {
                var separated = name.Split('/');
                var filename = separated[separated.Length - 1];
                if (_config.ShouldDebug)
                {
                    if (_config.DebugFileExtensions.Any(x => filename.EndsWith(x)))
                    {
                        ModLogger.Debug($"Loading Asset ({type}) {filename}");
                    }
                }

                if (PossibleFileNames.Any(x => filename.EndsWith(x)) &&
                    _audioFiles.TryGetValue(StripExtension(filename), out var filepath) &&
                    type == typeof(AudioClip))
                {
                    if (typeof(AudioClip) == type)
                        __result = Manager.Load(filepath);
                    return false;
                }

                return true;
            }
        }
    }
}
