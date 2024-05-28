using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tutor.Models;
using Newtonsoft.Json;

namespace tutor.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			ViewData["acc"] = HttpContext.Session.GetString("username");
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult VuaDonLinh(string cc, string cn)
		{
			var username = HttpContext.Session.GetString("username");
			if (username == null)
			{
				TempData["login"] = "Please log in to calculate BMI.";
				return RedirectToAction("Index");
			}

			if (double.TryParse(cc, out double height) && double.TryParse(cn, out double weight) && height > 0)
			{
				double bmi = weight / (height * height);
				ViewData["kq"] = $"Bạn {username} cao {height}m với cân nặng {weight}kg sẽ có BMI là: {bmi:F2}";
			}
			else
			{
				ViewData["kq"] = "Invalid input. Please enter valid numbers for height and weight.";
			}

			return RedirectToAction("Index");
		}
		public IActionResult BocBatHo(int m, long mn, double lai)
		{
			if (m == 0 && mn == 0 && lai == 0)
			{
				string a = "bạn chưa nhập gì cả";
				HttpContext.Session.SetString("laiKep", a);
			}
			else if (m < 0 || mn < 0 || lai < 0)
			{
				string a = "dữ liệu nhập vào không hợp lệ";
				HttpContext.Session.SetString("laiKep", a);
			}
			else
			{
				string request = $@"https://localhost:7014/WeatherForecast/boc-bat-ho?month={m}&money={mn}&lai={lai}";
				HttpClient http = new HttpClient();
				string response = http.GetStringAsync(request).Result;
				HttpContext.Session.SetString("laiKep", response);
			}
			return RedirectToAction("Index");
		}
		public IActionResult PtBacHai(double a, double b, double c)
		{
			string request = $@"https://localhost:7014/WeatherForecast/pt-bac-hai?a={a}&b={b}&c={c}";
			HttpClient http = new HttpClient();
			string response = http.GetStringAsync(request).Result;
			ViewData["timX"] = response;
			return View();
		}
		public IActionResult CheckSNT(int a) {
			string request = $@"https://localhost:7014/WeatherForecast/chovy-ca-mau?a={a}";
			HttpClient http = new HttpClient();
			string response = http.GetStringAsync(request).Result;
			ViewData["SNT"] = response;
			return View();
		}
		public IActionResult Food()
		{
			string rq = $@"https://localhost:7163/api/K/Get-all-food";
			HttpClient http = new HttpClient();
			var response = http.GetStringAsync(rq).Result; 
			var data = JsonConvert.DeserializeObject<List<Food>>(response);
			return View(data);

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}