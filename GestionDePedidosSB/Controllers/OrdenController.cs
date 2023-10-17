using UtilidadesGenerales.Utilidades;
using GestionDePedidosSB.Models;
using GestionDePedidosSB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Web;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Drawing;

namespace GestionDePedidosSB.Controllers
{
    public class OrdenController : Controller
    {
        private readonly IServiciesOrden _servicesorden;

       
        public OrdenController(IServiciesOrden servicesorden)
        {
            _servicesorden = servicesorden;
        }
        public async Task CargarInicial()
        {
           
            Material material;
            Producto producto;
            ProductoMaterial productomaterial;

            material =new Material() {
                MaterialID = 1,
                Nombre = "Leche Completa",
                Descripcion = "Leche Completa",
                Cantidad = 5,
                UnidadMedida = "ml",
                Estado = 3
            };
            await _servicesorden.PostMaterial(material);
            material = new Material()
            {
                MaterialID = 2,
                Nombre = "Cafe Gourmet",
                Descripcion = "Cafe Gourmet",
                Cantidad = 5,
                UnidadMedida = "gr",
                Estado = 3

            };
            await _servicesorden.PostMaterial(material);
            material = new Material()
            {
                MaterialID = 3,
                Nombre = "Chocolate",
                Descripcion = "Chocolate",
                Cantidad = 5,
                UnidadMedida = "kl",
                Estado = 3

            };
            await _servicesorden.PostMaterial(material);


            producto = new Producto()
            {
                ProductoID = 1,
                Nombre = "Cafe Capuchino",
                Descripcion = "Cafe Capuchino tiene crema",
                Precio = 10,
                TipoMoneda = 1,
                Observacion = "Bebida Caliente",
                Estado = 3
            };
            await _servicesorden.PostProducto(producto);
            producto = new Producto()
            {
                ProductoID = 2,
                Nombre = "Cafe Mokachino",
                Descripcion = "Cafe Mokachino tiene chocolate",
                Precio = 12,
                TipoMoneda = 1,
                Observacion = "Bebida Caliente",
                Estado = 3
            };
            await _servicesorden.PostProducto(producto);

           
                productomaterial = new ProductoMaterial()
                {
                    MeterialID = 1,
                    ProductoID = 1,
                    CantidadPorMaterial = 250
                };
                await _servicesorden.PostProductoMaterial(productomaterial);

            productomaterial = new ProductoMaterial()
            {
                MeterialID = 1,
                ProductoID = 2,
                CantidadPorMaterial = 250
            };
            await _servicesorden.PostProductoMaterial(productomaterial);

            productomaterial = new ProductoMaterial()
            {
                MeterialID = 2,
                ProductoID = 1,
                CantidadPorMaterial = 10
            };
            await _servicesorden.PostProductoMaterial(productomaterial);
            productomaterial = new ProductoMaterial()
            {
                MeterialID = 2,
                ProductoID = 2,
                CantidadPorMaterial = 10
            };
            await _servicesorden.PostProductoMaterial(productomaterial);
            productomaterial = new ProductoMaterial()
            {
                MeterialID = 3,
                ProductoID = 2,
                CantidadPorMaterial = 10
            };
            await _servicesorden.PostProductoMaterial(productomaterial);
           


        }

        public async Task<IActionResult> RegistrarOrden()
        {
            await CargarInicial();
            int UsuarioID = Convert.ToInt32(HttpContext.Session.GetString("IDUsuario"));
            List<Producto> ListaUsuario = await _servicesorden.GetProductoUsuario(UsuarioID);
            ViewBag.ListaUsuario = new List<Producto>();
            ViewBag.ListaUsuario = ListaUsuario;
            ViewData["NombreUsuario"] = HttpContext.Session.GetString("NombreUsuario"); 
            
            return View("RegistrarOrden");

        }

        public async Task<IActionResult> GuardarOrden(int[] ProductoID,int[] Cantidad)
        {
          
            List<OrdenDetalle> listordenDetalles = new();
         
            Orden orden = new()
            {
                ClientID = Convert.ToInt32(HttpContext.Session.GetString("IDUsuario")),
                FechaOrden = Util.ObtenerFechaEncurso(),
                Estado = Convert.ToInt16(Estado.Registrada)
            };
            foreach (var item in  ProductoID)
            {
                listordenDetalles.Add(new OrdenDetalle
                {
                    ProductoID = item,
                    Cantidad = Cantidad[Array.IndexOf(ProductoID, item)],
                    ObservacionOrden = ""
                });
            
            }
            orden.OrdenDetalle=listordenDetalles;

            orden = await _servicesorden.PostOrden(orden);
            ViewBag.Rol = HttpContext.Session.GetString("UsuarioRol");
           
            return RedirectToAction("Index", "Home");
       

        }
        public async Task<IActionResult> VerOrdenes()
        {
            List<ServicesOrden.OrdeneRegistrada> ListadoOrdenRegistradas = new();
            
            List<Orden> Orden = await _servicesorden.GetAllOrden();
           
            ServicesOrden.OrdeneRegistrada OrdenRegistrada = new();
            foreach (var orden in Orden)
            {
                OrdenRegistrada = await _servicesorden.GetUsuarioOrden(orden);
                ListadoOrdenRegistradas.Add(OrdenRegistrada);
            }

            ViewBag.ListadoOrdenRegistradas = new List<ServicesOrden.OrdeneRegistrada>();
            ViewBag.ListadoOrdenRegistradas = ListadoOrdenRegistradas;
            ViewBag.Rol= Convert.ToInt16(HttpContext.Session.GetString("UsuarioRol"));
            return View("VerOrdenes");

        }
       
        public async Task<IActionResult> VerOrdenesxUsuario()
        {
            List<ServicesOrden.OrdeneRegistrada> ListadoOrdenRegistradas = new();
            int usuarioid = Convert.ToInt32(HttpContext.Session.GetString("IDUsuario"));
            List<Orden> Orden = await _servicesorden.GetOrdenxUsuario(usuarioid);
          
            ServicesOrden.OrdeneRegistrada OrdenRegistrada = new();
            foreach (var orden in Orden)
            {
                OrdenRegistrada = await _servicesorden.GetUsuarioOrden(orden);
                ListadoOrdenRegistradas.Add(OrdenRegistrada);
            }

            ViewBag.ListadoOrdenRegistradas = new List<ServicesOrden.OrdeneRegistrada>();
            ViewBag.ListadoOrdenRegistradas = ListadoOrdenRegistradas;
            return View("VerOrdenes");

        }
        public async Task<IActionResult> VerDetalleOrden(int ordenid)
        {
            int usuarioid = Convert.ToInt32(HttpContext.Session.GetString("IDUsuario"));
            Orden Orden = await _servicesorden.GetOrden(ordenid);
            List<OrdenDetalle> ordenDetalle = Orden.OrdenDetalle;//await _servicesorden.GetDeralleOrdenXUsuario(UsuarioID);
            ViewBag.Orden = Orden;
           ViewBag.ordenDetalle = new List<OrdenDetalle>();
            ViewBag.ordenDetalle = ordenDetalle;
            ViewData["NombreUsuario"] = HttpContext.Session.GetString("NombreUsuario");
            ViewBag.UsuarioID = usuarioid;
            return View("VerDetalleOrden");

        }
        public IActionResult Index()
        {
            return View();
        }
     
    }
}
