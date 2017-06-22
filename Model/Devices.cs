using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class Devices
    {
        public int Id { get; set; }
        public string DeviceUsed { get; set; }
        public int DownloadId { get; set; }

        public virtual Downloads IdNavigation { get; set; }
    }
}
