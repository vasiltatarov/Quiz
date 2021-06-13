namespace Quiz.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Quiz.Services.Data.Models;

    public interface IUserTestService
    {
        Task Add(string userId, int testId);

        IEnumerable<StatsUserTestViewModel> GetAllByUserId(string userId);
    }
}
