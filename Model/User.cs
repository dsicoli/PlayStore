using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class User
    {
        // public Users()
        // {
        //     Downloads = new HashSet<Downloads>();
        //     Uploads = new HashSet<Uploads>();
        //     UserApp = new HashSet<UserApp>();
        // }

        public int Id { get; set; }
        public DateTime? Birth { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SecondEmail { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<Download> Downloads { get; set; }
        public virtual ICollection<Upload> Uploads { get; set; }
        public virtual ICollection<UserApp> UserApp { get; set; }
    }
}
