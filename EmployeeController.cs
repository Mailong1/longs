using Microsoft.AspNetCore.Mvc;
using SV20T1020451.BusinessLayers;
using SV20T1020451.DomainModels;
using SV20T1020451.Web.AppCodes;

namespace SV20T1020451.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(out rowCount, page, PAGE_SIZE, searchValue ?? "");
            var model = new Models.EmployeeSearchResult()
            {
                Page = page,
                PageSize = PAGE_SIZE,
                SearchValue = searchValue ?? "",
                RowCount = rowCount,
                DataEmployees = data
            };

            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            Employee model = new Employee()
            {
                EmployeeID = 0
            };

            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhập thông tin nhân viên";
            Employee? model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            if(string.IsNullOrEmpty(model.Photo))
            {
                model.Photo = "NoPhoto.png";
            }
            return View(model);
        }
        
        public IActionResult Save(Employee data , string birthDateInput,IFormFile? uploadPhoto )
        {
            //xu ly ngay sinh
            DateTime? birthDate = birthDateInput.ToDateTime();
            if (birthDate.HasValue)
            {
                data.BirthDate = birthDate.Value;
            }
            if(uploadPhoto != null) {
                string filename =$"{DateTime.Now.Ticks}_{uploadPhoto.FileName}"; //ten file se luu
                string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images\\Employees"); //duong dan den thu muc luu file
                string filePath=Path.Combine(folder, filename); // đường dẫn file cần lưu D:\ images\employee\photo.png.
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                data.Photo = filename;
            }
            if (string.IsNullOrWhiteSpace(data.FullName))
                ModelState.AddModelError("FullName", "Tên nhân viên không được để trống");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError("Email", "Vui lòng nhập email khách hàng");
            if (string.IsNullOrWhiteSpace(data.Photo))
                ModelState.AddModelError("Photo", "Vui lòng chọn hình ành");
            
            // Thông qua thuộc tính isvalid của ModelState xem có tồn tại lỗi hay không
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung khách hàng" : "Cập nhập thông tin khách hàng";
                return View("Edit", data);
            }

            if (data.EmployeeID == 0)
                {
                    int id = CommonDataService.AddEmployee(data);
                if (id <= 0)
                {
                    ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng");
                    return View("Edit", data);
                }
            }
                else
                {
                    bool result = CommonDataService.updateEmployee(data);
                      if (!result)
                {
                    ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng với khách hàng khác");
                    return View("Edit", data);
                }
            }
                return RedirectToAction("Index");
             
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !CommonDataService.IsUsedEmployee(id);

            return View(model);
        }
    }
}
