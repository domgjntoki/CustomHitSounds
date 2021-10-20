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

namespace CustomHitSounds
{
    public class CustomHitSounds : IMod
    {
        private static String Custom_Sounds_Path = "Custom_Sounds";
        private static Dictionary<String, String> AudioFiles = new Dictionary<String, String>();
        private static string[] possibleFileNames = new string[]
            {
                ".aiff",
                ".mp3",
                ".ogg",
                ".wav"
            };

        public string Name => "CustomHitSounds";

        public string Description => "Modify the game sounds.";

        public string Author => "Dom Gintoki";

        public string HomePage => "";

        public void DoPatching()
        {
            Harmony harmony = new Harmony("com.domgintoki.customhitsounds");
            AudioFiles = GetAllAudioFiles();
            harmony.PatchAll();
            ModLogger.Debug("CustomHitSounds loaded successfully");
        }

        private static String StripExtension(string filename)
        {
            if (filename.Contains("."))
                return filename.Substring(0, filename.LastIndexOf("."));
            else
                return filename;
        }
        private static Dictionary<String, String> GetAllAudioFiles()
        {
            var filesInfo = new Dictionary<String, String>();
            if (!Directory.Exists(Custom_Sounds_Path))
            {
                Directory.CreateDirectory(Custom_Sounds_Path);
                return filesInfo;
            }
            
            var d = new DirectoryInfo(Custom_Sounds_Path);
            
            var files = d.GetFiles();
            foreach(var file in files)
            {
                if (possibleFileNames.Contains(file.Extension)) {
                    filesInfo[StripExtension(file.Name)] = file.FullName;
                }
            }
            return filesInfo;
        }

        [HarmonyPatch]
        class Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(AssetBundle), "LoadAsset", new Type[] { typeof(string), typeof(Type) })]
            public static void LoadAssetPostfix(string name, Type type, ref UnityEngine.Object __result)
            {
                var separated = name.Split('/');
                var filename = separated[separated.Length - 1];
                string filepath;
                if (AudioFiles.TryGetValue(StripExtension(filename), out filepath))
                {
                    ModLogger.Debug($"filename {filename} found loaded :) test: {filepath}"); 
                    __result = Manager.Load(filepath); 

                }
            }
        }



    }
}
