﻿
using SV20T1020451.DomainModels;

namespace SV20T1020451.Web.Models
{
    /// <summary>
    /// lớp cha cho các lớp biểu diễn dữ liệu kết quả tìm kiếm , phân trang
    /// </summary>
    public abstract class BasePaginationResult
    {
        public int Page {  get; set; }
        public int PageSize {  get; set; }
        public string SearchValue { get; set; } = "";
        public int RowCount {  get; set; }
        public int PageCount {
            get
            {
                if(PageSize ==0)
                    return 1;
                int c = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    c += 1;
                 return c;

            }
        }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
    public class CustomerSearchResult : BasePaginationResult
    {
        public List<Customer> DataCustomers { get; set; }
    }
    public class CategorySearchResult : BasePaginationResult
    {
        public List<Category> DataCategories { get; set; }
    }
    public class EmployeeSearchResult : BasePaginationResult
    {
        public List<Employee> DataEmployees { get; set; }
    }
    public class ShipperSearchResult : BasePaginationResult
    {
        public List<Shipper> DataShippers { get; set; }
    }
    public class SupplierSearchResult : BasePaginationResult
    {
        public List<Supplier> DataSuppliers { get; set; }
    }
    public class ProductSearchResult : BasePaginationResult
    {
        public List<Product> DataProducts { get; set; }
        
    }
      

}
