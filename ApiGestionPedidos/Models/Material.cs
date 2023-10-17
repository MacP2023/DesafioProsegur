using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGestionPedidos.Models
{
    public class Material
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaterialID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public int Estado { get; set; }

        //public virtual ICollection<Producto> Productos { get; set; }
        //public virtual ICollection<ProductoMaterial> ProductoMaterial { get; set; }
    }
}
