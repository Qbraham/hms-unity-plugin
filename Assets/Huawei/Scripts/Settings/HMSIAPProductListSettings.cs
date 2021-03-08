﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HmsPlugin
{
    public class HMSIAPProductListSettings : HMSEditorSingleton<HMSIAPProductListSettings>
    {
        private const string SettingsFilename = "HMSIAPProductListSettings";
        private SettingsScriptableObject loadedSettings;

        private Settings _settings;
        public Settings Settings => _settings;

        public HMSIAPProductListSettings()
        {
            loadedSettings = ScriptableHelper.Load<SettingsScriptableObject>(SettingsFilename, "Assets/Huawei/Settings/Resources");

            Debug.Assert(loadedSettings != null, "Failed to load the " + SettingsFilename);
            _settings = loadedSettings.settings;

            _settings.OnDictionaryChanged += _settings_OnDictionaryChanged;
        }

        private void _settings_OnDictionaryChanged()
        {
            loadedSettings.Save();
        }

        public void Reset()
        {
            _settings.Dispose();
            _instance = null;
        }

        public List<IAPProductEntry> GetAllIAPProducts()
        {
            var returnList = new List<IAPProductEntry>();

            for (int i = 0; i < _settings.Keys.Count(); i++)
            {
                returnList.Add(new IAPProductEntry(_settings.Keys.ElementAt(i), (IAPProductType)Enum.Parse(typeof(IAPProductType), _settings.Values.ElementAt(i))));
            }

            return returnList;
        }

        public List<string> GetProductIdentifiersByType(IAPProductType type)
        {
            var returnList = new List<string>();

            for (int i = 0; i < _settings.Keys.Count(); i++)
            {
                if (_settings.Values.ElementAt(i) == type.ToString())
                {
                    returnList.Add(_settings.Keys.ElementAt(i));
                }
            }

            return returnList;
        }
    }
}
