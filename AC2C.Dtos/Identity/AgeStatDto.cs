using AC2C.Entites.Identity;
using DNTPersianUtils.Core;

namespace AC2C.Dtos.Identity
{
    public class AgeStatDto
    {
        const char RleChar = (char) 0x202B;

        public int UsersCount { set; get; }
        public int AverageAge { set; get; }
        public User MaxAgeUser { set; get; }
        public User MinAgeUser { set; get; }

        public string MinMax => $"{RleChar}جوان‌ترین عضو: {MinAgeUser.FirstName + " " + MinAgeUser.LastName} ({MinAgeUser.BirthDate.Value.GetAge()})، مسن‌ترین عضو: {MinAgeUser.FirstName + " " + MinAgeUser.LastName} ({MaxAgeUser.BirthDate.Value.GetAge()})، در بین {UsersCount} نفر";
    }
}