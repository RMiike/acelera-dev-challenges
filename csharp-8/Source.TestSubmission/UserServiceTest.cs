using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;

namespace Codenation.Challenge
{
    public class UserServiceTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Return_Right_User_When_Find_By_Id(int id)
        {
            var fakeContext = new FakeContext("UserById");            
            fakeContext.FillWith<User>();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<User>().Find(x => x.Id == id);

                var service = new UserService(context);                
                var actual = service.FindById(id);

                Assert.Equal(expected, actual, new UserIdComparer());
            }
        }

        [Theory]
        [InlineData("Velvet Grass")]
        [InlineData("Progesterone")]
        public void Should_Be_Right_Users_When_Find_By_Accelaration_Name(string accelerationName)
        {
            var fakeContext = new FakeContext("UserByAcceleration");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Acceleration>().
                    Where(acceleration => acceleration.Name == accelerationName).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        acceleration => acceleration.Id, 
                        candidate => candidate.AccelerationId, 
                        (acceleration, candidate) => candidate).
                    Join(fakeContext.GetFakeData<User>(),
                        candidate => candidate.UserId,
                        user => user.Id,
                        (candidate, user) => user).
                    Distinct().
                    ToList();

                var service = new UserService(context);                
                var actual = service.FindByAccelerationName(accelerationName);

                Assert.Equal(expected, actual, new UserIdComparer());
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Should_Be_Right_Users_When_Find_By_Company_Id(int companyId)
        {
            var fakeContext = new FakeContext("UserByCompany");            
            fakeContext.FillWithAll();

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var expected = fakeContext.GetFakeData<Company>().                
                    Where(company => company.Id == companyId).
                    Join(fakeContext.GetFakeData<Candidate>(), 
                        company => company.Id, 
                        candidate => candidate.CompanyId, 
                        (company, candidate) => candidate).                    
                    Join(fakeContext.GetFakeData<User>(),
                        candidate => candidate.UserId,
                        user => user.Id,
                        (candidate, user) => user).
                    Distinct().
                    ToList();

                var service = new UserService(context);                
                var actual = service.FindByCompanyId(companyId);

                Assert.Equal(expected, actual, new UserIdComparer());
            }
        }

        [Fact]
        public void Should_Add_New_User_When_Save()
        {
            var fakeContext = new FakeContext("SaveNewUser");
            var fakeUser = fakeContext.GetFakeData<User>().First();
            fakeUser.Id = 0;

            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new UserService(context);
                var actual = service.Save(fakeUser);

                Assert.NotEqual(0, actual.Id);
            }
        }

        [Fact]
        public void Should_Update_The_User_When_Save()
        {
            var fakeContext = new FakeContext("SaveUser");
            fakeContext.FillWithAll();

            var expected = fakeContext.GetFakeData<User>().First();
            expected.FullName = "new value";
            expected.Email = "new value";
            expected.Nickname = "new value";
            expected.Password = "new pass";
            
            using (var context = new CodenationContext(fakeContext.FakeOptions))
            {
                var service = new UserService(context);
                var actual = service.Save(expected);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FullName, actual.FullName);
                Assert.Equal(expected.Nickname, actual.Nickname);
                Assert.Equal(expected.Email, actual.Email);
                Assert.Equal(expected.Password, actual.Password);
            }
        }

    }
}
