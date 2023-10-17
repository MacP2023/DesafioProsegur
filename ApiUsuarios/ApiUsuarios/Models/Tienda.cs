using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiUsuarios.Models
{
    public class Tienda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        int TiendaID { get; set; }
        string Descripcion { get; set; }
        string Direccion { get; set; }
    }
}
