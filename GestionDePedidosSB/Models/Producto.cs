using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDePedidosSB.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public int TipoMoneda { get; set; }
        //public int ProvinciaImpuesto { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
    }
}
