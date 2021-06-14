namespace Quiz.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Quiz.Services.Data.Models;
    using Quiz.Web.ViewModels.Statistics;

    public interface IUserTestService
    {
        Task Add(string userId, int testId);

        IEnumerable<UserTestStatsViewModel> GetAllByUserId(string userId);

        TestStatsViewModel GetAllByTestId(int testId);
    }
}
