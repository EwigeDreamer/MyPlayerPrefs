using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MyAssets.PlayerPrefs.Helpers
{
    public class MyDataStorageManager
    {
        IDataIO<byte[]> m_DataIO = null;
        IDataEncoder<byte[]> m_DataEncoder = null;

        string DirPath
        {
            get
            {
#if UNITY_EDITOR
                string path = Application.dataPath;
                return path.Substring(0, path.LastIndexOf("/Assets"));
#else
                return Application.persistentDataPath;
#endif
            }
        }

        public MyDataStorageManager(string fileName, string fileExt)
        {
            m_DataIO = new MyBinaryDataIO(DirPath, fileName, fileExt);
            m_DataEncoder = new CaesarBinaryDataEncoder(128);
        }

        public void SaveData(object data)
        {
            byte[] bytes = null;
            if (!MyBinaryDataSerializer.Serialize(data, out bytes))
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Data serialization error!");
#endif
                #endregion
                return;
            }
            m_DataEncoder.EncodeNoGC(ref bytes);
            if (!m_DataIO.TrySave(bytes))
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Data saving error!");
#endif
                #endregion
                return;
            }
        }

        public object LoadData()
        {
            byte[] bytes;
            if (!m_DataIO.TryLoad(out bytes))
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarning("Data is not loaded!");
#endif
                #endregion
                return null;
            }
            m_DataEncoder.DecodeNoGC(ref bytes);
            object data;
            if (!MyBinaryDataSerializer.Deserialize(bytes, out data))
            {
                #region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Data deserialization error!");
#endif
                #endregion
                return null;
            }
            return data;
        }
    }
}
