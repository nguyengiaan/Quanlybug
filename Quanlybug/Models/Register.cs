using Microsoft.Build.Framework;
using Xunit.Sdk;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Quanlybug.Models
{
    public class Register
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập Tên")]
        public string? NameUser { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string? Account { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string? Password { get; set; }
     
    }
}
