using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumlekCoffeeBE.Base.Enum
{
    public enum Errors
    {
        #region Error for user
        [Description("Đăng Nhập thất bại.")]
        ER001,

        [Description("User name, Password không đúng.")]
        ER002,

        [Description("Access Token lỏ.")]
        ER003,

        [Description("User không có quyền truy cập.")]
        ER004,

        [Description("Tạo user không thành công.")]
        ER005,

        [Description("user éo có quyền.")]
        ER006,
        #endregion
    }
}
