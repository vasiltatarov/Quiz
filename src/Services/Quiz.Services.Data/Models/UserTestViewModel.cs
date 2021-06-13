namespace Quiz.Services.Data.Models
{
    public class UserTestViewModel
    {
        public int TestId { get; set; }

        public string Title { get; set; }

        public int Participants { get; set; }

        public TestStatus Status { get; set; }
    }
}
