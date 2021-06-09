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

    [Authorize]
    public class TestsController : Controller
    {
        private const string TestsDirectory = "../../../tests/";
        private const string FileExtension = ".json";

        private readonly IJsonImportService jsonImportService;
        private readonly UserManager<ApplicationUser> userManager;

        public TestsController(IJsonImportService jsonImportService, UserManager<ApplicationUser> userManager)
        {
            this.jsonImportService = jsonImportService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddTest()
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
