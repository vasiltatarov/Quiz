namespace Quiz.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;
    using Quiz.Services.Data.Models;

    public class UserTestService : IUserTestService
    {
        private readonly IRepository<UserTest> userTestRepository;
        private readonly IUserAnswerService userAnswerService;
        private readonly IQuestionService questionService;

        public UserTestService(IRepository<UserTest> userTestRepository, IUserAnswerService userAnswerService, IQuestionService questionService)
        {
            this.userTestRepository = userTestRepository;
            this.userAnswerService = userAnswerService;
            this.questionService = questionService;
        }

        public async Task Add(string userId, int testId)
        {
            var userTest = await this.userTestRepository.All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TestId == testId);

            if (userTest == null)
            {
                userTest = new UserTest
                {
                    UserId = userId,
                    TestId = testId,
                    CreatedOn = DateTime.UtcNow,
                };
                await this.userTestRepository.AddAsync(userTest);
            }
            else
            {
                userTest.CreatedOn = DateTime.UtcNow;
            }

            await this.userTestRepository.SaveChangesAsync();
        }

        public IEnumerable<StatsUserTestViewModel> GetAllByUserId(string userId)
            => this.userTestRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => new StatsUserTestViewModel
                {
                    Title = x.Test.Title,
                    CreatedOn = x.CreatedOn,
                    Result = this.userAnswerService.GetUserResult(userId, x.TestId).Result,
                    Questions = this.questionService.GetQuestionCountByTestId(x.TestId).Result,
                })
                .ToList();
    }
}
