namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    public interface IUserAnswerService
    {
        Task Add(string userId, int testId, int answerId);

        Task<int> GetUserResult(string userId, int testId);
    }
}
