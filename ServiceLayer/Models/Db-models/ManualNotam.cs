using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class ManualNotam
    {
        public int NotamId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Creator { get; set; }
        public string LineObstacles { get; set; }
        public string Obstacles { get; set; }
        public int UnparsedNotamId { get; set; }

        public virtual UnparsedNotam UnparsedNotam { get; set; }
    }
}
