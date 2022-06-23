using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApi.Entity;

namespace WebApiSecond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecondController : ControllerBase
    {
        string Baseurl = "https://localhost:44309/";

        [HttpGet]
        public async Task<IActionResult> GetgradesFirstController()
        {
            List<Grade> response = new List<Grade>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetDepartments using HttpClient
                //sends a GET request to the specified Uri as an asynchronous operation.
                HttpResponseMessage Res = await client.GetAsync("api/Grade/ListOfGrades");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {

                    var ObjResponse = Res.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<List<Grade>>(ObjResponse);
                    //var r = JObject.Parse(ObjResponse)
                }
                //returning the student list to view  
                return Ok(response);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostGrade(Grade gr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var gra = new
                {
                    GradeName = gr.GradeName
                };
                JsonContent content = JsonContent.Create(gra);
                var result = await client.PostAsync("api/Grade/GradePost", content);

                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);
                return Ok(resultContent);
            }

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> PutGrade(Grade gr,int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                var obj = new
                {
                    GradeName = gr.GradeName,
                    GradeId=id
                };
                JsonContent content = JsonContent.Create(obj);
                var methodurl = "api/Grade/GradePut/"+id;
                var result = await client.PutAsync(methodurl, content);

                string resultContent = await result.Content.ReadAsStringAsync();

                Console.WriteLine(resultContent);

                return Ok(resultContent);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);


                var methodurl = "api/Grade/GradeDelete/" + id;
                var result = await client.DeleteAsync(methodurl);

                string resultContent = await result.Content.ReadAsStringAsync();

                Console.WriteLine(resultContent);

                return Ok(resultContent);
            }
        }


    }
}
