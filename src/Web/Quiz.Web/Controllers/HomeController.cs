namespace Quiz.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quiz.Data.Models;
    using Quiz.Services.Data;
    using Quiz.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly ITestService testService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ITestService testService, UserManager<ApplicationUser> userManager)
        {
            this.testService = testService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userTests = this.testService.GetTestsByUserId(user.Id);

            return this.View(userTests);
        }

        public IActionResult Privacy() => this.View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
