using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace MyAssets.PlayerPrefs.Helpers
{
    public class MyPlayerPrefsDataManager<T> : IDataManager<T>
    {
        MyDataStorageManager m_Storage = null;

        Dictionary<string, T> m_DataDict = null;
        bool IsInited { get { return m_DataDict != null; } }
        

        public MyPlayerPrefsDataManager(string fileName, string fileExt)
        {
            m_Storage = new MyDataStorageManager(fileName, fileExt);
            Load();
        }

        bool IDataManager<T>.IsInited { get { return m_DataDict != null && m_Storage != null; } }

        bool IDataManager<T>.TryGetEntry(string key, out T entry)
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                entry = default;
                return false;
            }
            #endregion
            return m_DataDict.TryGetValue(key, out entry);
        }

        void IDataManager<T>.SetEntry(string key, T entry)
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                return;
            }
            #endregion
            if (m_DataDict.ContainsKey(key))
                m_DataDict[key] = entry;
            else
                m_DataDict.Add(key, entry);
        }

        bool IDataManager<T>.ContainsKey(string key)
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                return false;
            }
            #endregion
            return m_DataDict.ContainsKey(key);
        }

        bool IDataManager<T>.DeleteKey(string key)
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                return false;
            }
            #endregion
            return m_DataDict.Remove(key);
        }

        void IDataManager<T>.DeleteAll()
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                return;
            }
            #endregion
            m_DataDict.Clear();
        }

        void IDataManager<T>.Save()
        {
            #region IF_IS_NOT_INITED
            if (!IsInited)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(string.Format("{0} is not inited!", typeof(MyPlayerPrefsDataManager<T>).Name));
#endif
                return;
            }
            #endregion
            var keys = new string[m_DataDict.Count];
            m_DataDict.Keys.CopyTo(keys, 0);
            var entries = new T[m_DataDict.Count];
            m_DataDict.Values.CopyTo(entries, 0);
            var data = new object[] { keys, entries };
            m_Storage.SaveData(data);
        }

        void Load()
        {
            object loadedData = m_Storage.LoadData();
            if (loadedData != null
                && loadedData is object[] data
                && data.Length == 2
                && data[0] != null
                && data[0] is string[] keys
                && data[1] != null
                && data[1] is T[] entries
                && keys.Length == entries.Length)
            {
                int count = keys.Length;
                m_DataDict = new Dictionary<string, T>(count);
                for (int i = 0; i < count; ++i)
                    m_DataDict.Add(keys[i], entries[i]);
                return;
            }
            m_DataDict = new Dictionary<string, T>();
        }
    }
}
