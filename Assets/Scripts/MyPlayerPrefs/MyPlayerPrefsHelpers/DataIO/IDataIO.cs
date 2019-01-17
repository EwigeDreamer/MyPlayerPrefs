using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.PlayerPrefs.Helpers
{
    public interface IDataIO<T>
    {
        bool TryLoad(out T data);
        bool TrySave(T data);
    }
}
