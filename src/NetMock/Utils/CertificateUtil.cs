using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NetMock.Exceptions;

namespace NetMock.Utils
{
	internal static class CertificateUtil
	{
		public static X509Certificate2 LoadCertifiace(string certificateThumbprint, StoreName storeName, StoreLocation storeLocation)
		{
			using (X509Store store = new X509Store(storeName, storeLocation))
			{
				store.Open(OpenFlags.ReadOnly);
				X509Certificate2 certificate = store.Certificates
					.OfType<X509Certificate2>()
					.FirstOrDefault(x => x.Thumbprint.Equals(certificateThumbprint, StringComparison.OrdinalIgnoreCase));

				if (certificate == null)
					throw new CertificateException($"Certificate not found: {certificateThumbprint} {storeName} @ {storeLocation}");

				return certificate;
			}
		}

		public static void BindCertificate(X509Certificate2 certificate, int port)
		{
			try
			{
				ExecuteNetsh($"http delete sslcert ipport=0.0.0.0:{port}");
				ExecuteNetsh($"http add sslcert ipport=0.0.0.0:{port} certhash={certificate.GetCertHashString()} appid={{{Guid.NewGuid()}}}");
			}
			catch (Exception ex)
			{
				throw new CertificateException($"Failed to bind certificate to port {port} (requires administrative privileges)", ex);
			}
		}

		public static void UnbindCertificate(int port)
		{
			try
			{
				ExecuteNetsh($"http delete sslcert ipport=0.0.0.0:{port}");
			}
			catch (Exception ex)
			{
				throw new CertificateException($"Failed to unbind certificate for port {port} (requires administrative privileges)", ex);
			}
		}

		private static void ExecuteNetsh(string args)
		{
			using (Process process = new Process
				{
					StartInfo = new ProcessStartInfo
						{
							FileName = "netsh",
							Arguments = args,
							ErrorDialog = true,
							CreateNoWindow = true,
							RedirectStandardOutput = true,
							UseShellExecute = false
						}
				})
			{
				process.Start();
				process.WaitForExit();
			}
		}
	}
}
