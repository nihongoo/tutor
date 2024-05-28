using Microsoft.AspNetCore.Mvc;

namespace tutor.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string username, string passwold)//http method: get
        {
            if (username == null && passwold == null) { return View(); }
            else
            {
                //kiểm tra tài khoản
                if(username != "admin" && passwold != "K")
                {
                    HttpContext.Session.SetString("username", username);
                    TempData["login"] = $"chào mừng {username} đã đăng nhập với quyền khánh";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
					HttpContext.Session.SetString("username", username);
					TempData["login"] = $"chào mừng {username} đã đăng nhập với quyền admin";
                    return RedirectToAction("Index", "Home");
                }
            }
        }

    }
}
