using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FacelessASN.Bytes
{
    public struct ByteArray
    {
        public int Length;
        private byte[] _bytes;
        private byte _byteClusterSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">String from which new ByteArray will be created. MaxLength is 255</param>
        public ByteArray(string s)
        {
            _byteClusterSize = sizeof(char);
            Length = s.Length * _byteClusterSize;
            //disable setting default values to elements of an array
            _bytes = new byte[Length];
            var chars = s.ToCharArray();
            for (byte i = 0; i < s.Length; i++)
            {
                byte iClustered = (byte) (i * _byteClusterSize);
                for (byte j = 0; j < _byteClusterSize; j++)
                    _bytes[iClustered + j] =
                        Buffer.GetByte(chars, i * _byteClusterSize + j);
            }
        }

        /// <summary>
        /// returns string byte interpretation
        /// </summary>
        /// <returns></returns>
        public string AsString()
        {
            var builder = new StringBuilder();
            int charId = 0;
            int charCount = Length / _byteClusterSize;

            for (byte i = 0; i < charCount; i++)
            {
                byte iClustered = (byte) (i * _byteClusterSize);
                for (int j = _byteClusterSize - 1; j >= 0; j--)
                {
                    charId += _bytes[iClustered + j];
                    charId = (charId << 8);
                }

                charId = (charId >> 8);
                builder.Append(char.ConvertFromUtf32(charId));
                charId = 0;
            }

            return builder.ToString();
        }
    }
}