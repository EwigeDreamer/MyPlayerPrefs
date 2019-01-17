using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace MyAssets.PlayerPrefs.Helpers
{
    public class MyBinaryDataIO : IDataIO<byte[]>
    {
        readonly string m_DirPath = "";
        readonly string m_FileName = "";
        readonly string m_FileExt = "";
        private string FullPath { get { return string.Format("{0}/{1}.{2}", m_DirPath, m_FileName, m_FileExt); } }
        private string FullPathTmp { get { return string.Format("{0}/{1}_tmp.{2}", m_DirPath, m_FileName, m_FileExt); } }

        public MyBinaryDataIO(string dirPath, string fileName, string fileExtension)
        {
            char[] invalidChrs = Path.GetInvalidFileNameChars();
            foreach (var ch in invalidChrs)
                fileName = fileName.Replace(ch, '_');
            m_DirPath = !string.IsNullOrEmpty(dirPath) ? dirPath : "";
            m_FileName = !string.IsNullOrEmpty(fileName) ? fileName : "data";
            m_FileExt = !string.IsNullOrEmpty(fileExtension) ? fileExtension : "bin";
        }

        public bool LoadBytes(out byte[] bytes)
        {
            bytes = null;
            try
            {
                if (!File.Exists(FullPath))
                {
                    #region DEBUG
#if UNITY_EDITOR
                    UnityEngine.Debug.LogWarning(string.Format("Load bytes warning: file {0} is not exists!", FullPath));
#endif
                    #endregion
                    return false;
                }
                using (FileStream stream = new FileStream(FullPath, FileMode.Open))
                {
                    long lenght = stream.Length;
                    if (stream.Length > int.MaxValue)
                    {
                        #region DEBUG
#if UNITY_EDITOR
                        UnityEngine.Debug.LogError(string.Format("Load bytes error: file {0} is too big!", FullPath));
#endif
                        #endregion
                        return false;
                    }
                    BinaryReader reader = new BinaryReader(stream);
                    bytes = reader.ReadBytes((int)lenght);
                    return true;
                }
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
                UnityEngine.Debug.LogError(string.Format("Load bytes error: {0}", e.ToString()));
#endif
                #endregion
                return false;
            }
        }

        public bool SaveBytes(byte[] bytes)
        {
            try
            {
                if (!File.Exists(FullPath))
                    using (FileStream stream = new FileStream(FullPath, FileMode.Create))
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write(bytes);
                        return true;
                    }
                using (FileStream stream = new FileStream(FullPathTmp, FileMode.Create))
                {
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(bytes);
                }
                File.Delete(FullPath);
                File.Move(FullPathTmp, FullPath);
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
                UnityEngine.Debug.LogError(string.Format("Saving bytes error: {0}", e.ToString()));
#endif
                #endregion
                return false;
            }
        }

        bool IDataIO<byte[]>.TryLoad(out byte[] data)
        {
            return LoadBytes(out data);
        }

        bool IDataIO<byte[]>.TrySave(byte[] data)
        {
            return SaveBytes(data);
        }
    }
}
