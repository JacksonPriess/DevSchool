using DevSchool.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevSchool.Controllers
{
    public class TaskController : Controller
    {
        string baseUrl = "http://localhost:58034";

        public async Task<ActionResult> Index()
        {
            List<DevSchool.Models.Task> TaskInfo = new List<DevSchool.Models.Task>();

            using (var client = new HttpClient())
            {                
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                HttpResponseMessage Res = await client.GetAsync("api/Task");
                
                if (Res.IsSuccessStatusCode)
                {                    
                    var TaskResponse = Res.Content.ReadAsStringAsync().Result;                 
                    TaskInfo = JsonConvert.DeserializeObject<List<DevSchool.Models.Task>>(TaskResponse);

                }                                
            }
            return View(TaskInfo);
        }

        // GET: Task/Details/5
        public async Task<ActionResult> Details(int? id)        
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DevSchool.Models.Task TaskInfo = new DevSchool.Models.Task();

            using (var client = new HttpClient())
            {                
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                HttpResponseMessage Res = await client.GetAsync("api/Task" + "/" + id.ToString());
                
                if (Res.IsSuccessStatusCode)
                {                    
                    var TaskResponse = Res.Content.ReadAsStringAsync().Result;                 
                    TaskInfo = JsonConvert.DeserializeObject<DevSchool.Models.Task>(TaskResponse);
                }                
            }
            return View(TaskInfo);
        }       

        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Description,Status,CreationDate")] DevSchool.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var serializedTask = JsonConvert.SerializeObject(task);
                    var content = new StringContent(serializedTask, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(baseUrl + "/api/Task", content);
                }

                return RedirectToAction("Index");
            }

            return View(task);
        }
        
        // GET: Task/Details/5
        public async Task<ActionResult> Edit(int? id)        
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DevSchool.Models.Task TaskInfo = new DevSchool.Models.Task();

            using (var client = new HttpClient())
            {        
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                HttpResponseMessage Res = await client.GetAsync("api/Task" + "/" + id.ToString());
                                
                if (Res.IsSuccessStatusCode)
                {                    
                    var TaskResponse = Res.Content.ReadAsStringAsync().Result;                 
                    TaskInfo = JsonConvert.DeserializeObject<DevSchool.Models.Task>(TaskResponse);
                }                                
            }

            return View(TaskInfo);
        }

        // POST: Movies/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Description,Status,CreationDate")] DevSchool.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage responseMessage =
                        await client.PutAsJsonAsync(baseUrl + "/api/Task/" + task.Id, task);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //MessageBox.Show("Produto atualizado");
                    }
                    else
                    {
                        //MessageBox.Show("Falha ao atualizar o produto : " + responseMessage.StatusCode);
                    }
                }                
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int? id)        
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DevSchool.Models.Task TaskInfo = new DevSchool.Models.Task();

            using (var client = new HttpClient())
            {                
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                HttpResponseMessage Res = await client.GetAsync("api/Task" + "/" + id.ToString());
                                
                if (Res.IsSuccessStatusCode)
                {                
                    var TaskResponse = Res.Content.ReadAsStringAsync().Result;                
                    TaskInfo = JsonConvert.DeserializeObject<DevSchool.Models.Task>(TaskResponse);
                }                
            }

            return View(TaskInfo);
        }

        // POST: Movies/Delete/5                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
            
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage responseMessage = 
                    await client.DeleteAsync(String.Format("{0}/{1}", baseUrl+"/api/Task", id));
                if (responseMessage.IsSuccessStatusCode)
                {
                    //MessageBox.Show("Produto excluído com sucesso");
                }
                else
                {
                    //MessageBox.Show("Falha ao excluir o produto  : " + responseMessage.StatusCode);
                }
            }
            return RedirectToAction("Index");
        }

    }
}