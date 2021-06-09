namespace Quiz.Services.Data.Models
{
    using System.Collections.Generic;

    public class QuizViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
