namespace Quiz.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Quiz.Data.Common.Repositories;
    using Quiz.Data.Models;

    public class UserTestService : IUserTestService
    {
        private readonly IRepository<UserTest> userTestRepository;

        public UserTestService(IRepository<UserTest> userTestRepository)
        {
            this.userTestRepository = userTestRepository;
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
    }
}
