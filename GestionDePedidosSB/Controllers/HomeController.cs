using UtilidadesGenerales.Utilidades;
using GestionDePedidosSB.Models;
using GestionDePedidosSB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace GestionDePedidosSB.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly IServicesUsuario _servicessuario;

        public HomeController(IServicesUsuario servicesUsuario)
        {
            _servicessuario = servicesUsuario;
        }



        public async Task<IActionResult> Inicio( string UserName,string Clave)
        {
           
            List<ModUsuario> Usuarios = await _servicessuario.GetIniciarSession(UserName, Clave);
            TiposRol Rol;
            string VistaRetornar="InicioSesion";
            string Controlador = "Home";
            if (Usuarios.Count()==0)
            {
                ViewData["Message"] = "Usuario no Registrado";
                return View(VistaRetornar);
            }
            await ObtenerInicio(Usuarios);
            Rol = (TiposRol)Usuarios[0].Rol; 
            HttpContext.Session.SetString("UsuarioRol", Usuarios[0].Rol.ToString());
            ViewBag.Rol = Convert.ToInt32(Rol);
            return View("Index");
           

        }

        public async Task<IActionResult> Index(string UserName, string Clave)
        {
            ViewBag.Rol=Convert.ToInt16( HttpContext.Session.GetString("UsuarioRol"));
            return View("Index");

        }

        public async Task<IActionResult> RegistrarUsuario()
        {
           
           
            ViewData["Titulo"] = "Registro de Usuario";
            ViewBag.Provincias = ObtenerProvincias();
            return View("RegistrarUsuario");
        }

        public async Task<IActionResult> GuardarUsuario(string UserName, string Nombre,string Apellido,int ProvinciaSelec, string Direccion,string Celular, string Clave)
        {
            ModUsuario usuarioresult = new();
            ModUsuario usuario = new()
            {
                UserName=UserName,
                Nombre=Nombre,  
                Apellido=Apellido, 
                DNI="",
                Provincia= ProvinciaSelec,
                Direccion=Direccion,
                Celular=Celular,
                Clave=Clave,
                FechaRegistro= Util.ObtenerFechaEncurso(),
                FechaUltimoAcceso = Util.ObtenerFechaEncurso(),
                Rol=0,
            };

            usuarioresult=await _servicessuario.PostUsuario(usuario);
            if (usuarioresult!=null)
                if (usuarioresult.UsuarioID>0)
                    return View("InicioSesion");

            ViewData["Mensaje"] = "No se pudo registrar el usuario";
            return View();

           
        }

            public async Task ObtenerInicio(List<ModUsuario> Usuarios)
            {
                List<Claim> ClaimListUsuario = new List<Claim>() { new Claim(ClaimTypes.Name, Usuarios[0].UserName), new Claim(ClaimTypes.NameIdentifier, Usuarios[0].UsuarioID.ToString()) };

                ClaimsIdentity ClaimsIdtentityUsuario = new ClaimsIdentity(ClaimListUsuario, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties PropiedadesAutenticacion = new AuthenticationProperties() { AllowRefresh = true };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdtentityUsuario), PropiedadesAutenticacion);
            
                ClaimsPrincipal ClaimUsuario = HttpContext.User;

                string NombreUsuario = "";
                int IDUsuario = 0;
                if (ClaimUsuario.Identity.IsAuthenticated)
                {
                    NombreUsuario = ClaimUsuario.Claims.Where(x => x.Type == ClaimTypes.Name).Select(y => y.Value).SingleOrDefault();
                    IDUsuario = Convert.ToInt32(ClaimUsuario.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(y => y.Value).SingleOrDefault());
                }
                HttpContext.Session.SetString("IDUsuario", IDUsuario.ToString());
                HttpContext.Session.SetString("NombreUsuario", NombreUsuario);         

            }

        public async Task<IActionResult> Principal()
        {
            return View("InicioSesion");
            //return View("Index");
            
        }

        
        public IActionResult InicioSesion()
        {
            return View("InicioSesion");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async  Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("InicioSesion", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<SelectListItem> ObtenerProvincias()
        {
            var provincias = new List<SelectListItem>();

            foreach( Provincia itemProvincia in Enum.GetValues(typeof(Provincia)))
            {
                var item = new SelectListItem
                {
                    Value = ((int)itemProvincia).ToString(),
                    Text = Util.ObtenerDescripcionProvincia(itemProvincia)
                };
                provincias.Add(item);

            }
            return provincias;  
        }
    }
}