using System;
using System.Collections.Generic;

namespace PlayStore.Model
{
    public partial class Ratings
    {
        public int Id { get; set; }
        public int DownloadId { get; set; }
        public string IndividualRating { get; set; }

        public virtual Downloads Download { get; set; }
    }
}
