using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using SV20T1020451.BusinessLayers;
using SV20T1020451.DomainModels;
using SV20T1020451.Web.AppCodes;
using SV20T1020451.Web.Models;
using System.Diagnostics;
namespace SV20T1020451.Web.Controllers
{
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 20;
        private string Customer_Search = "customer_search"; //tên biến lưu trong session
        public IActionResult Index()
        {
            //Lấy đầu vào tìm kiếm đang lưu lại trong session
            PaginationSearchInput input = ApplicationContext.GetSessionData<PaginationSearchInput>(Customer_Search);
            if(input==null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CustomerSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                DataCustomers=data
            };
            //Lưu lại điều kiện tìm kiếm vào trong session
            ApplicationContext.SetSessionData(Customer_Search, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin Khách hàng ";
            Customer? model = CommonDataService.GetCustomer(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public IActionResult Save(Customer data)
        {
            try
            {
                // Kiểm soát đầu vào và đưa các thông báo lỗi vào trong ModleState
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", "Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Vui lòng nhập email khách hàng");
                if (string.IsNullOrWhiteSpace(data.Province))
                    ModelState.AddModelError(nameof(data.Province), "Vui lòng chọn tỉnh thành ");
                // Thông qua thuộc tính isvalid của ModelState xem có tồn tại lỗi hay không
                if(!ModelState.IsValid)
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhập thông tin khách hàng";
                    return View("Edit", data);
                }
                if (data.CustomerID == 0)
                {
                    int id = CommonDataService.AddCustomer(data);
                    if(id<=0)
                    {
                        ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng");
                        return View("Edit",data);
                    }
                }
                else
                {
                    bool result = CommonDataService.updateCustomer(data);
                    if(!result)
                    {
                        ModelState.AddModelError(nameof(data.Email), "Địa chỉ email bị trùng với khách hàng khác");
                        return View("Edit", data);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ERROR", "Không thể lưu dữ liệu,vui lòng thử lại sau vài phút");
                return View("Edit", data);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            //ViewBag.Title = "Xóa khách hàng ";
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetCustomer(id);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !CommonDataService.IsUsedCustomer(id);

            return View(model);
        }
    }
}

/* tương tự thiết kế giao diện bổ sung cập nhật đối với:
 nhà cung câp, người giao hàng, loại hàng*/

// crtl k c : comment 1 dong lenh