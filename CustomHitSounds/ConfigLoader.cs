using System;
using System.Collections.Generic;
using System.IO;
using ModHelper;
using Newtonsoft.Json;

namespace CustomHitSounds
{
    public class Config
    {
        public bool ShouldDebug = false;
        public string[] DebugFileExtensions = {".aiff", ".mp3", ".ogg", ".wav"};

        public override string ToString()
        {
            return $"{nameof(ShouldDebug)}: {ShouldDebug}, {nameof(DebugFileExtensions)}: {DebugFileExtensions}";
        }
    }
    public static class ConfigLoader
    {
        private const string ConfigPath = "CustomHitSounds.json";

        private static void ResetConfigFile()
        {
            var create = JsonConvert.SerializeObject(new Config(),  Formatting.Indented);
            var tw = new StreamWriter(ConfigPath);
            tw.Write(create);
            tw.Close();
        }
        
        public static Config GetConfig()
        {
            if (!File.Exists(ConfigPath))
            {
                ResetConfigFile();
                return new Config();
            }

            var json = File.ReadAllText(ConfigPath);
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            };
            Config config = null;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(json, settings);
            }
            catch (Exception)
            {
                // ignore 
            }
            
            if (config == null)
            {
                ModLogger.Debug("Incorrect json file, recreating.");
                ResetConfigFile();
                config = new Config();
            }
            return config;

        }
    }
    
}