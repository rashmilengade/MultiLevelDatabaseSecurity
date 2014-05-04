using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace healthcare
{
    class MD5Class
    {
        //Declarations
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;

        public MD5Class()
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
        }
        public string EncodePassword(string originalPassword)
        {
            
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            string encryptedString = ByteArrayToString(encodedBytes);
            return encryptedString;
        }
        public string decodePassword(string ciper)
        {
            encodedBytes = StringToByteArray(ciper);
            //Convert encoded bytes back to a 'readable' string
            //return BitConverter.ToString(encodedBytes);
            return Regex.Replace(BitConverter.ToString(encodedBytes), "-", "");
        }
        public static byte[] StringToByteArray(string str)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            return encoding.GetBytes(str);
        }

        public string ByteArrayToString(byte[] input)
        {
            UTF8Encoding enc = new UTF8Encoding();
            string str = enc.GetString(input);
            return str;
        }
    }
}
