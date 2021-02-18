using System;
using System.IO;

namespace P3DCleaner
{
    internal class Utility
    {
        //Converts next four (little endian) bytes in given FileStream to int
        public static int ReadUint32(FileStream reader)
        {
            byte[] tmp = new byte[4];
            reader.Read(tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }

        public static byte[] ReadByte4(FileStream reader)
        {
            byte[] tmp = new byte[4];
            reader.Read(tmp, 0, 4);
            return tmp;
        }

        //Converts next four (little endian) bytes in reader to int
        public static int Byte4ToInt(byte[] arr, int offset)
        {
            byte[] tmp = new byte[4];
            Array.Copy(arr, offset, tmp, 0, 4);
            return BitConverter.ToInt32(tmp, 0);
        }

        public static int Byte2ToInt(byte[] arr, int offset)
        {
            byte[] tmp = new byte[2];
            Array.Copy(arr, offset, tmp, 0, 2);
            return BitConverter.ToInt16(tmp, 0);
        }
    }
}