namespace Quiz.Web.ViewModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    public class QuestionFormModel
    {
        [Required]
        public string Question { get; set; }

        public AnswerFormModel[] Answers { get; set; } = new AnswerFormModel[4];
    }
}
