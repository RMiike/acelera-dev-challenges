using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class AccelerationServiceTest
    {       
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Acceleration_When_Find_By_Id(int id)
        {
            var fakeContext = new FakeContext("AccelerationById");            
            fakeContext.FillWith<Acceleration>();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Acceleration>().Find(x => x.Id == id);

                var service = new AccelerationService(context);                
                var actual = service.FindById(id);

                Assert.Equal(expected, actual, new AccelerationIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Should_Be_Right_Accelerations_When_Find_By_Company_Id(int companyId)
        {
            var fakeContext = new FakeContext("AccelerationByCompany");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Company>().
                    Where(company => company.Id == companyId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        company => company.Id, 
                        candidate => candidate.CompanyId, 
                        (company, candidate) => candidate).
                    Join(fakeContext.GetFakeData<Acceleration>(),
                        candidate => candidate.AccelerationId,
                        acceleration => acceleration.Id,
                        (candidate, acceleration) => acceleration).
                    Distinct().
                    ToList();

                var service = new AccelerationService(context);                
                var actual = service.FindByCompanyId(companyId);

                Assert.Equal(expected, actual, new AccelerationIdComparer());
            }
        }

        [Fact]
        public void Should_Add_New_Acceleration_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewAcceleration");
            var fakeAcceleration = fakeContext.GetFakeData<Acceleration>().First();
            fakeAcceleration.Id = 0;
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new AccelerationService(context);
                var actual = service.Save(fakeAcceleration);

                Assert.NotEqual(0, actual.Id);
            }
        }

        [Fact]
        public void Should_Update_The_Acceleration_When_Save()
        {
            var fakeContext = new FakeContext("SaveAcceleration");
            fakeContext.FillWith<Acceleration>();

            var expected = fakeContext.GetFakeData<Acceleration>().First();
            expected.Name = "new value";
            expected.Slug = "new value";
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new AccelerationService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Slug, actual.Slug);
            }
        }

    }
}
