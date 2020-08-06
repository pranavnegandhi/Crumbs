using System;

namespace Notadesigner.Crumbs
{
    /// <summary>
    /// Primary data structure for the HTTP audit log.
    /// </summary>
    internal sealed class HttpAuditEntry
    {
        /// <summary>
        /// The time of this audit entry.
        /// </summary>
        public DateTime TimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// The absolute URL being invoked.
        /// </summary>
        public string RawUrl
        {
            get;
            set;
        }

        /// <summary>
        /// The relative path of the URL being invoked.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the HTTP method being used.
        /// </summary>
        public string HttpMethod
        {
            get;
            set;
        }

        /// <summary>
        /// The HTTP status code being returned to the caller.
        /// </summary>
        public int HttpStatus
        {
            get;
            set;
        }

        /// <summary>
        /// The user under whose credentials the URL is invoked.
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// The authentication mechanism being used to identify the user.
        /// </summary>
        public string AuthenticationType
        {
            get;
            set;
        }

        /// <summary>
        /// The IP address of the device from where the request is received.
        /// </summary>
        public string HostAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The host name of the device from where the request is received.
        /// </summary>
        public string HostName
        {
            get;
            set;
        }

        /// <summary>
        /// The URL from where the current request was referred.
        /// </summary>
        public string Referrer
        {
            get;
            set;
        }

        /// <summary>
        /// The MIME type of the outgoing response.
        /// </summary>
        public string ContentType
        {
            get;
            set;
        }

        /// <summary>
        /// The user agent string of the client making the request.
        /// </summary>
        public string UserAgent
        {
            get;
            set;
        }
    }
}