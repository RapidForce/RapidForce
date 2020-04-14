using System;
using System.Collections.Generic;

namespace RapidForce
{
    public class Persona
    {
        public int LocalId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string DOB { get; private set; }
        public string Sex { get; private set; }

        public Persona()
        {

        }
    }

    public class License
    {
        public string LicenseNumber { get; private set; } = "l" + new Random().Next(1000000, 9999999).ToString();
        public string Class { get; private set; }
        public string Expiration { get; private set; } // find date 5 years after issuance
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string AddressStreet { get; private set; } = "9674 26TH STREET";
        public string AddressTown { get; private set; }
        public string AddressState { get; private set; } = "SA 90000";
        public string DOB { get; private set; } // find date >= 16 years ago
        public string Restriction { get; private set; } = "NONE";
        public Sex Sex { get; private set; }
        public string Hair { get; private set; } = HairColor.Colors[new Random().Next(0, HairColor.Colors.Length)];
        public string Eyes { get; private set; } = EyeColor.Colors[new Random().Next(0, EyeColor.Colors.Length)];
        public string Height { get; private set; } = "5'-" + new Random().Next(4, 12).ToString() + "\"";
        public string Weight { get; private set; } = new Random().Next(135, 251).ToString() + " lb";
        public string Issuance { get; private set; } // find date between today and 16th birthday

        public License()
        {

        }
    }

    public class LicenseClass
    {
        public readonly static string[] Classes = new[] { "A", "B", "C", "M1", "M2" };
    }

    public class Town
    {
        public readonly static string[] Towns = new[] { "PALETO BAY", "GRAPESEED", "SANDY SHORES", "LOS SANTOS" };
    }

    public class Sex
    {
        public const string Male = "M";
        public const string Female = "F";
    }

    public class HairColor
    {
        public readonly static string[] Colors = new[] { "BLK", "BRN", "RED", "BLN" };
    }

    public class EyeColor
    {
        public readonly static string[] Colors = new[] { "BRN", "GRN", "BLU" };
    }
}
