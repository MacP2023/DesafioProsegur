using NUnit.Framework;
using GestionDePedidosSB;
using ApiGestionPedidos.Context;
using ApiGestionPedidos.Models;
using ApiUsuarios.Context;
using ApiUsuarios.Models;
using UtilidadesGenerales.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GestionDePedidosSB.Services;
using ApiGestionPedidos.Controllers;
using GestionDePedidosSB.Controllers;
using GestionDePedidosSB.Models;

namespace GestionDePedidosSB_Test
{
    internal class ServicesOrden_Test
    {
        private ApiGestionPedidos.Models.Producto producto;
        private ApiGestionPedidos.Models.Material material;
        private ApiGestionPedidos.Models.ProductoMaterial productoMaterial;
        private Usuario usuario;
        private GestionDePedidosSB.Models.Orden orden;
        private GestionDePedidosSB.Models.OrdenDetalle ordenDetalle;
        private List<ApiGestionPedidos.Models.Material> materialList;
        private List<GestionDePedidosSB.Models.OrdenDetalle> ordenDetalleList;
        private List<ApiGestionPedidos.Models.Producto> productoList;
        private List<ApiGestionPedidos.Models.ProductoMaterial> productoMaterialList;
        [SetUp]
        public void Setup()
        {
            usuario = new Usuario()
            {
                UsuarioID = 1,
                UserName = "AngelaM",
                Nombre = "Angela",
                Apellido = "Marin",
                DNI = "343333",
                Provincia = 0,
                Direccion = "dire1",
                Celular = "1233",
                Clave = "123",
                FechaRegistro = Util.ObtenerFechaEncurso(),
                FechaUltimoAcceso = Util.ObtenerFechaEncurso(),
                Rol = 0
            };
            materialList = new List<ApiGestionPedidos.Models.Material>()
            {
                new ApiGestionPedidos.Models.Material() {
                    MaterialID = 1,
                    Nombre = "Leche Completa",
                    Descripcion = "Leche Completa",
                    Cantidad = 5,
                    UnidadMedida = "ml",
                    Estado = 3
                },
                new ApiGestionPedidos.Models.Material()
                {
                    MaterialID = 2,
                    Nombre = "Cafe Gourmet",
                    Descripcion = "Cafe Gourmet",
                    Cantidad = 5,
                    UnidadMedida = "gr",
                    Estado = 3

                },
                  new ApiGestionPedidos.Models.Material()
                {
                    MaterialID = 3,
                    Nombre = "Chocolate",
                    Descripcion = "Chocolate",
                    Cantidad = 5,
                    UnidadMedida = "kl",
                    Estado = 3

                }

            };
            productoList = new List<ApiGestionPedidos.Models.Producto>()
            {

                new ApiGestionPedidos.Models.Producto()
                {
                    ProductoID = 1,
                    Nombre = "Cafe Capuchino",
                    Descripcion = "Cafe Capuchino tiene crema",
                    Precio = 10,
                    TipoMoneda = 1,
                    Observacion = "Bebida Caliente",
                    Estado = 3
                },
                new ApiGestionPedidos.Models.Producto()
                {
                    ProductoID = 2,
                    Nombre = "Cafe Mokachino",
                    Descripcion = "Cafe Mokachino tiene chocolate",
                    Precio = 12,
                    TipoMoneda = 1,
                    Observacion = "Bebida Caliente",
                    Estado = 3
                }
            };


            productoMaterialList = new List<ApiGestionPedidos.Models.ProductoMaterial>()
            {
                    new ApiGestionPedidos.Models.ProductoMaterial()
                    {
                        MeterialID = 1,
                        ProductoID = 1,
                        CantidadPorMaterial = 250
                    }
                    ,
                    new ApiGestionPedidos.Models.ProductoMaterial()
                    {
                        MeterialID = 1,
                        ProductoID = 2,
                        CantidadPorMaterial = 250
                    },
                    new ApiGestionPedidos.Models.ProductoMaterial()
                    {
                        MeterialID = 2,
                        ProductoID = 1,
                        CantidadPorMaterial = 10
                    },
                    new ApiGestionPedidos.Models.ProductoMaterial()
                    {
                        MeterialID = 2,
                        ProductoID = 2,
                        CantidadPorMaterial = 10
                    },
                    new ApiGestionPedidos.Models.ProductoMaterial()
                    {
                        MeterialID = 3,
                        ProductoID = 2,
                        CantidadPorMaterial = 10
                    }
            };
            ordenDetalleList = new();

            foreach (ApiGestionPedidos.Models.Producto item in productoList)
            {
                ordenDetalleList.Add(new GestionDePedidosSB.Models.OrdenDetalle()
                {
                    ProductoID = item.ProductoID,
                    Cantidad = 1,
                    ObservacionOrden = ""

                });


            }


            orden = new GestionDePedidosSB.Models.Orden()
            {
                ClientID = usuario.UsuarioID,
                Estado = Convert.ToInt16(Estado.Registrada),
                FechaOrden = Util.ObtenerFechaEncurso(),
                OrdenDetalle = ordenDetalleList
            };
        }

        private IServiceProvider CrearContextOrden()
        {
            var servicio = new ServiceCollection();
            servicio.AddDbContext<GestionPedidosContext>(options => options.UseInMemoryDatabase("GestionPedidosSB"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            return servicio.BuildServiceProvider();
        }
        private IServiceProvider CrearContextUsuario()
        {
            var servicio = new ServiceCollection();
            servicio.AddDbContext<UsuarioContext>(options => options.UseInMemoryDatabase("GestionPedidosSB"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            return servicio.BuildServiceProvider();
        }
        [Test]
        public async Task Test1()
        {
            //Arrange
           // GestionDePedidosSB.Models.Orden ordengp;
            var serviceproviderUsuario = CrearContextUsuario();
            var dbUsuario = serviceproviderUsuario.GetService<UsuarioContext>();
            var serviceproviderOrden = CrearContextOrden();
            var dbOrden = serviceproviderOrden.GetService<GestionPedidosContext>();
            dbUsuario.Usuarios.Add(usuario);
            dbOrden.Materiales.AddRange(materialList);
            dbOrden.Productos.AddRange(productoList);
            dbOrden.ProductosMateriales.AddRange(productoMaterialList);
            // ApiGestionPedidos.Models.Orden OrdenGP = (ApiGestionPedidos.Models.Orden)orden;
            //dbOrden.Ordenes.Add(orden);
           
            //Act
            ServicesOrden servicesOrden = new ServicesOrden();
           // OrdenController ordencontroller = new OrdenController(servicesOrden);
            // ServicesOrden servicesOrden= new ServicesOrden();
            var respuesta = await servicesOrden.PostOrden(orden);
            Assert.Pass();

        }
    }
}
