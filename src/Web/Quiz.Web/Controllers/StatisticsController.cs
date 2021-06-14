namespace Quiz.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quiz.Data.Models;
    using Quiz.Services.Data;

    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserTestService userTestService;

        public StatisticsController(UserManager<ApplicationUser> userManager, IUserTestService userTestService)
        {
            this.userManager = userManager;
            this.userTestService = userTestService;
        }

        public async Task<IActionResult> UserStatistics()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.userTestService.GetAllByUserId(user.Id);

            return this.View(viewModel);
        }

        public IActionResult TestStatistics(int testId)
        {
            var viewModel = this.userTestService.GetAllByTestId(testId);
            return this.View(viewModel);
        }
    }
}
