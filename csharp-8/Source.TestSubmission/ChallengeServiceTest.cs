using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class ChallengeServiceTest
    {       
        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(3,3)]
        public void Should_Be_Right_Challenges_When_Find_By_Accelartion_And_User(int accelerationId, int userId)
        {
            var fakeContext = new FakeContext("ChallengeByAccelerationAndUser");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<User>().
                    Where(user => user.Id == userId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        user => user.Id, 
                        candidate => candidate.UserId, 
                        (user, candidate) => candidate).
                    Join(fakeContext.GetFakeData<Acceleration>(),
                        candidate => candidate.AccelerationId,
                        acceleration => acceleration.Id,
                        (candidate, acceleration) => acceleration).
                    Where(acceleration => acceleration.Id == accelerationId).
                    Join(fakeContext.GetFakeData<Models.Challenge>(),
                        acceleration => acceleration.ChallengeId,
                        challenge => challenge.Id,
                        (acceleration, challenge) => challenge).
                    Distinct().
                    ToList();

                var service = new ChallengeService(context);                
                var actual = service.FindByAccelerationIdAndUserId(userId, accelerationId);

                Assert.Equal(expected, actual, new ChallengeIdComparer());
            }
        }

        [Fact]
        public void Should_Add_New_Challenge_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewChallenge");
            var fakeChallenge = fakeContext.GetFakeData<Models.Challenge>().First();
            fakeChallenge.Id = 0;

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new ChallengeService(context);
                var actual = service.Save(fakeChallenge);

                Assert.NotEqual(0, actual.Id);
            }
        }

        [Fact]
        public void Should_Update_The_Challenge_When_Save()
        {
            var fakeContext = new FakeContext("SaveChallenge");
            fakeContext.FillWith<Models.Challenge>();

            var expected = fakeContext.GetFakeData<Models.Challenge>().First();
            expected.Name = "new value";
            expected.Slug = "new value";
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new ChallengeService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Slug, actual.Slug);
            }
        }

    }
}
