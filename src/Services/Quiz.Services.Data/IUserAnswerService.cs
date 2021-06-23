namespace Quiz.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Quiz.Data.Models;

    public interface IUserAnswerService
    {
        Task Add(string userId, int questionId, int answerId);

        Task<int> GetUserPoints(string userId, int testId);
    }
}
