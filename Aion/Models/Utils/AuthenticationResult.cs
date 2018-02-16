using System.Web;

namespace Aion.Models.Utils
{
    //Authentication result details container
    public class AuthenticationResult
    {
        public string UserName
        {
            get
            {
                return HttpContext.Current.Session["_UserName"].ToString();
            }
            set
            {
                HttpContext.Current.Session.Add("_UserName", value);
            }
        }
        public string EmpNum => HttpContext.Current.Session["_EmpNum"].ToString();
        public string ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);
    }
}