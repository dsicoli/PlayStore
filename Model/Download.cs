using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class Download
    {
        // public Downloads()
        // {
        //     Ratings = new HashSet<Ratings>();
        // }

        public int Id { get; set; }
        public int AppId { get; set; }
        public byte[] Successful { get; set; }
        public int UserId { get; set; }

        public virtual Devices Devices { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
        public virtual User User { get; set; }
    }
}
