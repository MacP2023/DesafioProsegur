using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace UtilidadesGenerales.Utilidades
{
    public enum TiposRol
    {
        Usuario,
        Empleado,
        Supervisor,
        Administrador
    }

    public enum Estado
    {
        [DescriptionAttribute("Registrada")]
        Registrada,
        [DescriptionAttribute("EnPreparacion")]
        EnPreparacion,
        [DescriptionAttribute("PorFacturar")]
        PorFacturar,
        [DescriptionAttribute("Activo")]
        Activo,
        [DescriptionAttribute("Inactivo")]
        Inactivo
    }
    public enum TipoMoneda
    {
        PEN,
        USD
      
    }

    public enum Provincia
    {
        [DescriptionAttribute("Lima")]
        Lima,
        [DescriptionAttribute("Callao")]
        Callao,
        [DescriptionAttribute("Arequipa")]
        Arequipa,
        [DescriptionAttribute("Trujillo")]
        Trujillo
    }
    public class Util
    {
        public static string EncriptarClave(string Clave)
        {

            StringBuilder sb = new StringBuilder();

            using (SHA256 sha256 = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] resultadoSha = sha256.ComputeHash(Encoding.UTF8.GetBytes(Clave));
                foreach (byte b in resultadoSha)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }

        public static DateTime ObtenerFechaEncurso()

        {
            
            DateTime FechaenCurso = DateTime.Today;

            return FechaenCurso; 
        
        }

        
        public static string ObtenerDescripcionProvincia( Provincia provincia)

        {
            var campo = provincia.GetType().GetField(provincia.ToString());
            var atributo = campo.GetCustomAttribute<DescriptionAttribute>();
            return atributo.Description;
        }

        public static string ObtenerDescripcionEstado(Estado estado)

        {
            var campo = estado.GetType().GetField(estado.ToString());
            var atributo = campo.GetCustomAttribute<DescriptionAttribute>();
            return atributo.Description;
        }

    

    }
}