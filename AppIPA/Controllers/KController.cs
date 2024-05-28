using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppIPA.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class KController : ControllerBase
	{
		[HttpGet("API-Tai-Xiu")]
		public ActionResult<string> FB88(int betNumber, int betMoney)
		{
			Random r = new Random();
			int result = r.Next(100000,999999);
			if ((result - betNumber) % 100 == 0)
			{
				string kq = $"Bạn rất may mắn, đã trúng{betMoney * 70}";
				return Ok(kq);
			}
			else return BadRequest($"Chúc bạn may mắn lần sau, kết quả là {result}");
		}
		[HttpGet("Get-all-food")]
		public ActionResult<string> GetAllFood()
		{
			List<Food> foodList = new List<Food>()
			{
				new Food (){Name = "rau tơ", Price= 99},
				new Food (){Name = "Bân xiển", Price= 99},
				new Food (){Name = "K", Price= 99},
			};
			return Ok(foodList); 
		}
	}
	public class Food
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
	}
}
