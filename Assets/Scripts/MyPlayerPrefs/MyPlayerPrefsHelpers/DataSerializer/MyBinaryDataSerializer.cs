using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace MyAssets.PlayerPrefs.Helpers
{
    public static class MyBinaryDataSerializer
    {
        public static bool Serialize(object obj, out byte[] bytes)
        {
            bytes = null;
            if (obj == null)
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Serialisation error: input object is null!");
#endif
                #endregion
                return false;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    formatter.Serialize(stream, obj);
                    bytes = stream.ToArray();
                    return true;
                }
                catch
                #region DEBUG
#if UNITY_EDITOR
            (Exception e)
#endif
                #endregion
                {
                    #region DEBUG
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError(string.Format("Serialisation error: {0}", e.ToString()));
#endif
                    #endregion
                    return false;
                }
            }
        }

        public static bool Deserialize(byte[] bytes, out object obj)
        {
            obj = null;
            if (bytes == null || bytes.Length == 0)
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Deserialisation error: input bytes is null or empty!");
#endif
                #endregion
                return false;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                try { obj = formatter.Deserialize(stream); return true; }
                catch
                #region DEBUG
#if UNITY_EDITOR
            (Exception e)
#endif
                #endregion
                {
                    #region DEBUG
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError(string.Format("Deserialisation error: {0}", e.ToString()));
#endif
                    #endregion
                    return false;
                }
            }
        }
    }
}
