using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Identity.Client;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using SV20T1020451.DataLayers.SQLServer;
using SV20T1020451.DataLayers;
using SV20T1020451.DomainModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV20T1020451.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }
       

        public int Add(Product data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into products(ProductName,ProductDescription,SupplierID,CategoryID,Unit,Price,Photo,IsSelling)
                            values(@ProductName,@ProductDescription,@SupplierID,@CategoryID,@Unit,@Price,@Photo,@IsSelling);
                            select @@identity;
                            ";
                var parameters = new
                {
                    ProductName = data.ProductName ?? "",
                    ProductDescription = data.ProductDescription ?? "",
                    SupplierID = data.SupplierID,
                    CategoryID = data.CategoryID,
                    Unit = data.unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo ?? "",
                    IsSelling = data.IsSelling

                };
                // thuc thi cau lenh ?query , scalar , NonQuery
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }

            return id;
        }

      
        public int AddAttribute(ProductAttribute data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into productAttributes(AttributeID,ProductID,AttributeName,AttributeValue,DisPlayOrder)
                            values(@AttributeID,@ProductID,@AttributeName,@AttributeValue,@DisPlayOrder);
                            select @@identity;
                           ";
                var parameters = new
                {
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue ?? "",
                    ProductID = data.ProductID,
                    DisPlayOrder = data.DisPlayOrder
                    
                };
                // thuc thi cau lenh ?query , scalar , NonQuery
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public int AddPhoto(ProductPhoto data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into ProductPhotos(PhotoID,ProductID,Photo,Description,DisPlayOrder,IsHidden)
                            values(@PhotoID,@ProductID,@Photo,@Description,@DisPlayOrder,@IsHidden);
                            select @@identity;
                            ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    Photo = data.Photo ?? "",
                    Description = data.Description,
                    DisPlayOrder = data.DisPlayOrder,
                    IsHidden = data.IsHidden

                };
                // thuc thi cau lenh ?query , scalar , NonQuery
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int count = 0;

            // Xử lý tìm kiếm
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }

            using (var connection = OpenConnection())
            {
                var sql = @"
                             SELECT COUNT(*) 
                             FROM Products 
                             WHERE (@searchValue = '' OR ProductName LIKE @searchValue)
                             AND (@categoryID = 0 OR CategoryID = @categoryID)
                             AND (@supplierID = 0 OR SupplierID = @supplierID)
                             AND (Price >= @minPrice)
                             AND (@maxPrice = 0 OR Price <= @maxPrice)";

                var parameters = new
                {
                    searchValue = searchValue,
                    categoryID = categoryID,
                    supplierID = supplierID,
                    minPrice = minPrice,
                    maxPrice = maxPrice
                };

                // Sử dụng ExecuteScalar để lấy số lượng sản phẩm
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
            }

            return count;
        }


        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from products where ProductID=@ProductID";
                var parameters = new
                {
                    ProductID = id,
                };
                //thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

      
        public bool DeleteAttribute(long attributeid)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from attributes where AttributeID = @AttributeID";
                var parameters = new
                {
                    AttributeID = attributeid,
                };
                //thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeletePhoto(long photoID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ProductPhotos where PhotoID = @PhotoID";
                var parameters = new
                {
                    PhotoID = photoID,
                };
                //thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Product? Get(int id)
        {
            Product? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select *from Products where ProductID = @ProductID";
                var parameters = new { ProductID = id };
                data = connection.QueryFirstOrDefault<Product>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                //??
                connection.Close();
            }
            return data;
        }


        public ProductAttribute? GetAttribute(long attributeID)
        {
            ProductAttribute? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select *from ProductAttributes where AttributeID = @AttributeID";
                var parameters = new { AttributeID = attributeID };
                data = connection.QueryFirstOrDefault<ProductAttribute>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                //??
                connection.Close();
            }
            return data;
        }

        public ProductPhoto? GetPhoto(long PhotoID)
            {
                ProductPhoto? data = null;
                using (var connection = OpenConnection())
                {
                    var sql = @"select *from ProductPhotos where PhotoID = @PhotoID";
                    var parameters = new { PhotoID = PhotoID };
                    data = connection.QueryFirstOrDefault<ProductPhoto>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                    //??
                    connection.Close();
                }
                return data;
            }

        public bool IsUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"IF EXISTS (SELECT * FROM OrderDetails WHERE ProductID = @ProductID)
                        SELECT 1
                    ELSE 
                        SELECT 0";
                var parameters = new { ProductID = id };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters);
            }
            return result;
        }

        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            List<Product> data = new List<Product>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%"; // Tìm kiếm tương đối

            using (var connection = OpenConnection())
            {
                var sql = @"WITH cte AS
                    (
                        SELECT *,
                            ROW_NUMBER() OVER (ORDER BY ProductName) AS RowNumber
                        FROM Products 
                        WHERE (@searchValue = N'' OR ProductName LIKE @searchValue)
                            AND (@categoryID = 0 OR CategoryID = @categoryID)
                            AND (@supplierID = 0 OR SupplierID = @supplierID)
                            AND (Price >= @minPrice)
                            AND (@maxPrice = 0 OR Price <= @maxPrice)
                    )
                    SELECT * FROM cte
                    WHERE  (@pageSize = 0) 
                        OR (RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)
                    ORDER BY RowNumber";

                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? "",
                    categoryID = categoryID,
                    supplierID = supplierID,
                    minPrice = minPrice,
                    maxPrice = maxPrice
                };

                data = connection.Query<Product>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }

            return data;
        }
        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (var connection = OpenConnection())
            {
                var sql = @"
                            SELECT *
                            FROM ProductAttributes
                            WHERE ProductID = @ProductID
                            ORDER BY DisplayOrder;
        ";
                var parameters = new { ProductID = productID };
                data = connection.Query<ProductAttribute>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();

            }


            return data;
        }



        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();
             
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT *
                            FROM ProductPhotos
                            WHERE ProductID = @ProductID
                            Order by DisPlayOrder;
