namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    public interface IUserTestService
    {
        Task Add(string userId, int testId);
    }
}
