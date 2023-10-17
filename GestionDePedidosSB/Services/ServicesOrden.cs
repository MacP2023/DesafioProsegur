using GestionDePedidosSB.Services;
using GestionDePedidosSB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using UtilidadesGenerales.Utilidades;
using static GestionDePedidosSB.Services.ServicesOrden;

namespace GestionDePedidosSB.Services
{


    public class ServicesOrden: IServiciesOrden
    {
        private static string UrlBase = "";
        private static string UrlBaseUsuario = "";

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly HttpClient _httpClientUsu = new HttpClient();
        public ServicesOrden()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json").Build();
            UrlBase = "http://localhost:5208/api/";
            UrlBaseUsuario = "http://localhost:5132/api/";
            _httpClient = new();
            _httpClient.BaseAddress = new(UrlBase);
            _httpClientUsu = new();
            _httpClientUsu.BaseAddress = new(UrlBaseUsuario);
        }

        public class OrdeneRegistrada
        {
            public int OrdenID { get; set; }
            public string FechaOrden { get; set; }

            public string NombreCliente { get; set; }

            public int EstadoID { get; set; }
            public string Estado { get; set; }
        }
        public async Task<Orden> GetOrden(int ordenid)
        {
            Orden orden = new();

            StringContent content = new(JsonConvert.SerializeObject(orden), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/OrdenUsuario");

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                orden = JsonConvert.DeserializeObject<Orden>(Respuesta);
                return orden;
            }
            else
                return await Task.FromResult(orden);
        }

        public async Task<List<Orden>> GetAllOrden()
        {
            List<Orden> orden = new();

            StringContent content = new(JsonConvert.SerializeObject(orden), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/OrdenAll");

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                orden = JsonConvert.DeserializeObject<List<Orden>>(Respuesta);
                return orden;
            }
            else
                return await Task.FromResult(orden);
        }

        //public async Task<List<Orden>> GetOrdenxID(int ordenid)
        //{
        //    List<Orden> orden = new();

        //    StringContent content = new(JsonConvert.SerializeObject(orden), Encoding.UTF8, "Application/json");
        //    HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/Orden");

        //    if (Resultado.IsSuccessStatusCode)
        //    {
        //        string Respuesta = await Resultado.Content.ReadAsStringAsync();
        //        orden = JsonConvert.DeserializeObject<List<Orden>>(Respuesta);
        //        return orden;
        //    }
        //    else
        //        return await Task.FromResult(orden);
        //}



        public async Task<List<Orden>> GetOrdenxUsuario(int usuarioid)
        {
            List<Orden> orden = new();
            StringContent content = new(JsonConvert.SerializeObject(orden), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/OrdenUsuario/" + usuarioid);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                orden = JsonConvert.DeserializeObject<List<Orden>>(Respuesta);
                return orden;
            }
            else
                return await Task.FromResult(orden);
        }

        public async Task<OrdeneRegistrada> GetUsuarioOrden(Orden orden)
        {
            
            //ModUsuario usuario = new();
            OrdeneRegistrada ordenregistrada = new();
            //int UsuarioID = orden.ClientID;
            //StringContent content = new(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "Application/json");
            //HttpResponseMessage Resultado = await _httpClientUsu.GetAsync("Usuario/" + UsuarioID);

            //if (Resultado.IsSuccessStatusCode)
            //{
            //    //string Respuesta = await Resultado.Content.ReadAsStringAsync();
            //    //usuario = JsonConvert.DeserializeObject<ModUsuario>(Respuesta);

                ordenregistrada.OrdenID = orden.OrdenID;
                ordenregistrada.FechaOrden = orden.FechaOrden.ToString();
                ordenregistrada.NombreCliente = orden.ClientID.ToString();
                ordenregistrada.EstadoID = orden.Estado;
                ordenregistrada.Estado = Util.ObtenerDescripcionEstado((Estado)orden.Estado);
                
                return ordenregistrada;
            //}
            //else
            //    return await Task.FromResult(ordenregistrada);
        }


