using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayStore.Model
{
    public class App
    {
        // public Apps()
        // {
        //     Compatibilities = new HashSet<Compatibilities>();
        //     Prices = new HashSet<Prices>();
        //     UserApp = new HashSet<UserApp>();
        // }

        [Key]
        public int Id { get; set; }
        public string AppBrand { get; set; }
        public string Genre { get; set; }
        public string LastUpdate { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Compatibility> Compatibilities { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<UserApp> UserApp { get; set; }
    }
}
