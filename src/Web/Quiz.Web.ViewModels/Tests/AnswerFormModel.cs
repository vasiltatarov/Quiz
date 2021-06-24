namespace Quiz.Web.ViewModels.Tests
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerFormModel
    {
        [Required]
        public string Answer { get; set; }

        public bool Correct { get; set; }
    }
}
