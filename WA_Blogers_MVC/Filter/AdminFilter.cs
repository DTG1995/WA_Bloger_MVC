using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WA_Blogers_MVC.Filter
{
    public class AdminFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Không có quyền truy cập!"
                };

            }
        }
    }
}