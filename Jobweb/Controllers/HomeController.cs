using Jobweb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Jobweb.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://localhost:44309/"; //API Base URL
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Log()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Log(string user, string pass)
        {
            Usuario usr = new Usuario();
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
                else
                {
                    ViewBag.Error = "El usuario no existe";
                    return View();
                }
            }
            if(user == usr.username && pass == usr.password)
            {
                Session["User"] = usr;
                return RedirectToAction("Index","Home");
            }
            
                ViewBag.Error = "Contraseña Incorrecta";
                return View();
            
           
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Log", "Home");
            
        }
    }
}