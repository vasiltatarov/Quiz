namespace Quiz.Web.ViewModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    public class TestFormModel
    {
        [Required]
        public string Title { get; set; }

        public QuestionFormModel[] Questions { get; set; } = new QuestionFormModel[10];
    }
}
