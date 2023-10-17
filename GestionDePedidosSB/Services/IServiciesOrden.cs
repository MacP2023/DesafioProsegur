using GestionDePedidosSB.Models;
using Microsoft.AspNetCore.Mvc;
using static GestionDePedidosSB.Services.ServicesOrden;

namespace GestionDePedidosSB.Services
{
    public interface IServiciesOrden
    {
        Task<List<Orden>> GetAllOrden();
        Task<Orden> GetOrden(int ordenid);
        Task<List<Orden>> GetOrdenxUsuario(int usuarioid);
        Task<List<OrdenDetalle>> GetDeralleOrdenXUsuario(int usuarioid);
        Task<OrdeneRegistrada> GetUsuarioOrden(Orden orden);
        Task<Orden> PostOrden(Orden orden);
        Task<IActionResult> PutOrden(int id, Orden orden);
        Task<List<Producto>> GetProducto();
        Task<Producto> GetProducto(int id);
        Task<List<Producto>> GetProductoUsuario(int UsuarioID);
        Task<Material> PostMaterial(Material material);
        Task<Producto> PostProducto(Producto prodcuto);
        Task<ProductoMaterial> PostProductoMaterial(ProductoMaterial prodcutomaterial);
        Task<IActionResult> PutMaterial(Material material);
    }
}
