using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.CloudSecurityAWS
{
    public static class Initializer
    {
        public static IKeyVaultHandler InitAWSKeyVaultHandler()
        {
            var awsOptions = new AWSSecretManagerOptions
            {
                SecretNames = new[]
                {
                    "services/navdb"
                }
            };
            return new AWSSecretManagerHandler(awsOptions);
        }

        public static string GetBucket(IKeyVaultHandler keyVaultHandler)
        {
            var env = keyVaultHandler.GetSecretValue("Environment");
            string bucket = env == "production" ? "mainstack-prod" : "mainstack-qa";
            return bucket;
        }
    }
}
