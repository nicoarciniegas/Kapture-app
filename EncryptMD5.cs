using System.Text; //Clases para codificar y decodificar cadenas de caracteres
using System.Threading.Tasks; //Clases para trabajar con tareas asincrónicas
using System.Security.Cryptography; //Clases para cifrar y descifrar datos

namespace Prodygytek_Kapture_App
{
    class EncryptMD5
    {
        private const string HashKey = "JPAspb42*";
        public String Encrypt (string message)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(message);

            MD5 md5= MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HashKey));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public String Decrypt(string Encryptedmessage)
        {
            byte[] data = Convert.FromBase64String(Encryptedmessage);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HashKey));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
