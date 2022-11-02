using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using NetMock.Exceptions;

namespace NetMock.Utils
{
	internal static class CertificateUtil
	{
		private const StoreName InstallationStoreName = StoreName.My;
		private const StoreLocation InstallationStoreLocation = StoreLocation.LocalMachine;

		public static X509Certificate2 LoadCertificate(X509FindType findType, string findValue, StoreName storeName, StoreLocation storeLocation)
		{
			using (X509Store store = new X509Store(storeName, storeLocation))
			{
				store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection certificates = store.Certificates.Find(findType, findValue, false);

				if (certificates.Count == 0)
					throw new CertificateException($"Certificate not found: [{findType}:{findValue}] {storeName} @ {storeLocation}");

				if (certificates.Count > 1)
					throw new CertificateException($"Multiple matching certificates found: [{findType}:{findValue}] {storeName} @ {storeLocation}");

				return certificates[0];
			}
		}

		public static void AddCertificate(X509Certificate2 certificate)
		{
			ThrowIfNoThumbprint(certificate);

			using (X509Store store = new X509Store(InstallationStoreName, InstallationStoreLocation))
			{
				store.Open(OpenFlags.ReadWrite);

				if (CertificateExists(store, certificate.Thumbprint))
					return;

				store.Add(certificate);
			}
		}

		public static void RemoveCertificate(X509Certificate2 certificate)
		{
			ThrowIfNoThumbprint(certificate);

			using (X509Store store = new X509Store(InstallationStoreName, InstallationStoreLocation))
			{
				store.Open(OpenFlags.ReadWrite);

				if (!CertificateExists(store, certificate.Thumbprint))
					return;

				store.Remove(certificate);
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

		private static bool CertificateExists(X509Store store, string thumbprint)
		{
			X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

			return certificates.Count > 0;
		}

		private static void ThrowIfNoThumbprint(X509Certificate2 certificate)
		{
			if (string.IsNullOrEmpty(certificate.Thumbprint))
			{
				throw new CertificateException($"No thumbprint in certificate");
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
