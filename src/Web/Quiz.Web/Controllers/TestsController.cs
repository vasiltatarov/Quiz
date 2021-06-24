namespace Quiz.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Quiz.Data.Models;
    using Quiz.Services.Data;
    using Quiz.Web.ViewModels.Tests;

    using static Quiz.Common.GlobalConstants;

    [Authorize]
    public class TestsController : Controller
    {
        private readonly IJsonImportService jsonImportService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITestService testService;
        private readonly IUserAnswerService userAnswerService;
        private readonly IUserTestService userTestService;

        public TestsController(
            IJsonImportService jsonImportService,
            UserManager<ApplicationUser> userManager,
            ITestService testService,
            IUserAnswerService userAnswerService,
            IUserTestService userTestService)
        {
            this.jsonImportService = jsonImportService;
            this.userManager = userManager;
            this.testService = testService;
            this.userAnswerService = userAnswerService;
            this.userTestService = userTestService;
        }

        public IActionResult Add()
            => this.View(new TestFormModel());

        [HttpPost]
        public IActionResult Add(TestFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var title = model.Title;
            var json = JsonConvert.SerializeObject(model.Questions, Formatting.Indented);
            var filePath = $"{TestsDirectory}{title}{JsonFileExtension}";

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, json);
            }

            return this.RedirectToAction("Index", "Home");
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
            await this.userTestService.Add(user.Id, testId);

            foreach (var item in this.Request.Form)
            {
                if (item.Key != "__RequestVerificationToken")
                {
                    var questionId = int.Parse(item.Key.Replace("q_", string.Empty));
                    var answerId = int.Parse(item.Value);
                    await this.userAnswerService.Add(user.Id, questionId, answerId);
                }
            }

            return this.RedirectToAction("Result", new { testId });
        }

        public async Task<IActionResult> Result(int testId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.testService.GetTestResult(user.Id, testId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> AddTestsFromFiles()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var files = this.GetTestNames(Directory.GetFiles(TestsDirectory));

            foreach (var file in files)
            {
                var filePath = TestsDirectory + file;
                var fileName = file.Replace(JsonFileExtension, string.Empty);
                await this.jsonImportService.Import(filePath, fileName, user.Id);
            }

            return this.RedirectToAction("Index", "Home");
        }

        private List<string> GetTestNames(string[] files)
            => files
                .Select(x => x.Replace(TestsDirectory, string.Empty))
                .ToList();
    }
}
