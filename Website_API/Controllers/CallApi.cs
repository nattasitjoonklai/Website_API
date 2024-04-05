using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using Website_API.Data;
using System.Web.Mvc; 
using Website_API.Models;


namespace Website_API.Controllers
{
    public class CallApi : Controller


    {
        private readonly ApplicationDBContext _db;
        
        
        private readonly IHttpClientFactory _clientFactory;
        public CallApi(ApplicationDBContext db)
        {
            _db = db;
        }
       
        [HttpGet] // Method Get 
        public async Task<ActionResult<IEnumerable<CustomerUser>>> GetList() //ประกาศฟังค์ชั่นเพื่อใช้ดึงข้อมูล CustomerUser เพื่อที่จะนำข้อมูลไปแสดงผล
        {
         
            var query = from customer in _db.Customers  // querry นำข้อมูลออกมาแสดง
                        join user in _db.User
                        on customer.UserID equals user.Id into join_customer // join customer.user.id = user.id 
                        from user in join_customer.DefaultIfEmpty()
                        join address in _db.Address
                        on customer.Id equals address.Customer_ID into join_address // join customer.id = address.Customer_ID 
                        from address in join_address.DefaultIfEmpty()
                        select new CustomerUser //จัดเก็บไว้ในให้อยู่ในรูปแบบของ Model CustomerUser
                        {
                            
                            Id = user.Id, 
                            Name = user.Name ?? " " ,
                           Telephone = user.telephone ?? " " ,
                            Salary = customer.Salary  ,
                            Age = customer.Age     ,
                            Provice = address.Province ?? " " , 
                            Address = address.Address  ?? " " + " " + address.District ?? " " + " " + address.Province ?? " "  + " " + address.Country ?? " " + " " + address.Postal_Code ?? " ",



                        };

            var querylist = query.ToList(); //เก็บไว้ในตัวแปลที่ชื่อว่า  querylist



            return querylist; //ส่งค่ากลับไป 
        }

        
        [HttpPost]


        public async Task<IActionResult> ProcessInput(string input)
        {
            // ตรวจสอบความยาวของค่าที่ส่งเข้ามา
            if (input.Length > 99)
            {
                return BadRequest("ความยาวของค่าไม่เกิน 99 ตัวอักษร");
            }

            // ตัดค่าที่คั่นด้วย comma และเรียงลำดับตามเงื่อนไข
            var result = string.Join(",", input.Where(char.IsLetter).OrderBy(c => c))
                + string.Join(",", input.Where(char.IsDigit).OrderBy(c => c));


            return Ok(new { data = result });
        }
        private IEnumerable<string> GetDuplicates(string[] numbers)
        {
            var grouped = numbers.GroupBy(x => x)
                                 .Where(g => g.Count() > 1)
                                 .Select(g => g.Key);

            // เรียงตัวอักษรขึ้นก่อนตัวเลข
            var sorted = grouped.OrderBy(x => !char.IsDigit(x.FirstOrDefault())).ThenBy(x => x);

            // เรียงลำดับจากน้อยไปหามาก (ถ้าต้องการ)
            //var sorted = grouped.OrderBy(x => int.Parse(x));

            return sorted;
        }


        public async Task<ActionResult> CallApiInterface()
        {



           
            // URL ของ Free API
            string apiUrl = "https://jsonplaceholder.typicode.com/todos/1";

            // สร้าง HttpClient เพื่อเรียกใช้งาน API
            using (HttpClient client = new HttpClient())
            {
                // เรียกใช้งาน API ด้วย HTTP Method GET
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // ตรวจสอบสถานะการเรียกใช้งาน API
                if (response.IsSuccessStatusCode)
                {

                    // Store data in TempData
                   
                    // อ่านข้อมูล response และแปลงเป็นสตริง JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // แปลง JSON เป็น Object ApiResponseModel
                    CallApiInterface apiResponse = JsonConvert.DeserializeObject<CallApiInterface>(jsonResponse);
                    TempData["ApiResponse"] = apiResponse;

                   
                    TempData["ApiStatus"] = "Success";
                    TempData["HttpMethod"] = "GET";
                    // แสดงผลลัพธ์ใน View
                    return RedirectToAction("Displ0ayData", "SecondController");





                }
                else
                {
                    // ถ้าไม่สำเร็จให้แสดงข้อความผิดพลาด
                    ViewBag.ErrorMessage = "Failed to call API: " + response.ReasonPhrase;
                    return View("Error");
                }
            }
        }

       
        public async Task<ActionResult> GetFreeAPIResult(string url)
        {
            string method = HttpContext.Request.Method;  // Method ที่ใช้ในการเรียก Free API
            using (HttpClient clients = new HttpClient())
            {
                var response = await clients.GetAsync(url); // เรียก URL ไปดึงข้อมูลมาเก็บไว้ใน response
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    Respone todo = JsonConvert.DeserializeObject<Respone>(responseData); // เปลี่ยน string เป็นjson และนำค่าไปใส่ใน model 
                    return Ok(new { url, method, response = todo }); // แสดงผล Response 
                }
                else
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                }
            }
                

           



            
        }

        //public ActionResult Calculation(CalculationRequest request)
        //{
        //    // Assuming you have some data to return

        //    string msg = "";
        //    var data = new List<Response>();

        //    if (request.P1.Length > 99)
        //    {
        //        Console.WriteLine("The length of the input value is 99 characters.");
        //    }
        //    else if (request.P1.Length == 0)
        //    {
        //        Console.WriteLine("No input value");
        //    }
        //    else
        //    {

        //        // Split input data into array
        //        string[] dataArray = request.P1.Split(',');

        //        // Count occurrences of each element
        //        Dictionary<string, int> countDict = new Dictionary<string, int>();
        //        foreach (string item in dataArray)
        //        {
        //            if (countDict.ContainsKey(item))
        //            {
        //                countDict[item]++;
        //            }
        //            else
        //            {
        //                countDict[item] = 1;
        //            }
        //        }

        //        // Filter items with count > 1
        //        List<string> duplicates = countDict.Where(pair => pair.Value > 1)
        //                                           .OrderBy(pair => pair.Key)
        //                                           .Select(pair => pair.Key)
        //                                            .Distinct()
        //                                           .ToList();

        //        // Output the sorted duplicates
        //        foreach (string duplicate in duplicates)
        //        {
        //            // Concatenate string based on whether it's a number or not
        //            if (!int.TryParse(duplicate, out _))
        //            {
        //                data.Add(new Response
        //                {

        //                    rank = duplicate
        //                });
        //            }
        //        }

        //        // Now add the numeric values
        //        foreach (string duplicate in duplicates)
        //        {
        //            // Concatenate numeric values
        //            if (int.TryParse(duplicate, out _))
        //            {
        //                data.Add(new Response
        //                {

        //                    rank = duplicate
        //                });
        //            }
        //        }

        //    }

        //    return Json(data); // JsonRequestBehavior.AllowGet allows GET requests
        //}
    }
}

