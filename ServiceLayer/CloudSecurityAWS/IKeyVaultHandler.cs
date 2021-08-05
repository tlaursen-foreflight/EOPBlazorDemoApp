using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ServiceLayer.CloudSecurityAWS
{
    public interface IKeyVaultHandler
    {
        X509Certificate2 GetCertificate(string certificateName);
        string GetSecretValue(string secretName);
        bool TryGetSecretValue(string secretName, out string value);
    }
}
