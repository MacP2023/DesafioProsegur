using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiGestionPedidos.Context;
using ApiGestionPedidos.Models;
using Microsoft.Extensions.Hosting;
using UtilidadesGenerales.Utilidades;
using Microsoft.AspNetCore.Server.IIS.Core;
//using System.Data

namespace ApiGestionPedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionPedidosController : ControllerBase
    {
        private readonly GestionPedidosContext _context;


        public GestionPedidosController(GestionPedidosContext context)
        {
            _context = context;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("Material")]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterial()
        {
            return await _context.Materiales.ToListAsync();
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("Material/{MaterialID}")]
        public async Task<ActionResult<Material>> GetMaterial(int MaterialID)
        {
            var Material = await _context.Materiales.FindAsync(MaterialID);
            if (Material == null)
                return NotFound();

            return Material;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("Producto")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {

            return await _context.Productos.ToListAsync();
        }

        [HttpGet("Producto/{ProductoID}")]
        public async Task<ActionResult<Producto>> GetProducto(int ProductoID)
        {
            var Producto = await _context.Productos.FindAsync(ProductoID);
            if (Producto == null)
                return NotFound();

            return Producto;
        }

        [HttpGet("Producto/{ProductoID}/{MaterialID}")]
        public async Task<ActionResult<ProductoMaterial>> GetProductoMaterial(int ProductoID, int MaterialID)
        {
            var ProductosMaterial = await _context.ProductosMateriales.FindAsync(ProductoID, MaterialID);
            if (ProductosMaterial == null)
                return NotFound();
            return ProductosMaterial;
        }

        [HttpGet("ProductoUsuario/{UsuarioID}")]

        public async Task<ActionResult<List<Producto>>> GetProductoUsuario(int UsuarioID)
        {
            List<Producto> productoslistar = new();
            List<Producto> productosnolistar = new();
            List<Orden> ordenUsuario = new();
            List<OrdenDetalle> ordenDetalle = new();
          
            int ordenid = 0;

            productoslistar = _context.Productos.Where(x => x.Estado == 3).ToList();
            if (productoslistar.Count() == 0)
                return NotFound();
            else
            {
                productosnolistar = ObtenerProductosNoListar(productoslistar);
                productoslistar = productoslistar.Where(x => !productosnolistar.Contains(x)).ToList();

                if (productoslistar.Count() == 0)
                    return NotFound();
            }
           

            return productoslistar;
        }



        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenAll")]
        public async Task<ActionResult<List<Orden>>> GetOrden()
        {
            return await _context.Ordenes.ToListAsync();
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("Orden/{ordenid}")]
        public async Task<ActionResult<Orden>> GetOrdenxID(int ordenid)
        {
            OrdenDetalle ordendetalle = new();
            Orden orden= new();
            List <OrdenDetalle> listordendetalle = new();
            orden= await _context.Ordenes.FindAsync(ordenid);
            if (orden == null)
                return NotFound();
            else
            {
                    listordendetalle = await _context.OrdenDetalles.Where(x => x.OrdenID == ordenid).ToListAsync();
                    orden.OrdenDetalle = listordendetalle;
             
            }

            return orden;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenUsuario/{usuarioid}")]
        public async Task<ActionResult<List<Orden>>> GetOrdenUsuario(int usuarioid)
        {
            OrdenDetalle ordendetalle = new();
         
            List<OrdenDetalle> listordendetalle = new();
            List<Orden> ordenes = await _context.Ordenes.Where(x => x.ClientID == usuarioid).ToListAsync();
           
            if (ordenes == null)
                return NotFound();
            else
            {
                foreach (var orden in ordenes)
                {
                    listordendetalle = await _context.OrdenDetalles.Where(x => x.OrdenID == orden.OrdenID).ToListAsync();
                    orden.OrdenDetalle = listordendetalle;
                }
            }

            return ordenes;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenDetalle/{UsuarioID}")]
        public async Task<ActionResult<List<OrdenDetalle>>> GetDetalleOrdenUsuario(int UsuarioID)
        {
            List<Producto> productoslistar = new();
            List<Orden> ordenUsuario = new();
            List<OrdenDetalle> ordenDetalle = new();
            int ordenid = 0;
            ordenUsuario = _context.Ordenes.ToList().Where(x => x.ClientID == UsuarioID && x.Estado != Convert.ToInt32(Estado.PorFacturar)).ToList();
            ordenid = ordenUsuario[0].OrdenID;

            ordenDetalle = ObtenerDetalleOrden(ordenid);
            if (ordenDetalle.Count()==0)
                return NotFound();
            
            return ordenDetalle;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenDetalle")]
        public async Task<ActionResult<IEnumerable<OrdenDetalle>>> GetOrdenDetalle()
        {
            return await _context.OrdenDetalles.ToListAsync();
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenDetalle/{OrdenID}")]
        public async Task<ActionResult<OrdenDetalle>> GetOrdenDetalle(int OrdenID)
        {
            var OrdenDetalle = await _context.OrdenDetalles.FindAsync(OrdenID);
            if (OrdenDetalle == null)
                return NotFound();

            return OrdenDetalle;
        }

        // GET: api/<GestionPedidosController>
        [HttpGet("OrdenAgotados/{orden}")]
        private  async Task<List<Producto>> ObtenerProductosAgotadosOrden(Orden orden)
        {
            List<Producto> productosagotadoslistar = new();
            List<Producto> productosnolistar = new();

            productosnolistar = ObtenerProductosNoListarOrden(orden);
            var query = from producto in productosnolistar
                        join productosorden in orden.OrdenDetalle on producto.ProductoID equals productosorden.ProductoID
                        select new Producto
                        {
                            ProductoID = producto.ProductoID,
                            Descripcion = producto.Descripcion,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Observacion = producto.Observacion,
                            TipoMoneda = producto.TipoMoneda,
                            Estado = producto.Estado
                        };
            return productosagotadoslistar;
        }

        // POST api/<GestionPedidosController>
        [HttpPost("Material")]
        public async Task<ActionResult<Material>> PostMateriales(Material material)
        {
            _context.Add(material);
            if (!Existematerial(material.MaterialID))
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetMaterial", new { id = material.MaterialID }, material);

        }


        // POST api/<GestionPedidosController>
        [HttpPost("Producto")]
        public async Task<ActionResult<Material>> PostProducto(Producto producto)
        {
            _context.Add(producto);
            if (!Existeproducto(producto.ProductoID))
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetProducto", new { id = producto.ProductoID }, producto);
        }

        // POST api/<GestionPedidosController>
        [HttpPost("ProductoMaterial")]
        public async Task<ActionResult<ProductoMaterial>> PostProductoMaterial(ProductoMaterial productoMaterial)
        {
            _context.Add(productoMaterial);
            if (!Existeproductomaterial(productoMaterial.ProductoID, productoMaterial.MeterialID))
            {
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetProductoMaterial", new { ProductoID = productoMaterial.ProductoID, MaterialID = productoMaterial.MeterialID }, productoMaterial);
        }

        // POST api/<GestionPedidosController>
        [HttpPost("Orden")]
          public async Task<ActionResult<Orden>> PostOrden(Orden orden)
      
            {
               
               _context.Add(orden);
               var response= await _context.SaveChangesAsync();
             
               return CreatedAtAction("GetOrden", new { id = orden.OrdenID }, orden);
            }

        // PUT api/<GestionPedidosController>/5
        [HttpPut("Orden")]
        public async Task<IActionResult> PutMaterial(Material material)
        {
            _context.Update(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterial", new { MaterialID = material.MaterialID }, material);
        }

        // DELETE api/<GestionPedidosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private List<Producto> ObtenerProductosNoListar(List<Producto> productoslistar)
        {
            List<ProductoMaterial> productosmateriales = new();
            List<Material> materiales = new();
            List<Producto> productosnolistar = new();

            productosmateriales = _context.ProductosMateriales.ToList();
            materiales = _context.Materiales.ToList();

            var query = from producto in productoslistar
                        join prodmat in productosmateriales on producto.ProductoID equals prodmat.ProductoID
                        join material in materiales on prodmat.MeterialID equals material.MaterialID
                        where (prodmat.CantidadPorMaterial > material.Cantidad)

                        select new Producto
                        {
                            ProductoID = producto.ProductoID,
                            Descripcion = producto.Descripcion,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Observacion = producto.Observacion,
                            TipoMoneda = producto.TipoMoneda,
                            Estado = producto.Estado
                        };
            productosnolistar = query.Distinct().ToList();


            return productosnolistar;
        }

        private List<Producto> ObtenerProductosNoListarOrden(Orden orden)
        {
            List<ProductoMaterial> productosmateriales = new();
            List<Material> materiales = new();
            List<Producto> productosnolistar = new();
            List<Producto> productoslistar = new();

            productoslistar = _context.Productos.Where(x => x.Estado == 3).ToList();
            productosmateriales = _context.ProductosMateriales.ToList();
            materiales = _context.Materiales.ToList();

            var query = from producto in productoslistar
                        join detalleorden in orden.OrdenDetalle on producto.ProductoID equals detalleorden.ProductoID
                        join prodmat in productosmateriales on producto.ProductoID equals prodmat.ProductoID
                        join material in materiales on prodmat.MeterialID equals material.MaterialID
                        where (prodmat.CantidadPorMaterial*detalleorden.Cantidad > material.Cantidad)

                        select new Producto
                        {
                            ProductoID = producto.ProductoID,
                            Descripcion = producto.Descripcion,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Observacion = producto.Observacion,
                            TipoMoneda = producto.TipoMoneda,
                            Estado = producto.Estado
                        };
            productosnolistar = query.Distinct().ToList();


            return productosnolistar;
        }
        private List<Producto> ObtenerProductosListarxOrden(int ordenid, List<Producto> productoslistar)
        {
            List<Producto> productoslistarorden = new();
            List<Orden> ordenUsuario = new();
            List<OrdenDetalle> ordenDetalle = new();
            ordenDetalle= _context.OrdenDetalles.ToList();
            ordenDetalle = ordenDetalle.Where(x => x.OrdenID == ordenid).ToList();
            var query = from producto in productoslistar
                        join detalle in ordenDetalle on producto.ProductoID equals detalle.ProductoID
                        select new Producto
                        {
                            ProductoID = producto.ProductoID,
                            Descripcion = producto.Descripcion,
                            Nombre = producto.Nombre,
                            Precio = producto.Precio,
                            Observacion = producto.Observacion,
                            TipoMoneda = producto.TipoMoneda,
                            Estado = producto.Estado
                        };
            productoslistarorden = query.ToList();
            return productoslistarorden;
        }

        private List<OrdenDetalle> ObtenerDetalleOrden(int ordenid)
        {
            List<Producto> productoslistar = new();
            List<Orden> ordenUsuario = new();
            List<OrdenDetalle> ordenDetalle = new();
            productoslistar = _context.Productos.ToList();
            ordenDetalle = ordenDetalle.Where(x => x.OrdenID == ordenid).ToList();
            var query = from producto in productoslistar
                        join detalle in ordenDetalle on producto.ProductoID equals detalle.ProductoID
                        select new OrdenDetalle
                        {
                            OrdenID = detalle.OrdenID,
                            DetalleOrdenID = detalle.DetalleOrdenID,
                            ProductoID = producto.ProductoID,
                            Cantidad = detalle.Cantidad,
                            ObservacionOrden = "",                    
                        };
            ordenDetalle = query.ToList();
            return ordenDetalle;
        }

        private bool Existematerial(int materialid)
        {
            return _context.Materiales.Any(x => x.MaterialID == materialid);
        }

        private bool Existeproducto(int productoid)
        {
            return _context.Productos.Any(x => x.ProductoID == productoid);
        }

        private bool Existeproductomaterial(int producto, int material)
        {
            return _context.ProductosMateriales.Any(x => x.ProductoID == producto && x.MeterialID== material);
        }

    }
}
