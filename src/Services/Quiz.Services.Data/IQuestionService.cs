namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    public interface IQuestionService
    {
        Task<int> Add(string title, int testId);
    }
}
