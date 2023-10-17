
using GestionDePedidosSB.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestionDePedidosSB.Models.ViewModels
{
    public class RegistrarOrden:PageModel
    {
        public Orden OrdenV { get; set; }

        public List<OrdenDetalle> DetalleOrdenV { get; set; }

    }
}
