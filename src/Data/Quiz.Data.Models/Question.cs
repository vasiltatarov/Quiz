namespace Quiz.Data.Models
{
    using System.Collections.Generic;

    using Quiz.Data.Common.Models;

    public class Question : BaseDeletableModel<int>
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
            this.UserAnswers = new HashSet<UserAnswer>();
        }

        public string Title { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
