using FastReport.OpenSource.Web.MVC.Models;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FastReport.OpenSource.Web.MVC.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {      
        string reportToLoad { get; set; } = "Simple List";      
  


        [HttpGet]
        public IActionResult Index()
        {
            var myReport = new HomeModel()
            {
                WebReport = new WebReport(),                
            };
       
            myReport.WebReport.Report.Load(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"{ reportToLoad}.frx"));
            var myEmployeesList = Services.GetEmployees();
            var myConvertedTable = myEmployeesList.ConvertToDataSet("Employees");
            myReport.WebReport.Report.RegisterData(myConvertedTable, "Database");            
            return View(myReport);
        }
    }
}
