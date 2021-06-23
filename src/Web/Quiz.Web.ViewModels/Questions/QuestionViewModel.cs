namespace Quiz.Web.ViewModels.Questions
{
    using System.Collections.Generic;

    using Quiz.Web.ViewModels.Answers;

    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
