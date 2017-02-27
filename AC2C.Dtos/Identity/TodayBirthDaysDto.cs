using System.Collections.Generic;
using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity
{
    public class TodayBirthDaysDto
    {
        public List<User> Users { set; get; }

        public AgeStatDto AgeStat { set; get; }
    }
}