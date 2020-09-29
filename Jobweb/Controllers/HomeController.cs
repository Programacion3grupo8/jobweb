using Jobweb.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml.Schema;

namespace Jobweb.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://localhost:44309/"; //API Base URL

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Puesto_Trabajo()
        {
            List<PuestoTrabajo> Puesto = new List<PuestoTrabajo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/PuestoTrabajo");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PuestoResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Puesto = JsonConvert.DeserializeObject<List<PuestoTrabajo>>(PuestoResponse);

                }
            }
            return View(Puesto);
        }
        public ActionResult Log()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Log(string user, string pass)
        {
            ViewBag.username = user;
            Usuario usr = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Usuario using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user}");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                }

            }
            if (usr != null) 
            {
                if (pass == usr.password)
                {
                    Session["User"] = usr;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Contraseña Incorrecta";
                }                
            }               
            else
            {
                ViewBag.Error = "El usuario no existe";
            }
            return View();



        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Log", "Home");            
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Signup(Usuario user, string password2, Compañia company = null, HttpPostedFileBase logoImg = null)
        {
            ViewBag.user = user.username;
            ViewBag.tipo = user.tipo;
            ViewBag.nombre = company.nombre;
            ViewBag.correo = company.email;
            ViewBag.url = company.url;
            if(user.password != password2)
            {
                ViewBag.Error = "Las contraseñas no coincieden";
                return View();
            }

            user.tipo = user.tipo.ToLower();
            Usuario usr = null;
            //verificando que el usuario no existe
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Usuario using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user.username}");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                }

               
            }
            if (usr != null)
            {
                ViewBag.Error = "Este nombre de usuario ya existe, elije otro";
                return View();
            }
            else
            {           
                //Creando usuario
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);
                    //definiendo header
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource Usuario using HttpClient  
                    HttpResponseMessage Res = await client.PostAsync($"api/v1/Usuario", byteContent);
                    //Checking the response is successful or not which is sent using HttpClient  

                }
                if(user.tipo == "poster" && company != null)
                {                
                    //obteniendo id del usuario creado
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Sending request to find web api REST service resource Usuario using HttpClient  
                        HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user.username}");
                        //Checking the response is successful or not which is sent using HttpClient  
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api and storing into the Employee list  
                            usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                        }
                    }
                    //comprobando la respuesta
                    if(usr != null)
                    {               
                        //pasando id al objeto comapany
                        company.idUsuario = usr.id;
                        //creando ruta donde se guardara el logo                 
                        if (logoImg != null)
                        {
                            company.logo = Server.MapPath("~/Img");
                            company.logo += $"/logo-{company.nombre}-{company.idUsuario}.jpg";
                        }
                        //Creando company
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            //Define request data format  
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            //Convirtiendo a json
                            var userContent = JsonConvert.SerializeObject(company);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                            var byteContent = new ByteArrayContent(buffer);
                            //definiendo header
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            //Sending request to find web api REST service resource Usuario using HttpClient  
                            HttpResponseMessage Res = await client.PostAsync($"api/v1/Company", byteContent);
                            //Checking the response is successful or not which is sent using HttpClient  
                            if (Res.IsSuccessStatusCode)
                            {
                                if(logoImg != null)
                                    logoImg.SaveAs(company.logo);                               
                                
                            }
                        }

                    }
                }
                return await Log(user.username, user.password);

            }
                
            
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}