using System;
using System.Linq;
using System.Collections.Generic;

namespace core
{
	public class SeedingHelpers
	{
		// states list
		public static class StatesGenerator
		{
			public class State
			{
				public State(string ab, string name)
				{
					Name = name;
					Abbreviation = ab;
				}

				public string Name { get; set; }

				public string Abbreviation { get; set; }

				public override string ToString()
				{
					return string.Format("{0} - {1}", Abbreviation, Name);
				}
			}

			public static List<State> List = new List<State> {
	          //us states
	          new State("AL", "Alabama"),
			  new State("AK", "Alaska"),
			  new State("AZ", "Arizona"),
			  new State("AR", "Arkansas"),
			  new State("CA", "California"),
			  new State("CO", "Colorado"),
			  new State("CT", "Connecticut"),
			  new State("DE", "Delaware"),
			  new State("DC", "District Of Columbia"),
			  new State("FL", "Florida"),
			  new State("GA", "Georgia"),
			  new State("HI", "Hawaii"),
			  new State("ID", "Idaho"),
			  new State("IL", "Illinois"),
			  new State("IN", "Indiana"),
			  new State("IA", "Iowa"),
			  new State("KS", "Kansas"),
			  new State("KY", "Kentucky"),
			  new State("LA", "Louisiana"),
			  new State("ME", "Maine"),
			  new State("MD", "Maryland"),
			  new State("MA", "Massachusetts"),
			  new State("MI", "Michigan"),
			  new State("MN", "Minnesota"),
			  new State("MS", "Mississippi"),
			  new State("MO", "Missouri"),
			  new State("MT", "Montana"),
			  new State("NE", "Nebraska"),
			  new State("NV", "Nevada"),
			  new State("NH", "New Hampshire"),
			  new State("NJ", "New Jersey"),
			  new State("NM", "New Mexico"),
			  new State("NY", "New York"),
			  new State("NC", "North Carolina"),
			  new State("ND", "North Dakota"),
			  new State("OH", "Ohio"),
			  new State("OK", "Oklahoma"),
			  new State("OR", "Oregon"),
			  new State("PA", "Pennsylvania"),
			  new State("RI", "Rhode Island"),
			  new State("SC", "South Carolina"),
			  new State("SD", "South Dakota"),
			  new State("TN", "Tennessee"),
			  new State("TX", "Texas"),
			  new State("UT", "Utah"),
			  new State("VT", "Vermont"),
			  new State("VA", "Virginia"),
			  new State("WA", "Washington"),
			  new State("WV", "West Virginia"),
			  new State("WI", "Wisconsin"),
			  new State("WY", "Wyoming")
			};

			public static List<string> Abbreviations()
			{
				return List.Select(s => s.Abbreviation).ToList();
			}

			public static List<string> Names()
			{
				return List.Select(s => s.Name).ToList();
			}

			public static string GetName(string abbreviation)
			{
				return List.Where(s => s.Abbreviation.Equals(abbreviation, StringComparison.CurrentCultureIgnoreCase)).Select(s => s.Name).FirstOrDefault();
			}

			public static string GetAbbreviation(string name)
			{
				return List.Where(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).Select(s => s.Abbreviation).FirstOrDefault();
			}

			// get random
            static Random random = new Random();
            public static State GetRandomState()
            {
                return List[random.Next(0, List.Count)];
            }
		}


		// random name generator
		public class NameGenerator
		{
			static Random rnd = new Random();

			static List<string> LastNames = new List<string> {
				"Smith",
				"Johnson",
				"Williams",
				"Jones",
				"Brown",
				"Davis",
				"Miller",
				"Wilson",
				"Moore",
				"Taylor",
				"Anderson",
				"Thomas",
				"Jackson",
				"White",
				"Harris",
				"Martin",
				"Thompson",
				"Garcia",
				"Martinez",
				"Robinson",
				"Clark",
				"Rodriguez",
				"Lewis",
				"Lee",
				"Walker",
				"Hall",
				"Allen",
				"Young",
				"Hernandez",
				"King",
				"Wright",
				"Lopez",
				"Hill",
				"Scott",
				"Green",
				"Adams",
				"Baker",
				"Gonzalez",
				"Nelson",
				"Carter",
				"Mitchell",
				"Perez",
				"Roberts",
				"Turner",
				"Phillips",
				"Campbell",
				"Parker",
				"Evans",
				"Edwards",
				"Collins",
				"Stewart",
				"Sanchez",
				"Morris",
				"Rogers",
				"Reed",
				"Cook",
				"Morgan",
				"Bell",
				"Murphy",
				"Bailey",
				"Rivera",
				"Cooper",
				"Richardson",
				"Cox",
				"Howard",
				"Ward",
				"Torres",
				"Peterson",
				"Gray",
				"Ramirez",
				"James",
				"Watson",
				"Brooks",
				"Kelly",
				"Sanders",
				"Price",
				"Bennett",
				"Wood",
				"Barnes",
				"Ross",
				"Henderson",
				"Coleman",
				"Jenkins",
				"Perry",
				"Powell",
				"Long",
				"Patterson",
				"Hughes",
				"Flores",
				"Washington",
				"Butler",
				"Simmons",
				"Foster",
				"Gonzales",
				"Bryant",
				"Alexander",
				"Russell",
				"Griffin",
				"Diaz",
				"Hayes" 
		    };

