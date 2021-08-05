using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.CloudSecurityAWS
{
    public class AWSSecretManagerOptions
    {
        public IEnumerable<string> SecretNames { get; set; }
    }
}
