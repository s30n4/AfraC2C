using System.Collections.Generic;
using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity
{
    public class OnlineUsersDto
    {
        public List<User> Users { set; get; }
        public int NumbersToTake { set; get; }
        public int MinutesToTake { set; get; }
        public bool ShowMoreItemsLink { set; get; }
    }
}