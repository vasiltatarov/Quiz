namespace Quiz.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StatisticsController : Controller
    {
        public StatisticsController()
        {
        }

        public IActionResult UserStatistics()
        {
            return this.View();
        }
    }
}
