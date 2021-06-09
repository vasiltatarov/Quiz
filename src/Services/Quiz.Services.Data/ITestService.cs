namespace Quiz.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Quiz.Services.Data.Models;

    public interface ITestService
    {
        Task<int> Add(string title);

        QuizViewModel GetQuizById(int testId);

        Task<T> GetTestById<T>(int id);

        IEnumerable<UserTestViewModel> GetTestsByUserId(string userId);

        IEnumerable<T> GetTestsByUserId<T>(string id);

        Task StartTest(string userId, int testId);
    }
}
