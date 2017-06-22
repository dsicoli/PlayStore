using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class Prices
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string Currency { get; set; }
        public string Value { get; set; }

        public virtual App App { get; set; }
    }
}
