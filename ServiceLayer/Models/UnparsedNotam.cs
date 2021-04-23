using System;
using System.Collections.Generic;

#nullable disable

namespace ServiceLayer.Models
{
    public partial class UnparsedNotam
    {
        public int UnparsedNotamId { get; set; }
        public string RawNotamText { get; set; }
        public bool Ignore { get; set; }
        public string UnparedErrorStatus { get; set; }

        public virtual ManualNotam ManualNotam { get; set; }
    }
}