			static List<string> FirstNames = new List<string> {
				"Aiden",
				"Jackson",
				"Mason",
				"Liam",
				"Jacob",
				"Jayden",
				"Ethan",
				"Noah",
				"Lucas",
				"Logan",
				"Caleb",
				"Caden",
				"Jack",
				"Ryan",
				"Connor",
				"Michael",
				"Elijah",
				"Brayden",
				"Benjamin",
				"Nicholas",
				"Alexander",
				"William",
				"Matthew",
				"James",
				"Landon",
				"Nathan",
				"Dylan",
				"Evan",
				"Luke",
				"Andrew",
				"Gabriel",
				"Gavin",
				"Joshua",
				"Owen",
				"Daniel",
				"Carter",
				"Tyler",
				"Cameron",
				"Christian",
				"Wyatt",
				"Henry",
				"Eli",
				"Joseph",
				"Max",
				"Isaac",
				"Samuel",
				"Anthony",
				"Grayson",
				"Zachary",
				"David",
				"Christopher",
				"John",
				"Isaiah",
				"Levi",
				"Jonathan",
				"Oliver",
				"Chase",
				"Cooper",
				"Tristan",
				"Colton",
				"Austin",
				"Colin",
				"Charlie",
				"Dominic",
				"Parker",
				"Hunter",
				"Thomas",
				"Alex",
				"Ian",
				"Jordan",
				"Cole",
				"Julian",
				"Aaron",
				"Carson",
				"Miles",
				"Blake",
				"Brody",
				"Adam",
				"Sebastian",
				"Adrian",
				"Nolan",
				"Sean",
				"Riley",
				"Bentley",
				"Xavier",
				"Hayden",
				"Jeremiah",
				"Jason",
				"Jake",
				"Asher",
				"Micah",
				"Jace",
				"Brandon",
				"Josiah",
				"Hudson",
				"Nathaniel",
				"Bryson",
				"Ryder",
				"Justin",
				"Bryce",

	            //—————female

	            "Sophia",
				"Emma",
				"Isabella",
				"Olivia",
				"Ava",
				"Lily",
				"Chloe",
				"Madison",
				"Emily",
				"Abigail",
				"Addison",
				"Mia",
				"Madelyn",
				"Ella",
				"Hailey",
				"Kaylee",
				"Avery",
				"Kaitlyn",
				"Riley",
				"Aubrey",
				"Brooklyn",
				"Peyton",
				"Layla",
				"Hannah",
				"Charlotte",
				"Bella",
				"Natalie",
				"Sarah",
				"Grace",
				"Amelia",
				"Kylie",
				"Arianna",
				"Anna",
				"Elizabeth",
				"Sophie",
				"Claire",
				"Lila",
				"Aaliyah",
				"Gabriella",
				"Elise",
				"Lillian",
				"Samantha",
				"Makayla",
				"Audrey",
				"Alyssa",
				"Ellie",
				"Alexis",
				"Isabelle",
				"Savannah",
				"Evelyn",
				"Leah",
				"Keira",
				"Allison",
				"Maya",
				"Lucy",
				"Sydney",
				"Taylor",
				"Molly",
				"Lauren",
				"Harper",
				"Scarlett",
				"Brianna",
				"Victoria",
				"Liliana",
				"Aria",
				"Kayla",
				"Annabelle",
				"Gianna",
				"Kennedy",
				"Stella",
				"Reagan",
				"Julia",
				"Bailey",
				"Alexandra",
				"Jordyn",
				"Nora",
				"Carolin",
				"Mackenzie",
				"Jasmine",
				"Jocelyn",
				"Kendall",
				"Morgan",
				"Nevaeh",
				"Maria",
				"Eva",
				"Juliana",
				"Abby",
				"Alexa",
				"Summer",
				"Brooke",
				"Penelope",
				"Violet",
				"Kate",
				"Hadley",
				"Ashlyn",
				"Sadie",
				"Paige",
				"Katherine",
				"Sienna",
				"Piper"
		    };

			public static string GenRandomLastName()
			{
                return LastNames[rnd.Next(0, LastNames.Count())];
			}
			public static string GenRandomFirstName()
			{
                return FirstNames[rnd.Next(0, LastNames.Count())];
			}
		}

        // ssn generator
        public class SSNGenerator
        {
            public static List<string> UniqueSSNs = new List<string>();

            static SSNGenerator()
            {
                var random = new Random();
                while(UniqueSSNs.Count() < 100)
                {
                    var ssn = random.Next(100000000, 999999999).ToString();
                    if (UniqueSSNs.Contains(ssn)) continue;
                    UniqueSSNs.Add(ssn);
                }
            }
        }

        // date generator
        public class DateGenerator
        {
			static readonly Random random = new Random();
			public static DateTime GetRandomDate(DateTime from, DateTime to)
			{
				var range = to - from;

				var randTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));

				return from + randTimeSpan;
			}
        }
	}
}
