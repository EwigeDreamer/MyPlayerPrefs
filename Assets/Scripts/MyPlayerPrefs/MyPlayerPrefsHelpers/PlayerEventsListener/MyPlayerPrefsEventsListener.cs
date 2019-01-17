using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyAssets.PlayerPrefs.Helpers
{
    public class MyPlayerPrefsApplicationEventsManager
    {
        MyPlayerPrefsEventsListener m_AplEvsObj;
        bool IsInited { get { return m_AplEvsObj != null; } }

        public event Action<bool> OnPauseEvent
        {
            add { if (IsInited) m_AplEvsObj.ApplicationPauseEvent += value; }
            remove { if (IsInited) m_AplEvsObj.ApplicationPauseEvent -= value; }
        }
        public event Action<bool> OnFocusEvent
        {
            add { if (IsInited) m_AplEvsObj.ApplicationFocusEvent += value; }
            remove { if (IsInited) m_AplEvsObj.ApplicationFocusEvent -= value; }
        }
        public event Action OnQuitEvent
        {
            add { if (IsInited) m_AplEvsObj.ApplicationQuitEvent += value; }
            remove { if (IsInited) m_AplEvsObj.ApplicationQuitEvent -= value; }
        }


        public MyPlayerPrefsApplicationEventsManager()
        {
            m_AplEvsObj = new GameObject("MyPlayerPrefsEventsListener").AddComponent<MyPlayerPrefsEventsListener>();
            UnityEngine.Object.DontDestroyOnLoad(m_AplEvsObj);
        }
    }

    public class MyPlayerPrefsEventsListener : MonoBehaviour
    {
        public event Action<bool> ApplicationPauseEvent;
        public event Action<bool> ApplicationFocusEvent;
        public event Action ApplicationQuitEvent;
        private void OnApplicationPause(bool pause)
        {
            ApplicationPauseEvent?.Invoke(pause);
        }
        private void OnApplicationFocus(bool focus)
        {
            ApplicationFocusEvent?.Invoke(focus);
        }
        private void OnApplicationQuit()
        {
            ApplicationQuitEvent?.Invoke();
        }
    }
}
