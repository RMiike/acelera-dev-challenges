using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class CandidateServiceTest
    {
        [Theory]
        [InlineData(1,1,1)]
        [InlineData(2,2,2)]
        [InlineData(3,3,3)]
        public void Should_Be_Right_Candidate_When_Find_By_Id(int userId, int accelerationId, int companyId)
        {
            var fakeContext = new FakeContext("CandidateById");            
            fakeContext.FillWith<Candidate>();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Candidate>().
                    Find(x => x.UserId == userId && 
                              x.AccelerationId == accelerationId && 
                              x.CompanyId == companyId);

                var service = new CandidateService(context);
                var actual = service.FindById(userId, accelerationId, companyId);

                Assert.Equal(expected, actual, new CandidateIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Candidates_When_Find_By_Company_Id(int companyId)
        {
            var fakeContext = new FakeContext("CandidaterByCompany");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Company>().                
                    Where(company => company.Id == companyId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        company => company.Id, 
                        candidate => candidate.CompanyId, 
                        (company, candidate) => candidate).
                    ToList();

                var service = new CandidateService(context);                
                var actual = service.FindByCompanyId(companyId);

                Assert.Equal(expected, actual, new CandidateIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Candidates_When_Find_By_Accelaration_Id(int accelerationId)
        {
            var fakeContext = new FakeContext("CandidateByAcceleration");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Acceleration>().
                    Where(acceleration => acceleration.Id == accelerationId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        acceleration => acceleration.Id, 
                        candidate => candidate.AccelerationId, 
                        (acceleration, candidate) => candidate).
                    ToList();

                var service = new CandidateService(context);                
                var actual = service.FindByAccelerationId(accelerationId);

                Assert.Equal(expected, actual, new CandidateIdComparer());
            }
        }

        [Fact]
        public void Should_Add_New_Candidate_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewCandidate");
            // precisa ser uma tupla que não está no arquivo candidates.json
            var expected = new Candidate() {
                UserId = 5, 
                AccelerationId = 1,
                CompanyId = 1,
                Status = 99
            };

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new CandidateService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected, actual, new CandidateIdComparer());
            }
        }

        [Fact]
        public void Should_Update_The_Candidate_When_Save()
        {
            var fakeContext = new FakeContext("SaveCandidate");
            fakeContext.FillWith<Candidate>();

            var expected = fakeContext.GetFakeData<Candidate>().First();
            expected.Status = 999;
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new CandidateService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected, actual, new CandidateIdComparer());
                Assert.Equal(expected.Status, actual.Status);                
            }
        }

    }
}
