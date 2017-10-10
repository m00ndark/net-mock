using System;

namespace NetMock.Exceptions
{
    public class CertificateException : NetMockException
    {
	    public CertificateException(string message) : base(message) { }
	    public CertificateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
