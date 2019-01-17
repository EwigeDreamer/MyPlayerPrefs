using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.PlayerPrefs.Helpers
{
    public interface IDataEncoder<T>
    {
        T Encode(T sourceData);
        T Decode(T encodedData);
        void EncodeNoGC(ref T data);
        void DecodeNoGC(ref T data);
    }
}
