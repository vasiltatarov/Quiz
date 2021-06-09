namespace Quiz.Services.Data
{
    using System.Threading.Tasks;

    public interface IJsonImportService
    {
        Task Import(string fileName, string quizName);
    }
}
