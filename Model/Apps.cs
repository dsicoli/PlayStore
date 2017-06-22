using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class App
    {
        // public Apps()
        // {
        //     Compatibilities = new HashSet<Compatibilities>();
        //     Prices = new HashSet<Prices>();
        //     UserApp = new HashSet<UserApp>();
        // }

        public int Id { get; set; }
        public string AppBrand { get; set; }
        public string Genre { get; set; }
        public string LastUpdate { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Compatibilities> Compatibilities { get; set; }
        public virtual ICollection<Prices> Prices { get; set; }
        public virtual ICollection<UserApp> UserApp { get; set; }
    }
}
