using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace SV20T1020451.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Create()
        {
            var model = new Models.PerSon()
            {
                Name = "Long",
                BirthDate = new DateTime(1990,10,10),
                Salary = 10.25m
            };

            return View(model);
        }
        public IActionResult Save(Models.PerSon model,string BirthDayInput ="")
        {
            DateTime? dValue = StringToDateTime(BirthDayInput);
            if(dValue.HasValue)
            {
                model.BirthDate = dValue.Value;
            }
            return Json(model);
        }
        private DateTime? StringToDateTime(string s, string formats = "d/M/yyyy;d-M-yyyy;d.M.yyyy")
        {
            try
            {
                return DateTime.ParseExact(s,formats.Split(';'), CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}
