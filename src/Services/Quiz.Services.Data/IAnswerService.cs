namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    public interface IAnswerService
    {
        Task<int> Add(string title, bool isCorrect, int points, int questionId);
    }
}
