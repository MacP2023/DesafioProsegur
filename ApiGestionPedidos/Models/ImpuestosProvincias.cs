using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ApiGestionPedidos.Models
{
    public class ImpuestosProvincias
    {
        [Key]
        public int ImpuestoProvinciaID { get; set; }
        public decimal Impuesto { get; set; }
    }
}
