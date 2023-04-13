using TestOggettiBanca.Abstract;

namespace TestOggettiBanca
{
    public abstract class Bank : FinancialIntermediary
    {
        protected long _code; //> -> BANK
        protected string _name;
        protected string _country;

        public string Name { get => _name; }
        public string Country { get => _country; }

        public Bank(string name, string Country, string city) : base(name, Country, city)
        {
            _name = name;
            _country = Country;
        }

        public virtual bool Transfer(string nameFiat, string CF, Bank to, FIATDespositRequest data)
        {
            return false;
        }



     

    }
}

