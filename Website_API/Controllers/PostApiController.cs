using Microsoft.AspNetCore.Mvc;
using Website_API.Models;
using static Website_API.Models.ApiPost;

namespace Website_API.Controllers
{
    public class PostApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Calculation([FromBody] CalculationRequest request)  //ประกาศฟังค์ชัน รับค่าจาก parameter
        {
           

            string msg = "";
            var data = new List<Response>();  //ประกาศ List เก็บไว้ใน ตัวแปร data

            if (request.P1.Length > 99)  //เช็คจำนวนข้อมูลที่ส่งมาทั้งหมด  >  99
            {
                Console.WriteLine("The length of the input value is 99 characters.");
            }
            else if (request.P1.Length == 0) //เช็คจำนวนข้อมูลที่ส่งมาทั้งหมด == 0 
            {
                Console.WriteLine("No input value");
            }
            else
            {

                // Split input data into array
                string[] dataArray = request.P1.Split(','); //ตัด comma ออกจากข้อมูลทั้งหมด

                // Count occurrences of each element
                Dictionary<string, int> countDict = new Dictionary<string, int>(); //ประกาศตัวแปรเพื่อมาใช้เก็บข้อมูล
                foreach (string item in dataArray) // วนหาค่าจำนวนตัวอักษร
                {
                    if (countDict.ContainsKey(item))  
                    {
                        countDict[item]++;
                    }
                    else
                    {
                        countDict[item] = 1;
                    }
                }
               
               
                // Filter items with count > 1
                List<string> duplicates = countDict.Where(pair => pair.Value > 1)  //Query ดึงค่าที่มีมากกว่าหนึ่งตัวมาแสดงโดยเรียงกันจากน้อยไปมากและไม่เอาข้อมูลที่ซ้ำกัน
                                                   .OrderBy(pair => pair.Key)
                                                   .Select(pair => pair.Key)
                                                    .Distinct()
                                                   .ToList(); 

              
                foreach (string duplicate in duplicates) // วนเอาค่าที่เป็นตัวอักษร เก็บใส่ Data Model
                {
                   
                    if (!int.TryParse(duplicate, out _))
                    {
                        data.Add(new Response
                        {

                            rank = duplicate
                        });
                    }
                }

                // Now add the numeric values
                foreach (string duplicate in duplicates) // วนเอาค่าที่เป็นตัวเลข เก็บใส่ Data Model
                {
                    // Concatenate numeric values
                    if (int.TryParse(duplicate, out _))
                    {
                        data.Add(new Response
                        {

                            rank = duplicate
                        });
                    }
                }

            }

            return Json(data); //ส่งข้อมูล data เป็นjsonกลับไป
        }
    }
}
