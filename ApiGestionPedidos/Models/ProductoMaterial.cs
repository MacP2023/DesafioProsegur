using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGestionPedidos.Models
{
    public class ProductoMaterial
    {
        //[Key]
        //[Column(Order = 2)]
        public int MeterialID { get; set; }

        //[Key]
        //[Column(Order = 1)]
        public int ProductoID { get; set; }
        //public Producto Producto { get; set; }
        //// [ForeignKey("MaterialID")]
        //public int MaterialID { get; set; }
        //public Material  Material { get; set; }

       // [ForeignKey("ProductoID")]
        

        public int CantidadPorMaterial { get; set; }
    }
}
