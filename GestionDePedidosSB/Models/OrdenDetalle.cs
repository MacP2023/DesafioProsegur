using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDePedidosSB.Models
{
    public class OrdenDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleOrdenID { get; set; }

        //[ForeignKey("OrdenID")]
        public int OrdenID { get; set; }
       // public Orden Orden { get; set; }
        public int ProductoID { get; set; }
        //[ForeignKey("ProductoID")]
       // public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public string ObservacionOrden { get; set; }
    }
}
