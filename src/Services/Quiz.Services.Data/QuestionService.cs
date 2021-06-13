using Microsoft.EntityFrameworkCore;

namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;

    public class QuestionService : IQuestionService
    {
        private readonly IDeletableEntityRepository<Question> questions;

        public QuestionService(IDeletableEntityRepository<Question> questions)
        {
            this.questions = questions;
        }

        public async Task<int> Add(string title, int testId)
        {
            var question = new Question
            {
                Title = title,
                TestId = testId,
            };
            await this.questions.AddAsync(question);
            await this.questions.SaveChangesAsync();

            return question.Id;
        }

        public Task<int> GetQuestionCountByTestId(int testId)
            => this.questions.AllAsNoTracking()
                .CountAsync(x => x.TestId == testId);
    }
}
