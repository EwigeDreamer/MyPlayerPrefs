using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.PlayerPrefs.Helpers
{
    public class CaesarBinaryDataEncoder : IDataEncoder<byte[]>
    {
        byte m_Offset;

        public CaesarBinaryDataEncoder(byte offset)
        {
            m_Offset = offset;
        }

        byte[] IDataEncoder<byte[]>.Encode(byte[] sourceData)
        {
            int count = sourceData.Length;
            byte[] encodedData = new byte[count];
            for (int i = 0; i < count; ++i)
                encodedData[i] = (byte)(sourceData[i] + m_Offset);
            return encodedData;
        }
        byte[] IDataEncoder<byte[]>.Decode(byte[] encodedData)
        {
            int count = encodedData.Length;
            byte[] decodedData = new byte[count];
            for (int i = 0; i < count; ++i)
                decodedData[i] = (byte)(encodedData[i] - m_Offset);
            return decodedData;
        }

        void IDataEncoder<byte[]>.EncodeNoGC(ref byte[] data)
        {
            int count = data.Length;
            for (int i = 0; i < count; ++i)
                data[i] += m_Offset;
        }
        void IDataEncoder<byte[]>.DecodeNoGC(ref byte[] data)
        {
            int count = data.Length;
            for (int i = 0; i < count; ++i)
                data[i] -= m_Offset;
        }
    }
}
