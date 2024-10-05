using System.Security.Cryptography;
using System.Text;

namespace Mobile404API.Extention
{
    public class PasswordHelper
    {
        public static string EncodePasswordMd5(string password)
        {
            byte[] originalBytes;
            byte[] encodedBytes;

            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}
