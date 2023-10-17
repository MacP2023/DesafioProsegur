using GestionDePedidosSB.Models;
using Microsoft.AspNetCore.Mvc;
namespace GestionDePedidosSB.Services
{
    public interface IServicesUsuario
    {
        Task<ActionResult<IEnumerable<ModUsuario>>> GetUsuario();
        Task<ActionResult<ModUsuario>> GetUsuario(int id);
        Task<ModUsuario> PostUsuario(ModUsuario usuario);
        Task<IActionResult> PutUsuario(int id, ModUsuario usuario);
        Task<ActionResult<ModUsuario>> DeleteUsuario(int id);
        Task<List<ModUsuario>> GetIniciarSession(string UserName, string clave);
    }
}
