using SV20T1020451.DataLayers;
using SV20T1020451.DataLayers.SQLServer;
using SV20T1020451.DomainModels;

namespace SV20T1020451.BusinessLayers
{
    /// <summary>
    /// cung cấp các chức năng nghiệp vụ liên quan đến các dữ liệu "chung"
    /// (tinh/thành, khách hàng, nhà cung cấp, loại hàng, người giao hàng,nhân viên)
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Province> provinceDB;
        private static readonly ICommonDAL<Supplier> supplierDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Category> categoryDB;
        /// <summary>
        /// ctor( static constructor hoạt động như thế nào ? cách viết?)
        /// </summary>
        static CommonDataService()
        {
            string connectionString = Configuration.ConnectionString;
            provinceDB = new ProvinceDAL(connectionString);
            supplierDB = new SupplierDAL(connectionString);
            customerDB = new CustomerDAL(connectionString);
            shipperDB = new ShipperDAL(connectionString);
            employeeDB = new EmployeeDAL(connectionString);
            categoryDB = new CategoryDAL(connectionString);
        }

        /// <summary>
        ///Tìm kiếm và lấy ds khách hàng
        /// </summary>
        /// <param name="rowCount">Tham số đầu ra cho biết số dòng dữ liệu tìm được</param>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang(0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (Rỗng nếu lấy toàn bộ khách hàng)</param>
        /// <returns></returns>
        /// 


        /// <summary>
        /// Danh sách các tỉnh thành
        /// </summary>
        public static List<Province> ListOfProvinces()
        {
            return provinceDB.List().ToList();
        }
        /// <summary>
        /// Tìm  kiếm và lấy danh sách nhà cung cấp
        /// </summary>
       
        
        public static List<Supplier> ListOfSuppliers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin nhà cung cấp theo mã cung cấp
        /// </summary>
        /// 
        public static Supplier? GetSupplier(int id)
        {
            return supplierDB.Get(id);
        }
        /// <summary>
        /// Bổ sung nhà cung cấp mới
        /// </summary>
        public static int AddSupplier(Supplier supplier)
        {
            return supplierDB.Add(supplier);
        }

        /// <summary>
        /// Cập nhập nhà cung cấp
        /// </summary>
        public static bool updateSupplier(Supplier supplier)
        {
            return supplierDB.Update(supplier);
        }
        /// <summary>
        /// Xóa nhà cung cấp có mã là id
        /// </summary>
        public static bool DeleteSupplier (int id)
        {
            if (supplierDB.IsUsed(id))
                return false;
            return supplierDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem nhà cung cấp có mã id hiện có dữ liệu liên quan hay không
        /// </summary>
        public static bool IsUsedSupplier(int id)
        {
            return supplierDB.IsUsed(id);
        }




        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng
        /// </summary>
        public static List<Customer> ListOfCustomers(out int rowCount,int page=1,int pageSize=0,string searchValue="")
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page,pageSize,searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 khách hàng theo mã khách hàng
        /// </summary>
        public static Customer? GetCustomer(int id)
        {
            return customerDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khách hàng mới
        /// </summary>
        public static int AddCustomer(Customer customer)
            {
                return customerDB.Add(customer);
            }
        /// <summary>
        /// Cập nhập khách hàng
        /// </summary>
        public static bool updateCustomer(Customer customer)
        {
            return customerDB.Update(customer);
        }
        /// <summary>
        /// Xóa khách hàng có mã là id
        /// </summary>
        public static bool DeleteCustomer(int id)
        {
            if (customerDB.IsUsed(id))
                return false;
            return customerDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra khách hàng có mã là id hiện có dữ liệu liên quan hay không
        /// </summary>
        public static bool IsUsedCustomer(int id)
        {
            return customerDB.IsUsed(id);
        }


        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng
        /// </summary>
        public static List<Shipper> ListOfShippers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = supplierDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 người giao hàng theo mã giao hàng
        /// </summary>
        public static Shipper? GetShipper(int id)
        {
            return shipperDB.Get(id);
        }
        /// <summary>
        /// Bổ sung người giao hàng mới
        /// </summary>
        public static int AddShipper(Shipper shipper)
        {
            return shipperDB.Add(shipper);
        }
        /// <summary>
        /// Cập nhập khách hàng
        /// </summary>
        public static bool updateShipper(Shipper shipper)
        {
            return shipperDB.Update(shipper);
        }
        /// <summary>
        /// Xóa người giao hàng có mã là id
        /// </summary>
        public static bool DeleteShipper(int id)
        {
            if (shipperDB.IsUsed(id))
                return false;
            return shipperDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra người giang hàng có mã là id hiện có dữ liệu liên quan hay không
        /// </summary>
        public static bool IsUsedShipper(int id)
        {
            return shipperDB.IsUsed(id);
        }

        
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên
        /// </summary>
       
        public static List<Employee> ListOfEmployees(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 nhân viên theo mã nhân viên
        /// </summary>
        public static Employee? GetEmployee(int id)
        {
            return employeeDB.Get(id);
        }
        /// <summary>
        /// Bổ sung nhân viên mới
        /// </summary>
        public static int AddEmployee(Employee employee)
        {
            return employeeDB.Add(employee);
        }
        /// <summary>
        /// Cập nhập nhân viên
        /// </summary>
        public static bool updateEmployee(Employee employee)
        {
            return employeeDB.Update(employee);
        }
        /// <summary>
        /// Xóa nhân viên có mã là id
        /// </summary>
        public static bool DeleteEmployee(int id)
        {
            if (employeeDB.IsUsed(id))
                return false;
            return employeeDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra nhân viên có mã là id hiện có dữ liệu liên quan hay không
        /// </summary>
        public static bool IsUsedEmployee(int id)
        {
            return employeeDB.IsUsed(id);
        }


        /// <summary>
        /// Tìm kiếm và lấy danh sách loại hàng
        /// </summary>
        public static List<Category> ListOfCategories(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của 1 loại hàng theo mã loại hàng
        /// </summary>
        public static Category? GetCategory(int id)
        {
            return categoryDB.Get(id);
        }
        /// <summary>
        /// Bổ sung loại hàng mới
        /// </summary>
        public static int AddCategory(Category category)
        {
            return categoryDB.Add(category);
        }
        /// <summary>
        /// Cập nhập loai hàng
        /// </summary>
        public static bool updateCategory(Category category)
        {
            return categoryDB.Update(category);
        }
        /// <summary>
        /// Xóa loại hàng có mã là id
        /// </summary>
        public static bool DeleteCategory(int id)
        {
            if (categoryDB.IsUsed(id))
                return false;
            return categoryDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra loại hàng có mã là id hiện có dữ liệu liên quan hay không
        /// </summary>
        public static bool IsUsedCategory(int id)
        {
            return categoryDB.IsUsed(id);
        }

        public static List<Customer> ListProducts(out int rowCount, int page, int pAGE_SIZE, string v)
        {
            throw new NotImplementedException();
        }
    }

}
// 3-layers  -> n-layers