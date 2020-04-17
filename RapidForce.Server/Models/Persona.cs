using System;

namespace RapidForce
{
    public class Persona
    {
        public int LocalId { get; private set; }
        public License License { get; private set; }

        public Persona(int localId, Common.Sex sex)
        {
            License = new License(localId, sex);
        }
    }

    public class License
    {
        public string LicenseNumber { get; private set; }
        public string Class { get; private set; }
        public DateTime DOB { get; private set; }
        public DateTime Issuance { get; private set; }
        public DateTime Expiration { get; private set; }
        public string Sex { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string AddressStreet { get; private set; }
        public string AddressTown { get; private set; }
        public readonly string AddressState = "SA 90000";
        public readonly string Restriction = "NONE";
        public string Hair { get; private set; } // totally random..
        public string Eyes { get; private set; } // totally random..
        public string Height { get; private set; }
        public string Weight { get; private set; }

        //-- constants:
        private readonly static string[] classes = new[] { "A", "B", "C", "M1", "M2" };
        private readonly static string[] streetTypes = new[] { "ROAD", "WAY", "STREET", "AVENUE", "BOULEVARD", "LANE", "DRIVE", "TERRACE", "PLACE", "COURT" };
        private readonly static string[] towns = new[] { "PALETO BAY", "GRAPESEED", "SANDY SHORES", "LOS SANTOS" };
        private readonly static string[] hairColors = new[] { "BLK", "BRN", "RED", "BLN" };
        private readonly static string[] eyeColors = new[] { "BRN", "GRN", "BLU" };

        public License(int localId, Common.Sex sex)
        {
            LicenseNumber = "l" + Common.random.Next(1000000, 9999999).ToString();
            Class = classes[Common.random.Next(classes.Length)];

            DOB = Common.RandomDate(DateTime.Today.AddYears(-55), DateTime.Today.AddYears(-16).AddDays(-1)); // find date >= 16 years ago
            Issuance = Common.RandomDate(DateTime.Today.AddYears(-5), DateTime.Today); // find date between today and 16th birthday (cannot be more than 5 years ago.. well, could be, but MOST WOULDN'T BE)
            Expiration = Issuance.AddYears(5); // find date 5 years after issuance

            Sex = sex == Common.Sex.Male ? "M" : "F";
            FirstName = sex == Common.Sex.Male
                ? Common.FirstNamesMale[Common.random.Next(Common.FirstNamesMale.Count)]
                : Common.FirstNamesFemale[Common.random.Next(Common.FirstNamesFemale.Count)];
            LastName = Common.LastNames[Common.random.Next(Common.LastNames.Count)];

            AddressStreet = $"{Common.random.Next(1, 99999)} {Common.random.Next(1, 50)} {streetTypes[Common.random.Next(streetTypes.Length)]}"; // EX: = "9674 26 STREET";
            AddressTown = towns[Common.random.Next(towns.Length)];

            Hair = hairColors[Common.random.Next(hairColors.Length)];
            Eyes = eyeColors[Common.random.Next(eyeColors.Length)];
            Height = "5'-" + Common.random.Next(4, 12).ToString() + "\""; // 5'[4-11]"
            Weight = Common.random.Next(115, 251).ToString() + " lb"; // 115-250lb
        }
    }
}
