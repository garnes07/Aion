using System.Collections.Generic;

namespace Aion.Models.WebMaster
{
    public class UserAccessView
    {
        public string UserName { get; set; }
        public bool Krn { get; set; }
        public byte AccessLevel { get; set; }
        public byte AreaLevel { get; set; }
        public string Description { get; set; }
        
        public ICollection<UserAccessAreaView> UserAccessAreas { get; set; }

        public UserAccessView()
        {
            UserAccessAreas = new List<UserAccessAreaView>();
        }
    }
}