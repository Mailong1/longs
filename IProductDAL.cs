using SV20T1020451.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020451.DataLayers.SQLServer
{
    public interface IProductDAL
    {
        /// <summary>
        ///Tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">số dòng trên mỗi trang (bằng 0 nếu ko phân trang) ,</param>
        /// <param name="searchValue">Giá trị tìm kiếm(Chuõi rống nếu lấy toàn bộ dữ liệu</param>
        /// <param name="categoryID">Mã loại hàng cần tìm (0 nếu không tìm theo mã loại hàng</param>
        /// <param name="supplierID">mac nhà cung cấp cần tìm (0 nếu không tìm theo nhà cung cấp</param>
        /// <param name="maxPrice">Mức giá lớn nhất trong khoảng giá trị cần tìm</param>
        /// <param name="minPrice">Mực giá trị thấp nhất khoảng giá trị cần tìm</param>
        /// 
        /// <returns></returns>
        IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0);

        int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0);


        Product? Get(int id);

        int Add(Product data);

        bool Update(Product data);

        bool Delete(int id);

        bool IsUsed(int id);


        IList<ProductPhoto> ListPhotos(int productID);


        ProductPhoto? GetPhoto(long PhotoID);

        int AddPhoto(ProductPhoto data);

        bool UpdatePhoto(ProductPhoto data);


        bool DeletePhoto(long photoID);


        IList<ProductAttribute> ListAttributes(int productID);


        ProductAttribute? GetAttribute(long attributeID);



        int AddAttribute(ProductAttribute data);

        bool DeleteAttribute(long attributeid);
        bool UpdateAttribute(ProductAttribute data);
    }
}

