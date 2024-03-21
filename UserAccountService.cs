using SV20T1020451.DataLayers.SQLServer;
using SV20T1020451.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020451.BusinessLayers
{
    public static class UserAccountService
    {
        private static readonly EmployeeAccountDAL EmployeeAccountDB;
        /// <summary>
        /// ctor( static constructor hoạt động như thế nào ? cách viết?)
        /// </summary>
        static UserAccountService()
        {
            string connectionString = Configuration.ConnectionString;
            EmployeeAccountDB = new EmployeeAccountDAL(connectionString);

        }
        public static UserAccount? Authorize(string userName, string password)
        {
            return EmployeeAccountDB.Authorize(userName, password);
        }
        /// &lt;summary&gt;
        /// Đổi mật khẩu
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;userName&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;oldPassword&quot;&gt;&lt;/param&gt;
        /// &lt;param name=&quot;newPassword&quot;&gt;&lt;/param&gt;
        /// &lt;returns&gt;&lt;/returns&gt;
        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return EmployeeAccountDB.ChangePassword(userName, oldPassword, newPassword);

        }

    }
}
