using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace MustaabApp
{
    public class HybridWebView : WebView
    {
        Dictionary<string, Action> actions = new Dictionary<string, Action>();

        public void RegisterAction(string name, Action action)
        {
            actions[name] = action;
        }

        public void InvokeAction(string name)
        {
            if (actions.ContainsKey(name))
                actions[name].Invoke();
        }
    }
}
