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


       
        public ActionResult Calculation([FromBody] CalculationRequest request)
        {
            // Assuming you have some data to return

            string msg = "";
            var data = new List<Response>();

            if (request.P1.Length > 99)
            {
                Console.WriteLine("The length of the input value is 99 characters.");
            }
            else if (request.P1.Length == 0)
            {
                Console.WriteLine("No input value");
            }
            else
            {

                // Split input data into array
                string[] dataArray = request.P1.Split(',');

                // Count occurrences of each element
                Dictionary<string, int> countDict = new Dictionary<string, int>();
                foreach (string item in dataArray)
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
                List<string> duplicates = countDict.Where(pair => pair.Value > 1)
                                                   .OrderBy(pair => pair.Key)
                                                   .Select(pair => pair.Key)
                                                    .Distinct()
                                                   .ToList();

                // Output the sorted duplicates
                foreach (string duplicate in duplicates)
                {
                    // Concatenate string based on whether it's a number or not
                    if (!int.TryParse(duplicate, out _))
                    {
                        data.Add(new Response
                        {

                            rank = duplicate
                        });
                    }
                }

                // Now add the numeric values
                foreach (string duplicate in duplicates)
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

            return Json(data); // JsonRequestBehavior.AllowGet allows GET requests
        }
    }
}
