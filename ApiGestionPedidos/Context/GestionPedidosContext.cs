using UtilidadesGenerales.Utilidades;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApiGestionPedidos.Models;

namespace ApiGestionPedidos.Context
{
    public class GestionPedidosContext:DbContext
    {
        public GestionPedidosContext(DbContextOptions<GestionPedidosContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<ProductoMaterial>().HasKey(x => new { x.MeterialID, x.ProductoID });
            base.OnModelCreating(modelBuilder);
           
        }

        public DbSet<Material> Materiales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }

        public DbSet<ProductoMaterial> ProductosMateriales { get; set; }

        
        //public DbSet<EstadoOrden> EstadosOrden { get; set; }

        //public DbSet<OrdenEmpleado> OrdenesEmpleado { get; set; }

    }
}
