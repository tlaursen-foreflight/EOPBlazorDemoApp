using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace ServiceLayer.CloudSecurityAWS
{
    public sealed class AWSSecretManagerHandler : IKeyVaultHandler
    {
        private readonly Dictionary<string, string> m_secrets;

        public AWSSecretManagerHandler(AWSSecretManagerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.SecretNames?.Any() != true)
            {
                throw new ArgumentException("Should have at least one secret", nameof(options.SecretNames));
            }

            m_secrets = FetchAWSSecrets(options.SecretNames);
        }

        private static Dictionary<string, string> FetchAWSSecrets(IEnumerable<string> secretNames)
        {
            Dictionary<string, string> secrets = new Dictionary<string, string>();
            foreach (var secret in secretNames)
            {
                try
                {
                    var request = new GetSecretValueRequest
                    {
                        SecretId = secret,
                        VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
                    };

                    using (var client = new AmazonSecretsManagerClient(RegionEndpoint.USEast1))
                    {
                        var response = GetSecretValue(client, request);
                        var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);

                        foreach (var kvPair in values)
                        {
                            secrets[kvPair.Key] = kvPair.Value;
                        }
                    }
                }
                catch (Exception e)
                {
                    // Some exceptions might contain sensitive information ie unable to deserialize response.SecretString.
                    // In order to help debugging a little we will expose the exception type
                    var type = e.GetType();
                    throw new Exception($"Trying to fetch AWS secrets resulted in an exception of type {type}");
                }

            }

            return secrets;
        }

        public X509Certificate2 GetCertificate(string certificateName)
        {
            var certificate = GetSecretValue(certificateName);
            var bytes = Convert.FromBase64String(certificate);
            var passwordName = $"{certificateName}_password";
            var password = GetSecretValue(passwordName);
            var certObj = new X509Certificate2(bytes, password);
            return certObj;
        }

        public string GetSecretValue(string secretName)
        {
            if (!m_secrets.TryGetValue(secretName, out string value))
            {
                throw new KeyNotFoundException("Could not find secret named: " + secretName);
            }

            return value;
        }

        public bool TryGetSecretValue(string secretName, out string value)
        {
            return m_secrets.TryGetValue(secretName, out value);
        }

        private static GetSecretValueResponse GetSecretValue(AmazonSecretsManagerClient client, GetSecretValueRequest request)
        {
            return client.GetSecretValueAsync(request, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
