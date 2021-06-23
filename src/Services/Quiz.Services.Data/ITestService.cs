namespace Quiz.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Quiz.Services.Data.Models;
    using Quiz.Web.ViewModels.Tests;

    public interface ITestService
    {
        Task<int> Add(string title, string creatorId);

        TestViewModel GetTestById(int testId);

        TestViewModel GetTestResult(string userId, int testId);

        Task<T> GetTestById<T>(int id);

        IEnumerable<UserTestViewModel> GetTestsByUserId(string userId);

        IEnumerable<T> GetTestsByUserId<T>(string id);

        Task StartTest(string userId, int testId);
    }
}
