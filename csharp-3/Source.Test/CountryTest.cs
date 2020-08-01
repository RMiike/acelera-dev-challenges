using System;
using Xunit;

namespace Codenation.Challenge
{
    public class CountryTest
    {

        [Fact]
        public void Should_Return_Top_10_States_By_Area()
        {
            var statesByArea = new State[] {
                new State("Amazonas", "AM"),
                new State("Pará", "PA"),
                new State("Mato Grosso", "MT"),
                new State("Minas Gerais", "MG"),
                new State("Bahia", "BA"),
                new State("Mato Grosso do Sul", "MS"),
                new State("Goiás", "GO"),
                new State("Maranhão", "MA"),
                new State("Rio Grande do Sul", "RS"),
                new State("Tocantins", "TO")
            };
            var country = new Country();
            Assert.Equal(
                statesByArea,
                country.Top10StatesByArea(),
                new StateAcronymComparer());
        }
    }
}
