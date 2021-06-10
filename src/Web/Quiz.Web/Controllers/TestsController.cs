namespace Quiz.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Quiz.Data.Models;
    using Quiz.Services.Data;
    using Quiz.Web.ViewModels.Tests;

    [Authorize]
    public class TestsController : Controller
    {
        private const string TestsDirectory = "../../../tests/";
        private const string FileExtension = ".json";

        private readonly IJsonImportService jsonImportService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITestService testService;
        private readonly IUserAnswerService userAnswerService;

        public TestsController(
            IJsonImportService jsonImportService,
            UserManager<ApplicationUser> userManager,
            ITestService testService,
            IUserAnswerService userAnswerService)
        {
            this.jsonImportService = jsonImportService;
            this.userManager = userManager;
            this.testService = testService;
            this.userAnswerService = userAnswerService;
        }

        public async Task<IActionResult> Start(int testId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.testService.StartTest(user.Id, testId);

            var testViewModel = this.testService.GetTestById(testId);

            return this.View(testViewModel);
        }

        public async Task<IActionResult> Submit(int testId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            foreach (var item in this.Request.Form)
            {
                var questionId = int.Parse(item.Key.Replace("q_", string.Empty));
                var answerId = int.Parse(item.Value);
                await this.userAnswerService.Add(user.Id, questionId, answerId);
            }

            return this.RedirectToAction("Result", new { testId });
        }

        public async Task<IActionResult> Result(int testId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var points = await this.userAnswerService.GetUserResult(user.Id, testId);

            var viewModel = new ResultViewModel { Points = points };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Add()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var files = this.GetTestNames(Directory.GetFiles(TestsDirectory));

            foreach (var file in files)
            {
                var filePath = TestsDirectory + file;
                var fileName = file.Replace(FileExtension, string.Empty);
                await this.jsonImportService.Import(filePath, fileName, user.Id);
            }

            return this.Ok();
        }

        private List<string> GetTestNames(string[] files)
        {
            var tests = new List<string>();

            foreach (var file in files)
            {
                var fileName = file.Replace(TestsDirectory, string.Empty);
                tests.Add(fileName);
            }

            return tests;
        }
    }
}
