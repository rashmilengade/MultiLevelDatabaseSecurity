﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using healthcare.Exceptions;

namespace healthcare
{
    class MyMD5
    {
        public static string Encrypt(string toEncrypt, string securityKey, bool useHashing)
        {
            string retVal = string.Empty;

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                // Validate inputs
                ValidateInput(toEncrypt);
                ValidateInput(securityKey);

                // If hashing use get hashcode regards to your key
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));

                    // Always release the resources and flush data
                    // of the Cryptographic service provide. Best Practice
                    hashmd5.Clear();
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // Set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                // Mode of operation. there are other 4 modes.
                // We choose ECB (Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                // Padding mode (if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                // Transform the specified region of bytes array to resultArray
                byte[] resultArray =
                  cTransform.TransformFinalBlock(toEncryptArray, 0,
                  toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // Return the encrypted data into unreadable string format
                retVal = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                //throw new EncryptionException(EncryptionException.Code.EncryptionFailure, ex,MethodBase.GetCurrentMethod());
            }

            return retVal;
        }

        /// <summary>
        /// Decrypts a specified key against the original security
        /// key, with the option to hash.
        /// </summary>
        /// <param name="cipherString">String to decrypt</param>
        /// <param name="securityKey">The original security key</param>
        /// <param name="useHashing">Weather hashing is enabled</param>
        /// <returns>The decrypted key</returns>
        public static string Decrypt(string cipherString, string securityKey, bool useHashing)
        {
            string retVal = string.Empty;

            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                // Validate inputs
                ValidateInput(cipherString);
                ValidateInput(securityKey);

                if (useHashing)
                {
                    // If hashing was used get the hash code with regards to your key
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));

                    // Release any resource held by the MD5CryptoServiceProvider
                    hashmd5.Clear();
                }
                else
                {
                    // If hashing was not implemented get the byte code of the key
                    keyArray = UTF8Encoding.UTF8.GetBytes(securityKey);
                }

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                // Set the secret key for the tripleDES algorithm
                tdes.Key = keyArray;

                // Mode of operation. there are other 4 modes. 
                // We choose ECB(Electronic code Book)
                tdes.Mode = CipherMode.ECB;

                // Padding mode(if any extra byte added)
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                                     toEncryptArray, 0, toEncryptArray.Length);

                // Release resources held by TripleDes Encryptor
                tdes.Clear();

                // Return the Clear decrypted TEXT
                retVal = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
               // throw new EncryptionException(EncryptionException.Code.DecryptionFailure, ex, MethodBase.GetCurrentMethod());
            }

            return retVal;
        }
        /// <summary>
        /// Validates an input value
        /// </summary>
        /// <param name="inputValue">Specified input value</param>
        /// <returns>True | Falue - Value is valid</returns>
        private static bool ValidateInput(string inputValue)
        {
            bool valid = string.IsNullOrEmpty(inputValue);

            if (!valid)
            {
               // throw new EncryptionException(EncryptionException.Code.InvalidInputValues, MethodBase.GetCurrentMethod());
            }

            return valid;
        }
    }
}
