using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Aion.Models.Utils
{
    public class LoginVm
    {
        [Required, AllowHtml]
        public string Username { get; set; }

        [Required, AllowHtml, DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string ReturnURL { get; set; }
        
    }
}