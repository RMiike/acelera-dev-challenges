using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class CompanyServiceTest
    {       
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Company_When_Find_By_Id(int id)
        {
            var fakeContext = new FakeContext("CompanyById");            
            fakeContext.FillWith<Company>();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Company>().Find(x => x.Id == id);

                var service = new CompanyService(context);                
                var actual = service.FindById(id);

                Assert.Equal(expected, actual, new CompanyIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Should_Be_Right_Companies_When_Find_By_Accelaration_Id(int accelerationId)
        {
            var fakeContext = new FakeContext("CompanyByAcceleration");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Acceleration>().
                    Where(acceleration => acceleration.Id == accelerationId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        acceleration => acceleration.Id, 
                        candidate => candidate.AccelerationId, 
                        (acceleration, candidate) => candidate).
                    Join(fakeContext.GetFakeData<Company>(),
                        candidate => candidate.CompanyId,
                        company => company.Id,
                        (candidate, company) => company).
                    Distinct().
                    ToList();

                var service = new CompanyService(context);                
                var actual = service.FindByAccelerationId(accelerationId);

                Assert.Equal(expected, actual, new CompanyIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Companies_When_Find_By_User_Id(int userId)
        {
            var fakeContext = new FakeContext("CompanyByUser");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<User>().
                    Where(user => user.Id == userId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        user => user.Id, 
                        candidate => candidate.UserId, 
                        (user, candidate) => candidate).                    
                    Join(fakeContext.GetFakeData<Company>(),
                        candidate => candidate.CompanyId,
                        company => company.Id,
                        (candidate, company) => company).
                    Distinct().
                    ToList();

                var service = new CompanyService(context);                
                var actual = service.FindByUserId(userId);

                Assert.Equal(expected, actual, new CompanyIdComparer());
            }
        }

        [Fact]
        public void Should_Add_New_Company_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewCompany");
            var fakeCompany = fakeContext.GetFakeData<Company>().First();
            fakeCompany.Id = 0;

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new CompanyService(context);
                var actual = service.Save(fakeCompany);

                Assert.NotEqual(0, actual.Id);
            }
        }

        [Fact]
        public void Should_Update_The_Company_When_Save()
        {
            var fakeContext = new FakeContext("SaveCompany");
            fakeContext.FillWith<Company>();

            var expected = fakeContext.GetFakeData<Company>().First();
            expected.Name = "new value";
            expected.Slug = "new value";
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new CompanyService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Slug, actual.Slug);
            }
        }

    }
}
