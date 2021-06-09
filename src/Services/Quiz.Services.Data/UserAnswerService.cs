namespace Quiz.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;

    public class UserAnswerService : IUserAnswerService
    {
        private readonly IDeletableEntityRepository<UserAnswer> userAnswers;

        public UserAnswerService(IDeletableEntityRepository<UserAnswer> userAnswers)
        {
            this.userAnswers = userAnswers;
        }

        public async Task Add(string userId, int testId, int answerId)
        {
            var userAnswer = await this.userAnswers.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TestId == testId);

            if (userAnswer == null)
            {
                return;
            }

            userAnswer.AnswerId = answerId;
            await this.userAnswers.SaveChangesAsync();
        }

        public Task<int> GetUserResult(string userId, int testId)
            => this.userAnswers.AllAsNoTracking()
                .Where(x => x.UserId == userId && x.TestId == testId)
                .SumAsync(x => x.Answer.Points);
    }
}
