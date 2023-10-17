using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGestionPedidos.Models
{
    public class Producto
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int TipoMoneda { get; set; }
      //  public int ProvinciaImpuesto { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }

        //public virtual ICollection<Material> Materiales { get; set; }
        //public virtual ICollection<ProductoMaterial> ProductoMaterial { get; set; }

    }
}
