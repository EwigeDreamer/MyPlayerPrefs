using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using MyAssets.PlayerPrefs.Helpers;

namespace MyAssets.PlayerPrefs
{
    public static class MyPlayerPrefs
    {
        static IDataManager<string[]> m_DataManager = null;
        static MyPlayerPrefsApplicationEventsManager m_EventsManager = null;

        public static event Action BeforeSaving;
        public static event Action AfterLoad;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            m_DataManager = new MyPlayerPrefsDataManager<string[]>("troll", "face");
            m_EventsManager = new MyPlayerPrefsApplicationEventsManager();
            m_EventsManager.OnFocusEvent += OnFocus;
            m_EventsManager.OnPauseEvent += OnPause;
            m_EventsManager.OnQuitEvent += OnQuit;
            AfterLoad?.Invoke();
        }
        static void OnQuit() { Save(); }
        static void OnPause(bool pause) { if (pause) Save(); }
        static void OnFocus(bool focus) { if (!focus) Save(); }

        public static void SetFloat(string key, float value)
        {
            var entry = new string[2];
            entry[0] = typeof(float).FullName;
            entry[1] = value.ToString();
            m_DataManager.SetEntry(key, entry);
        }
        public static float GetFloat(string key, float defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(float).FullName
                && float.TryParse(entry[1], out float entryValue))
                return entryValue;
            return defaultValue;
        }

        public static void SetInt(string key, int value)
        {
            var entry = new string[2];
            entry[0] = typeof(int).FullName;
            entry[1] = value.ToString();
            m_DataManager.SetEntry(key, entry);
        }
        public static int GetInt(string key, int defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(int).FullName
                && int.TryParse(entry[1], out int entryValue))
                return entryValue;
            return defaultValue;
        }

        public static void SetDouble(string key, double value)
        {
            var entry = new string[2];
            entry[0] = typeof(double).FullName;
            entry[1] = value.ToString();
            m_DataManager.SetEntry(key, entry);
        }
        public static double GetDouble(string key, double defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(double).FullName
                && double.TryParse(entry[1], out double entryValue))
                return entryValue;
            return defaultValue;
        }

        public static void SetString(string key, string value)
        {
            var entry = new string[2];
            entry[0] = typeof(string).FullName;
            entry[1] = value;
            m_DataManager.SetEntry(key, entry);
        }
        public static string GetString(string key, string defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(string).FullName)
                return entry[1];
            return defaultValue;
        }
        
        public static void SetVector2(string key, Vector2 value)
        {
            var entry = new string[2];
            entry[0] = typeof(Vector2).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Vector2 GetVector2(string key, Vector2 defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Vector2).FullName)
                try { return JsonUtility.FromJson<Vector2>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetVector2Int(string key, Vector2Int value)
        {
            var entry = new string[2];
            entry[0] = typeof(Vector2Int).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Vector2Int GetVector2Int(string key, Vector2Int defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Vector2Int).FullName)
                try { return JsonUtility.FromJson<Vector2Int>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetVector3(string key, Vector3 value)
        {
            var entry = new string[2];
            entry[0] = typeof(Vector3).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Vector3 GetVector3(string key, Vector3 defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Vector3).FullName)
                try { return JsonUtility.FromJson<Vector3>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetVector3Int(string key, Vector3Int value)
        {
            var entry = new string[2];
            entry[0] = typeof(Vector3Int).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Vector3Int GetVector3Int(string key, Vector3Int defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Vector3Int).FullName)
                try { return JsonUtility.FromJson<Vector3Int>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetVector4(string key, Vector4 value)
        {
            var entry = new string[2];
            entry[0] = typeof(Vector4).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Vector4 GetVector4(string key, Vector4 defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Vector4).FullName)
                try { return JsonUtility.FromJson<Vector4>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetColor(string key, Color value)
        {
            var entry = new string[2];
            entry[0] = typeof(Color).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Color GetColor(string key, Color defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Color).FullName)
                try { return JsonUtility.FromJson<Color>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetColor32(string key, Color32 value)
        {
            var entry = new string[2];
            entry[0] = typeof(Color32).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Color32 GetColor32(string key, Color32 defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Color32).FullName)
                try { return JsonUtility.FromJson<Color32>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetQuaternion(string key, Quaternion value)
        {
            var entry = new string[2];
            entry[0] = typeof(Quaternion).FullName;
            entry[1] = JsonUtility.ToJson(value);
            m_DataManager.SetEntry(key, entry);
        }
        public static Quaternion GetQuaternion(string key, Quaternion defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(Quaternion).FullName)
                try { return JsonUtility.FromJson<Quaternion>(entry[1]); }
                catch { }
            return defaultValue;
        }

        public static void SetTimeSpan(string key, TimeSpan value)
        {
            var entry = new string[2];
            entry[0] = typeof(TimeSpan).FullName;
            entry[1] = value.ToString();
            m_DataManager.SetEntry(key, entry);
        }
        public static TimeSpan GetTimeSpan(string key, TimeSpan defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(TimeSpan).FullName
                && TimeSpan.TryParse(entry[1], out TimeSpan entryValue))
                return entryValue;
            return defaultValue;
        }

        public static void SetDateTime(string key, DateTime value)
        {
            var entry = new string[2];
            entry[0] = typeof(DateTime).FullName;
            entry[1] = value.ToString();
            m_DataManager.SetEntry(key, entry);
        }
        public static DateTime GetDateTime(string key, DateTime defaultValue)
        {
            if (m_DataManager.TryGetEntry(key, out string[] entry)
                && entry[0] == typeof(DateTime).FullName
                && DateTime.TryParse(entry[1], out DateTime entryValue))
                return entryValue;
            return defaultValue;
        }

        public static bool ContainsKey(string key)
        {
            return m_DataManager.ContainsKey(key);
        }
        public static void DeleteKey(string key)
        {
            m_DataManager.DeleteKey(key);
        }
        public static void DeleteAll()
        {
            m_DataManager.DeleteAll();
        }
        public static void Save()
        {
            BeforeSaving?.Invoke();
            m_DataManager.Save();
        }

    }
}
