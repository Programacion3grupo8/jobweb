using Jobweb.Filtros;
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
    public class AdminController : Controller
    {
        string Baseurl = "https://localhost:44309/"; //API Base URL
                                                     // GET: Admin
        [Autorizaciones(nivel: "administrador")]
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Users
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> Users()
        {
            List<Usuario> Users = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/Usuario");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Users = JsonConvert.DeserializeObject<List<Usuario>>(UserResponse);

                }
            }
            return View(Users);
        }


        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditUser(int id)
        {
            Usuario user = new Usuario();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        user = JsonConvert.DeserializeObject<Usuario>(UserResponse);

                    }
                }
                return View(user);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditUser(Usuario user)
        {

            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync($"api/v1/Usuario/{user.id}", byteContent);


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Users");
                    }
                }
                return View(user);
            }
            catch (Exception err)
            {
                return View(err);
            }
        }


        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            Usuario user = new Usuario();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.DeleteAsync($"api/v1/Usuario/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Users");

                    }
                }
                return RedirectToAction("Users");
            }
            catch (Exception err)
            {
                return View(err);
            }
        }

        [Autorizaciones(nivel: "administrador")]
        public ActionResult Categories()
        {
            return View();
        }

        [Autorizaciones(nivel: "administrador")]
        public ActionResult Listings()
        {
            return View();
        }

        [Autorizaciones(nivel: "administrador")]
        public ActionResult Settings()
        {
            return View();
        }
    }
}