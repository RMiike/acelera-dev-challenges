namespace Codenation.Challenge
{
    public class State
    {
        public State(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }

        public State(string name, string acronym, double territorialExtension)
        {
            this.Name = name;
            this.Acronym = acronym;
            this.TerritorialExtension = territorialExtension;
        }

        public string Name { get; set; }

        public string Acronym { get; set; }
        public double TerritorialExtension { get; set; }


    }

}
