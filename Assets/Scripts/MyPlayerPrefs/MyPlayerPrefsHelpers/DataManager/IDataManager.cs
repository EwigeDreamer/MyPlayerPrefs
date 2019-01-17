using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.PlayerPrefs.Helpers
{
    public interface IDataManager<T>
    {
        bool IsInited { get; }
        bool TryGetEntry(string key, out T entry);
        void SetEntry(string key, T entry);
        bool ContainsKey(string key);
        bool DeleteKey(string key);
        void DeleteAll();
        void Save();
    }
}
