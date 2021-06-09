namespace Quiz.Services.Data.Models
{
    using System.Collections.Generic;

    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
