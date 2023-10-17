using System.Security.Cryptography;
using System.Text;

namespace GestionDePedidosSB.Utilitarios
{
    public class Util
    {
        public static string  EncriptarClave(string clave)
        {

            StringBuilder sb = new StringBuilder();

            using(SHA256 sha256 = SHA256.Create()) 
            {
                Encoding encoding = Encoding.UTF8;
                byte[] resultadoSha= sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                foreach(byte b in resultadoSha)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