        public async  Task<List<OrdenDetalle>> GetDeralleOrdenXUsuario(int usuarioid)
        {
            List<OrdenDetalle> ordendetalle = new();

            StringContent content = new(JsonConvert.SerializeObject(ordendetalle), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/OrdenDetalle");

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                ordendetalle = JsonConvert.DeserializeObject<List<OrdenDetalle>>(Respuesta);
                return ordendetalle;
            }
            else
                return await Task.FromResult(ordendetalle);
        }

        public async Task<Orden> PostOrden(Orden orden)
        {
            Orden ordencreada = new ();
            List<Producto> productoslistar = new();
            List<Producto> productosagotadoslistar = new();
          
                    StringContent content1 = new(JsonConvert.SerializeObject(orden), Encoding.UTF8, "Application/json");
                    HttpResponseMessage Resultado = await _httpClient.PostAsync("GestionPedidos/Orden", content1);

                    if (Resultado.IsSuccessStatusCode)
                    {
                        string Respuesta = await Resultado.Content.ReadAsStringAsync();
                        ordencreada = JsonConvert.DeserializeObject<Orden>(Respuesta);
                        return ordencreada;
                    }
                    else
                        throw new Exception("Error al crear la Orden");

        }

        public async Task<Material> PostMaterial(Material material)
        {
            Material materialcreado = new();
            StringContent content1 = new(JsonConvert.SerializeObject(material), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.PostAsync("GestionPedidos/Material", content1);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                materialcreado = JsonConvert.DeserializeObject<Material>(Respuesta);
                return materialcreado;
            }
            else
                throw new Exception("Error al crear la Orden");

        }

        public async Task<Producto> PostProducto(Producto prodcuto)
        {
            Producto productocreado = new();
            StringContent content = new(JsonConvert.SerializeObject(prodcuto), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.PostAsync("GestionPedidos/Producto", content);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                productocreado = JsonConvert.DeserializeObject<Producto>(Respuesta);
                return productocreado;
            }
            else
                throw new Exception("Error al crear la Orden");

        }

        public async Task<ProductoMaterial> PostProductoMaterial(ProductoMaterial prodcutomaterial)
        {
            ProductoMaterial productomaterialcreado = new();
            StringContent content = new(JsonConvert.SerializeObject(prodcutomaterial), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.PostAsync("GestionPedidos/ProductoMaterial", content);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                productomaterialcreado = JsonConvert.DeserializeObject<ProductoMaterial>(Respuesta);
                return productomaterialcreado;
            }
            else
                throw new Exception("Error al crear la Orden");

        }

        public  Task<IActionResult> PutOrden(int id, Orden orden)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> GetProducto()
        {
            List<Producto> productos = new();

            StringContent content = new(JsonConvert.SerializeObject(productos), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/Producto");

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                productos = JsonConvert.DeserializeObject<List<Producto>>(Respuesta);
                return productos;
            }
            else
                return await Task.FromResult(productos);
           
        }

        public async Task<List<Producto>> GetProductoUsuario(int UsuarioID)
        {
            List<Producto> productos = new();
            
            StringContent content = new(JsonConvert.SerializeObject(productos), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/ProductoUsuario/" + UsuarioID);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                productos = JsonConvert.DeserializeObject<List<Producto>>(Respuesta);
                return productos;
            }
            else
                return await Task.FromResult(productos);

        }

        public async Task<Producto> GetProducto(int id)
        {
            Producto producto = new();

            StringContent content = new(JsonConvert.SerializeObject(producto), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.GetAsync("GestionPedidos/Producto?ProductoID="+ id);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                producto = JsonConvert.DeserializeObject<Producto>(Respuesta);
                return producto;
            }
            else
                return await Task.FromResult(producto);
        }

        public async Task<IActionResult> PutMaterial(Material material)
        {
            Material _material = new Material();
            StringContent content = new(JsonConvert.SerializeObject(material), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.PostAsync("GestionPedidos/Orden", content);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                _material = JsonConvert.DeserializeObject<Material>(Respuesta);
                return new OkResult();
            }
            else
                return new NoContentResult();
        }


    }
}
