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
        private static Dictionary<String, String> AudioFiles = new Dictionary<String, String>();
        private static bool IsDebugModeActivated = false;
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

        public string HomePage => "https://github.com/domgjntoki/CustomHitSounds";

        public void DoPatching()
        {
            Harmony harmony = new Harmony("com.domgintoki.customhitsounds");
            IsDebugModeActivated = GetDebugModeOption();
            AudioFiles = GetAllAudioFiles();
            harmony.PatchAll();
            ModLogger.Debug("CustomHitSounds loaded successfully");
        }

        private static void ResetConfigFile(string configPath)
        {
            var create = JsonConvert.SerializeObject(new Dictionary<String, bool> {
                    { "debug_mode", false }
                });
            var tw = new StreamWriter(configPath);
            tw.Write(create);
            tw.Close();
        }
        private static bool GetDebugModeOption()
        {
            var configPath = "CustomHitSounds.json";
            if (!File.Exists(configPath))
            {
                ResetConfigFile(configPath);
                return false;
            } 
            else
            {
                string json = File.ReadAllText(configPath);
                Dictionary<String, bool> dict = null;
                try
                {
                    dict = JsonConvert.DeserializeObject<Dictionary<String, bool>>(json);
                } catch(Exception)  { }

                bool should;
                if(dict == null || !dict.TryGetValue("debug_mode", out should))
                {
                    ModLogger.Debug("Incorrect json file, recreating.");
                    ResetConfigFile(configPath);
                    return false;
                }  else
                {
                    return should;
                }

                                
            }

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
            private static List<String> downloaded = new List<String>();
            [HarmonyPostfix]
            [HarmonyPatch(typeof(AssetBundle), "LoadAsset", new Type[] { typeof(string), typeof(Type) })]
            public static void LoadAssetPostfix(string name, Type type, ref UnityEngine.Object __result)
            {
                var separated = name.Split('/');
                var filename = separated[separated.Length - 1];
                if (IsDebugModeActivated)
                {
                    if (possibleFileNames.Any(x => filename.EndsWith(x)))
                    {
                        ModLogger.Debug($"filename {filename}");
                    }
                }
                
                string filepath;
                if (AudioFiles.TryGetValue(StripExtension(filename), out filepath))
                {
                    __result = Manager.Load(filepath); 

                }
            }
        }

        //[HarmonyPatch]
        //class Patch2
        //{
        //    [HarmonyPostfix]
        //    [HarmonyPatch(typeof(AudioManager), "Preload", new[] { typeof(IList<string>), typeof(bool) })]
        //    public static void Postfix(IList<string> preloadAudioNames, bool isAndroidPreload, Dictionary<string, AudioClip> ___m_SfxBuffer)
        //    {
        //        var obj = string.Join(", ", new List<string>(___m_SfxBuffer.Keys).ToArray());
        //        ModLogger.AddLog("CustomHitSounds", "PostFixPreload", $"m_SfxBuffer keys: [{obj}]");

        //        foreach (var entry in ___m_SfxBuffer)
        //        {
        //            ModLogger.Debug($"Saving ${entry.Key}.wav");
        //            SavWav.Save($"C:/Games/Steam/steamapps/common/Muse Dash/Custom_Sounds/downloaded/{entry.Key}.wav", entry.Value); 
        //        }
        //    }
        //}



    }
}
