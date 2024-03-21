using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SV20T1020451.BusinessLayers;
using SV20T1020451.DomainModels;
using SV20T1020451.Web.Models;

namespace SV20T1020451.Web.Controllers
{
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int rowCount = 0;
            var data = ProductDataService.ListOfProduct(out rowCount, page, PAGE_SIZE, searchValue ?? "");
            var model = new Models.ProductSearchResult()
            {
                Page = page,
                PageSize = PAGE_SIZE,
                SearchValue = searchValue ?? "",
                RowCount = rowCount,
                CategoryID= categoryID,
                SupplierID= supplierID,
                MinPrice= minPrice,
                MaxPrice= maxPrice,
                DataProducts = (List<Product>)data
            };

            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            EidtProduct model = new EidtProduct();
            model.product = new Product()
            {
                ProductID = 0
            };
            ViewBag.IsEdit = false;
           
            return View("Edit", model);
        }
        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin Khách hàng ";
            EidtProduct model = new EidtProduct();
             model.product = ProductDataService.Get(id);

            ViewBag.IsEdit = true;
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public IActionResult Save(Product data, IFormFile? uploadPhoto)
        {
            try
            {
                if (uploadPhoto != null)
                {
                    string filename = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}"; //ten file se luu
                    string folder = @"D:\C#\SV20T1020451\SV20T1020451.Web\wwwroot\images\Products"; //duong dan den thu muc luu file
                    string filePath = Path.Combine(folder, filename); // đường dẫn file cần lưu D:\ images\employee\photo.png.
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        uploadPhoto.CopyTo(stream);
                    }
                    data.Photo = filename;
                }
                if (string.IsNullOrWhiteSpace(data.ProductName))
                    ModelState.AddModelError("ProductName", "Tên mặt hàng không được để trống");
                if (string.IsNullOrWhiteSpace(data.ProductDescription))
                    ModelState.AddModelError("ProductDescription", "Mô tả mặt hàng không được để trống");

                // Thông qua thuộc tính isvalid của ModelState xem có tồn tại lỗi hay không
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung mặt hàng" : "Cập nhập thông tin mặt hàng";
                    return View("Edit", new EidtProduct { product = data });
                }
                if (data.ProductID == 0)
                {
                    int id = ProductDataService.Add(data);
                }
                else
                {
                    bool result = ProductDataService.Update(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public IActionResult Delete(int id = 0)
        {
            //ViewBag.Title = "Xóa khách hàng ";
            if (Request.Method == "POST")
            {
                ProductDataService.Delete(id);
                return RedirectToAction("Index");
            }

            var model = ProductDataService.Get(id);
            if (model == null)
                return RedirectToAction("Index");
            ViewBag.AllowDelete = !ProductDataService.IsUsed(id);

            return View(model);
        }
        public IActionResult Photo(ProductPhoto data,string method,int id =0)
        {
            
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    ProductPhoto model = new ProductPhoto()
                    {
                        ProductID = 0
                    };
                    return View("edit", model);
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";

                    ProductPhoto? model1 = ProductDataService.GetPhoto(id);
                    if (model1 == null)
                        return RedirectToAction("Index");

                    return View(model1);
                case "save":
                    try { 
                        if (data.PhotoID == 0)
                        {
                            id = ProductDataService.AddPhoto(data);
                        }
                        else
                        {
                            bool result = ProductDataService.UpdatePhoto(data);
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }
            
                case "delete":
                    if (Request.Method == "POST")
                    {
                        ProductDataService.DeletePhoto(id);
                        return RedirectToAction("Index");
                    }

                    var model3 = ProductDataService.GetPhoto(id);
                    if (model3 == null)
                        return RedirectToAction("Index");
                    return View(model3);
                default:
                    return RedirectToAction("Index");
            }
        }
        public IActionResult Attribute(ProductAttribute data, String method,int id=0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    ProductAttribute model = new ProductAttribute()
                    {
                        ProductID = 0
                    };
                    return View("edit", model);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    ProductAttribute? model1 = ProductDataService.GetAttribute(id);
                    if (model1 == null)
                        return RedirectToAction("Index");

                    return View(model1);
                case "save":
                    try
                    {
                        if (data.AttributeID== 0)
                        {
                            id = ProductDataService.AddAttribute(data);
                        }
                        else
                        {
                            bool result = ProductDataService.UpdateAttribute(data);
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        return Content(ex.Message);
                    }

                case "delete":
                    if (Request.Method == "POST")
                    {
                        ProductDataService.DeleteAttribute(id);
                        return RedirectToAction("Index");
                    }

                    var model3 = ProductDataService.GetAttribute(id);
                    if (model3 == null)
                        return RedirectToAction("Index");
                    return View(model3);
                default:
                    return RedirectToAction("Index");
            
            
            }
        }

    }
}




 
