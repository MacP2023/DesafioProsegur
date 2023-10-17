using GestionDePedidosSB.Services;
using GestionDePedidosSB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using UtilidadesGenerales.Utilidades;

namespace GestionDePedidosSB.Services
{
    public class ServicesUsuario:IServicesUsuario
    {
        //private static string UserName = "";
        //private static string Clave = "";
        private static string UrlBase = "";

        private readonly HttpClient _httpClient = new HttpClient();
        public ServicesUsuario()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json").Build();
            UrlBase = "http://localhost:5132/api/";
            _httpClient = new();
            _httpClient.BaseAddress = new(UrlBase);
        }

        public async Task Login(string UserName,string Clave)
        {
           bool usuariopordefecto=false;   
           ModUsuario usuario=new();
            if (UserName == "Administrador")
            {
                usuario = new ModUsuario()
                {
                    UserName = UserName,
                    Nombre = "Pepe",
                    Apellido = "Perez",
                    DNI = "1234",
                    Provincia = 0,
                    Direccion = "su casa",
                    Celular = "11111",
                    FechaRegistro = Util.ObtenerFechaEncurso(),
                    FechaUltimoAcceso = Util.ObtenerFechaEncurso(),
                    Rol = 3,
                    Clave = Clave

                };
                usuariopordefecto = true;
            }
            if (UserName == "Supervisor")
            {
                usuario = new ModUsuario()
                {
                    UserName = UserName,
                    Nombre = "Pepe1",
                    Apellido = "Perez",
                    DNI = "1234",
                    Provincia = 0,
                    Direccion = "su casa",
                    Celular = "11111",
                    FechaRegistro = Util.ObtenerFechaEncurso(),
                    FechaUltimoAcceso = Util.ObtenerFechaEncurso(),
                    Rol = 2,
                    Clave = Clave

                };
                usuariopordefecto = true;
            }
            if (UserName == "Empleado")
            {
                usuario = new ModUsuario()
                {
                    UserName = UserName,
                    Nombre = "Pepe2",
                    Apellido = "Perez",
                    DNI = "1234",
                    Provincia = 0,
                    Direccion = "su casa",
                    Celular = "11111",
                    FechaRegistro = Util.ObtenerFechaEncurso(),
                    FechaUltimoAcceso = Util.ObtenerFechaEncurso(),
                    Rol = 1,
                    Clave = Clave
                };
                usuariopordefecto = true;
            }
            if (usuariopordefecto) { 
                StringContent content = new(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "Application/json");
                HttpResponseMessage Resultado = await _httpClient.PostAsync("Usuario", content);
               
            }
        }


   
        public async Task<ActionResult<ModUsuario>> DeleteUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ModUsuario>> GetIniciarSession(string UserName, string clave)
        {
            List<ModUsuario> Usuarios = new();
            await Login(UserName,clave);

            //_httpClient.DefaultRequestHeaders.Add("UserName", UserName);
            //_httpClient.DefaultRequestHeaders.Add("Clave", clave);

            HttpResponseMessage Resultado = await _httpClient.GetAsync("Usuario/Iniciar?UserName=" + UserName + "&Clave=" + clave);
            
            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                Usuarios = JsonConvert.DeserializeObject<List<ModUsuario>>(Respuesta);
                return Usuarios;
            }
            else
                return await Task.FromResult(Usuarios);

        }

        public Task<ActionResult<IEnumerable<ModUsuario>>> GetUsuario()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<ModUsuario>> GetUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ModUsuario> PostUsuario(ModUsuario usuario)
        {
            ModUsuario musuario = new();
            //usuario.Provincia=Convert
            StringContent content = new(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "Application/json");
            HttpResponseMessage Resultado = await _httpClient.PostAsync("Usuario", content);

            if (Resultado.IsSuccessStatusCode)
            {
                string Respuesta = await Resultado.Content.ReadAsStringAsync();
                musuario = JsonConvert.DeserializeObject<ModUsuario>(Respuesta);
                return musuario;
            }
            else
                return null;
        }

        public Task<IActionResult> PutUsuario(int id, ModUsuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
