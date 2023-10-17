using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UtilidadesGenerales.Utilidades;

namespace GestionDePedidosSB.Models
{
    public class ModUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public int Provincia { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public string Clave { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public int Rol { get; set; }
    }
}
