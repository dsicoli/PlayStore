using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class UserApp
    {
        public int Id { get; set; }
        public int AppsId { get; set; }
        public int UsersId { get; set; }

        public virtual App Apps { get; set; }
        public virtual User Users { get; set; }
    }
}
