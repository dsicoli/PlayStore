using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlayStore.Model
{
    public partial class Compatibility
    {
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public int AppId { get; set; }
        public virtual App App { get; set; }
    }
}
