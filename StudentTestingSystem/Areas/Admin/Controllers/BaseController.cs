using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected string GetCurrentUserName()
        {
            return this.User.Identity.GetUserName();
        }

        protected string GetCurrentUserId()
        {
            return this.User.Identity.GetUserId();
        }

        protected bool IsUserExistInRole(string role)
        {
            return this.User.IsInRole(role);
        }
    }
}