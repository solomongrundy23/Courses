using Bogus;
using System;
using System.Linq;

namespace AddressBookAutotests.Models
{
    public class FIO
    {
        public enum Gender { Man, Woman, All}
        public FIO() { }
        public static Bogus.DataSets.Name.Gender GenderDataSets(Gender gender) => gender switch
        {
            Gender.Man => Bogus.DataSets.Name.Gender.Male,
            Gender.Woman => Bogus.DataSets.Name.Gender.Female,
            _ => (new Random()).Next(1) == 0 ? Bogus.DataSets.Name.Gender.Female : Bogus.DataSets.Name.Gender.Male
        };

        public static FIO Random(Gender gender = Gender.All)
        {
            Faker faker = new Faker("ru");
            FIO result = new FIO();
            Bogus.DataSets.Name.Gender genderSet = GenderDataSets(gender);
            result.Name = faker.Name.FirstName(genderSet);
            result.SurName = faker.Name.LastName(genderSet);
            result.FatherName =
                GetFatherName(faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male), genderSet == Bogus.DataSets.Name.Gender.Female);
            return result;
        }
        private static string GetFatherName(string fatherName, bool isWoman = false)
        {
            string? result;
            if (fatherName.EndsWith("а"))
                result = fatherName.Substring(0, fatherName.Length - 1) + (isWoman ? "инична" : "ич");
            else
            if (fatherName.EndsWith("ий") && fatherName != "Дмитрий")
                result = fatherName.Substring(0, fatherName.Length - 2) + (isWoman ? "ьевна" : "ьевич");
            else
            if ("йьъеоаыёэюия".Contains(fatherName.Last()))
                result = fatherName.Substring(0, fatherName.Length - 1) + (isWoman ? "евна" : "евич");
            else
                result = fatherName + (isWoman ? "овна" : "ович");
            return result;
        }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? SurName { get; set; }
    }
}
