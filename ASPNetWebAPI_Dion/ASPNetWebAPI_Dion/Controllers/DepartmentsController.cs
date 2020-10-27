using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetWebAPI_Dion.Controllers
{
    public class DepartmentsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44351/API/")
        };

        // GET: Departments
        public ActionResult Index()
        {
            //Response.AddHeader("Refresh", "5");
            return View();
        }
        public JsonResult LoadDepartment()
        {
            IEnumerable<Department> departments = null;
            var responseTask = client.GetAsync("Departments");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                //var readTask = result.Content.ReadAsStringAsync<IList<Department>>();
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                departments = readTask.Result;
            }
            else
            {
                departments = Enumerable.Empty<Department>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return new JsonResult { Data = departments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Insert(Department department)
        {
            var myContent = JsonConvert.SerializeObject(department);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Departments", byteContent).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("Departments");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<Department>>();
                var dept = data.FirstOrDefault(t => t.Id == Id);
                var json = JsonConvert.SerializeObject(dept, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("internal server error");
        }

        public JsonResult Update(Department department)
        {
            var myContent = JsonConvert.SerializeObject(department);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Departments/" + department.Id, byteContent).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Departments/" + Id).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}