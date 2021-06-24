namespace Quiz.Web.ViewModels.Tests
{
    public class QuestionFormModel
    {
        public string Question { get; set; }

        public AnswerFormModel[] Answers { get; set; } = new AnswerFormModel[4];
    }
}
