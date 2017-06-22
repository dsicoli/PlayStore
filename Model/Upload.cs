using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class Upload
    {
        public int Id { get; set; }
        public bool Accepted { get; set; }
        public int AppId { get; set; }
        public bool Update { get; set; }
        public int UserAppId { get; set; }
        public int UsersId { get; set; }

        public virtual User Users { get; set; }
    }
}
