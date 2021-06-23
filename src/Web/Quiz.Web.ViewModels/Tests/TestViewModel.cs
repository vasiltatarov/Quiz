namespace Quiz.Web.ViewModels.Tests
{
    using System.Collections.Generic;

    using Quiz.Web.ViewModels.Questions;

    public class TestViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}
