﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsPlugin
{
    public class IAPToggleEditor : IDrawer
    {
        private Toggle.Toggle _toggle;
        private TabBar _tabBar;
        private TabView _tabView;

        private const string IAPKitEnabled = "IAPKitEnabled";

        public IAPToggleEditor(TabBar tabBar)
        {
            bool enabled = HMSMainEditorSettings.Instance.Settings.GetBool(IAPKitEnabled);
            _tabView = HMSIAPTabFactory.CreateTab("IAP");
            _tabBar = tabBar;
            _toggle = new Toggle.Toggle("IAP", enabled, OnStateChanged, true);
        }

        private void OnStateChanged(bool value)
        {
            if (value)
            {
                _tabBar.AddTab(_tabView);
            }
            else
            {
                _tabBar.RemoteTab(_tabView);
            }
            HMSMainEditorSettings.Instance.Settings.SetBool(IAPKitEnabled, value);
        }

        public void Draw()
        {
            _toggle.Draw();
        }
    }
}
