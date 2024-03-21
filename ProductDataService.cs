using SV20T1020451.DataLayers.SQLServer;
using SV20T1020451.DataLayers;
using SV20T1020451.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV20T1020451.BusinessLayers
{
    public static class ProductDataService
    {
        private static readonly IProductDAL productDB;
        /// <summary>
        /// ctor( static constructor hoạt động như thế nào ? cách viết?)
        /// </summary>
        static ProductDataService()
        {
            string connectionString = Configuration.ConnectionString;
            productDB = new ProductDAL(connectionString);

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

        public static IList<Product> ListOfProduct(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            rowCount = productDB.Count(searchValue);
            return productDB.List(page, pageSize, searchValue).ToList();
        }

        public static int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            return productDB.Count(searchValue);
        }

        public static Product? Get(int id)
        {
            return productDB.Get(id);
        }

        public static int Add(Product data)
        {
            return productDB.Add(data);

        }

        public static bool Update(Product data)
        {
            return productDB.Update(data);
        }

        public static bool Delete(int id)
        {
            if (productDB.IsUsed(id))
                return false;
            return productDB.Delete(id);
        }

        public static bool IsUsed(int id)
        {
            return productDB.IsUsed(id);
        }


        public static IList<ProductPhoto> ListPhotos(int productID)
        {
            return productDB.ListPhotos(productID).ToList();
        }


        public static ProductPhoto? GetPhoto(long PhotoID)
        {
            return productDB.GetPhoto(PhotoID);
        }

        public static int AddPhoto(ProductPhoto data)
        {
            return productDB.AddPhoto(data);
        }

        public static bool UpdatePhoto(ProductPhoto data)
        {
            return productDB.UpdatePhoto(data);
        }


        public static bool DeletePhoto(long photoID)
        {
            return productDB.DeletePhoto(photoID);
        }


        public static IList<ProductAttribute> ListAttributes(int productID)
        {
            return productDB.ListAttributes(productID).ToList();
        }


        public static ProductAttribute? GetAttribute(long attributeID)
        {

            return productDB.GetAttribute(attributeID);
        }



        public static int AddAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }

        public static bool DeleteAttribute(long attributeid)
        {
            return productDB.DeleteAttribute(attributeid);
        }
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }

      
    }
}
