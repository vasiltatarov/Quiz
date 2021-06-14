namespace Quiz.Web.ViewModels.Statistics
{
    using System.Collections.Generic;

    public class TestStatsViewModel
    {
        public string Title { get; set; }

        public int Questions { get; set; }

        public int Participants { get; set; }

        public IEnumerable<TestStatsUserViewModel> Users { get; set; }
    }
}
