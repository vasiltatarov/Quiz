namespace Quiz.Data.Models
{
    using System.Collections.Generic;

    using Quiz.Data.Common.Models;

    public class Test : BaseDeletableModel<int>
    {
        public Test()
        {
            this.Questions = new HashSet<Question>();
        }

        public string Title { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
