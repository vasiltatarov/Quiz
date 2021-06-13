namespace Quiz.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Quiz.Data.Models;

    public class UserTestConfiguration : IEntityTypeConfiguration<UserTest>
    {
        public void Configure(EntityTypeBuilder<UserTest> userTest)
        {
            userTest.HasKey(x => new { x.TestId, x.UserId });

            userTest.HasOne(x => x.User)
                .WithMany(x => x.UserTests)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            userTest.HasOne(x => x.Test)
                .WithMany(x => x.UserTests)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
