namespace Quiz.Data.Models
{
    using Quiz.Data.Common.Models;

    public class UserAnswer : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
