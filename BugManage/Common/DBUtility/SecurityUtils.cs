using System;
using System.Collections.Generic;
using System.IO;

using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Zelo.Common.DBUtility
{
    public static class SecurityUtils
    {
        private readonly static String PREFIX = "a49b0cASDFASDFASDF3a279a7e8ASDFASDFSDFA72a49b0cASDFA";
        private readonly static String AES_KEY = "2afd65e1SDFASDFA72a49b0cASDFASDFASDF3a279a7e8ASDFASDFAFedbff";
        private readonly static String AES_IV = "asdfasdfasdASDFA72a49b0cASDFASDFASDF3a279a7e8ASDFASDFAFedbff";
        /// <summary>
        /// md5二次加密
        /// </summary>
        /// <param name="prefix">种子</param>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static String MD5WithString(String prefix, String sourceStr)
        {

            byte[] result = MD5(prefix, sourceStr);
            StringBuilder sb = new StringBuilder();
            foreach (byte byteItem in result)
            {

                if (byteItem < (0x10))
                {
                    sb.Append("0");
                }
                sb.Append(byteItem.ToString("x"));
            }
            return sb.ToString();
        }

        public static Byte[] MD5(String prefix, String sourceStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(prefix);
            byte[] result = md5.ComputeHash(data);
            return result;
        }
       
        
        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String AESEncrypt(String str)
        {
            return AESEncrypt(str, PREFIX);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static String AESEncrypt( String str,String prefix)
        {
            byte[] key = new byte[32];
            byte[] keyFragment = MD5(PREFIX, AES_KEY);
            byte[] iv = MD5(prefix, AES_IV);

            byte[] data = EncryptStringToBytes_Aes(str, GetKey(keyFragment, iv), iv);
            if (data != null)
            {
                return byteArray2String(data);
            }
            return null;
        }




        private static byte[] GetKey(byte[] preByte,byte[] sufByte)
        {
            byte[] key = new byte[32];
             for(int i=0;i<key.Length;i++)
            {
                if(i<16)
                {
                    key[i]=preByte[i];
                }else{
                 
                    key[i]=sufByte[ i - 16];
                }
            }
             return key;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static String AESDecrypt(String Text, String prefix)
        {
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            byte[] key = new byte[32];
            byte[] keyFragment = MD5(prefix, AES_KEY);
            byte[] iv = MD5(prefix, AES_IV);
            return DecryptStringFromBytes_Aes(inputByteArray, GetKey(keyFragment, iv), iv);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static String AESDecrypt(String Text)
        {
            return AESDecrypt(Text, PREFIX);
        }


        public static String byteArray2String(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString(); 
        }

     
       
        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {

                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

    }
}