";
                var parameters = new
                {
                    ProductID = productID
                 
            };
                data = connection.Query<ProductPhoto>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();

            }


            return data;
        }

        

        public bool Update(Product data)
        {
            bool result = false;

            using (var connection = OpenConnection())
            {
                var sql = @"update products 
                            set    ProductName = @ProductName,
                                   ProductDescription = @ProductDescription,
                                   SupplierID = @SupplierID,
                                   CategoryID = @CategoryID,
                                   Unit = @Unit,
                                   Price = @Price,
                                   Photo = @photo,
                                   IsSelling= @IsSelling
                                   where ProductID = @ProductID
";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    ProductName = data.ProductName ?? "",
                    ProductDescription = data.ProductDescription,
                    SupplierID = data.SupplierID,
                    CategoryID = data.CategoryID,
                    Unit = data.unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo ?? "",
                    IsSelling = data.IsSelling
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;

            using (var connection = OpenConnection())
            {
                var sql = @"
                            update ProductAttributes 
                            set    ProductID = @ProductID,
                                   AttributeName = @birthDate,
                                   AttributeValue = @address,
                                   DisPlayOrder = @phone
                                   where AttributeID = @AttributeID
";
                var parameters = new
                {
                    AttributeID = data.AttributeID,
                    ProductID = data.ProductID,
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue,
                    DisPlayOrder = data.DisPlayOrder
                    
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result = false;

            using (var connection = OpenConnection())
            {
                var sql = @"
                            update ProductPhotos 
                            set    ProductID = @ProductID,
                                   Photo = @Photo,
                                   Description = @Description,
                                   DisPlayOrder = @DisPlayOrder,
                                   IsHidden = @IsHidden
                                   where PhotoID = @PhotoID
";
                var parameters = new
                {
                    PhotoID = data.PhotoID,
                    ProductID = data.ProductID,
                    Photo = data.Photo,
                    Description = data.Description ?? "",
                    DisPlayOrder = data.DisPlayOrder,
                    IsHidden = data.IsHidden,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        
    }
}
