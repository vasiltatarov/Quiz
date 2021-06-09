namespace Quiz.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Quiz.Services.Data;

    public class TestsController : Controller
    {
        private const string TestsDirectory = "../../../tests/";
        private const string FileExtension = ".json";

        private readonly IJsonImportService jsonImportService;

        public TestsController(IJsonImportService jsonImportService)
        {
            this.jsonImportService = jsonImportService;
        }

        public async Task<IActionResult> AddTest()
        {
            var files = this.GetTestNames(Directory.GetFiles(TestsDirectory));

            foreach (var file in files)
            {
                var filePath = TestsDirectory + file;
                var fileName = file.Replace(FileExtension, string.Empty);
                await this.jsonImportService.Import(filePath, fileName);
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
