namespace Quiz.Data.Models
{
    using System.Collections.Generic;

    using Quiz.Data.Common.Models;

    public class Answer : BaseDeletableModel<int>
    {
        public Answer()
        {
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public int Points { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
