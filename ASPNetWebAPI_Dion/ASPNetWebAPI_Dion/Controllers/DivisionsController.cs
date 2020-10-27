using API.ViewModel;
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
    public class DivisionsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44351/API/")
        };
        // GET: Division
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult LoadDivision()
        {
            IEnumerable<DivisionVM> divisionVM = null;
            var responseTask = client.GetAsync("Divisions");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVM>>();
                readTask.Wait();
                divisionVM = readTask.Result;
            }
            else
            {
                divisionVM = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return new JsonResult { Data = divisionVM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Delete(int Id)
        {
            var result = client.DeleteAsync("Divisions/" + Id).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("Divisions");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<DivisionVM>>();
                var dept = data.FirstOrDefault(t => t.Id == Id);
                var json = JsonConvert.SerializeObject(dept, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("internal server error");
        }

        public JsonResult Insert(DivisionVM divisionVM)
        {
            var myContent = JsonConvert.SerializeObject(divisionVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Divisions", byteContent).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Update(DivisionVM divisionVM)
        {
            var myContent = JsonConvert.SerializeObject(divisionVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Divisions/" + divisionVM.Id, byteContent).Result;
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}