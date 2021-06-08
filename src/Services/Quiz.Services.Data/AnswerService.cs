namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;

    public class AnswerService : IAnswerService
    {
        private readonly IDeletableEntityRepository<Answer> answers;

        public AnswerService(IDeletableEntityRepository<Answer> answers)
        {
            this.answers = answers;
        }

        public async Task<int> Add(string title, bool isCorrect, int points, int questionId)
        {
            var answer = new Answer
            {
                Title = title,
                IsCorrect = isCorrect,
                Points = points,
                QuestionId = questionId,
            };
            await this.answers.AddAsync(answer);
            await this.answers.SaveChangesAsync();

            return answer.Id;
        }
    }
}
