using Notadesigner.Crumbs.Configuration;
using Notadesigner.Crumbs.Extensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Notadesigner.Crumbs
{
    /// <summary>
    /// Implements an IIS module that keeps track of incoming HTTP network requests.
    /// </summary>
    public class HttpAuditModule : IHttpModule
    {
        /// <summary>
        /// A mapping of HTTP method names to numeric codes.
        /// </summary>
        private static readonly Dictionary<string, int> _methodMap = new Dictionary<string, int>()
        {
            { "get", (int)HttpMethod.Get },
            { "put", (int)HttpMethod.Put },
            { "post", (int)HttpMethod.Post },
            { "delete", (int)HttpMethod.Delete },
            { "head", (int)HttpMethod.Head },
            { "options", (int)HttpMethod.Options }
        };

        /// <summary>
        /// The name of the ConnectionString configuration element that has to be used by this module.
        /// </summary>
        private string _connectionStringName;

        /// <summary>
        /// A bit array to store the tracking status of all HTTP methods.
        /// </summary>
        private int _enabledMethods = 0x0;

        /// <summary>
        /// Required by the IHttpModule interface. This module does not use any persistent resources that require disposal.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Invoked by the hosting environment to initialise this module instance.
        /// </summary>
        /// <param name="application">A reference to the active application.</param>
        public void Init(HttpApplication application)
        {
            application.PostRequestHandlerExecute += Application_PostRequestHandlerExecute;

            ConfigurationManager.RefreshSection(AuditSection.SectionName);
            var configuration = (AuditSection)ConfigurationManager.GetSection(AuditSection.SectionName);

            // Identify the HTTP methods that have to be audited. Store their state in a bit array.
            _enabledMethods = 0;
            _enabledMethods |= configuration.Methods.Get.IsEnabled ? (int)HttpMethod.Get : 0;
            _enabledMethods |= configuration.Methods.Put.IsEnabled ? (int)HttpMethod.Put : 0;
            _enabledMethods |= configuration.Methods.Post.IsEnabled ? (int)HttpMethod.Post : 0;
            _enabledMethods |= configuration.Methods.Post.IsEnabled ? (int)HttpMethod.Delete : 0;
            _enabledMethods |= configuration.Methods.Post.IsEnabled ? (int)HttpMethod.Head : 0;
            _enabledMethods |= configuration.Methods.Post.IsEnabled ? (int)HttpMethod.Options : 0;

            _connectionStringName = configuration.Log.ConnectionStringName;
        }

        /// <summary>
        /// Event handler fired by the ASP.NET application to creates an entry in the audit log for the completed request.
        /// </summary>
        /// <param name="sender">An instance of the current application instance.</param>
        /// <param name="e">The event argument object associated with this handler.</param>
        private void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            // Only proceed with execution if the HTTP method of the current request has to be audited.
            var method = HttpContext.Current.Request.HttpMethod.ToLowerInvariant();
            int code;
            if (!_methodMap.TryGetValue(method, out code))
            {
                code = (int)HttpMethod.Get;
            }

            if ((_enabledMethods & code) != code)
            {
                return;
            }

            var application = (HttpApplication)sender;
            var request = application.Request;

            var rawUrl = application.Context.Request.RawUrl.TrailingSubstring(255);
            var contentType = application.Response.ContentType.TrailingSubstring(255);
            var status = application.Response.StatusCode;
            var userName = application.User?.Identity?.Name?.TrailingSubstring(255) ?? "Anonymous";
            var authType = application.User?.Identity?.AuthenticationType?.TrailingSubstring(255) ?? "Anonymous";
            var userAgent = application.Request?.UserAgent?.TrailingSubstring(255) ?? "Unknown";
            var path = request.Path.TrailingSubstring(255);
            var timeStamp = request.RequestContext.HttpContext.Timestamp;
            var hostAddr = request.UserHostAddress?.TrailingSubstring(255) ?? "Unknown";
            var hostName = request.UserHostName?.TrailingSubstring(255) ?? "Unknown";
            var referrer = request.UrlReferrer?.ToString()?.TrailingSubstring(255) ?? "Unknown";

            var entry = new HttpAuditEntry()
            {
                AuthenticationType = authType,
                ContentType = contentType,
                HostAddress = hostAddr,
                HostName = hostName,
                HttpMethod = method,
                HttpStatus = status,
                Path = path,
                RawUrl = rawUrl,
                Referrer = referrer,
                TimeStamp = timeStamp,
                UserAgent = userAgent,
                UserName = userName
            };

            try
            {
                var connString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;
                using (var connection = new SqlConnection(connString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = @"insert into [audit].[HttpAuditEntry]
                            ([TimeStamp],
                                [RawUrl],
                                [Path],
                                [HttpMethod],
                                [HttpStatus],
                                [UserName],
                                [AuthenticationType],
                                [ContentType],
                                [UserAgent],
                                [HostAddress],
                                [HostName],
                                [Referrer])
                            values(
                                @timeStamp,
                                @rawUrl,
                                @path,
                                @httpMethod,
                                @httpStatus,
                                @userName,
                                @authType,
                                @contentType,
                                @userAgent,
                                @hostAddress,
                                @hostName,
                                @referrer)";

                        var param = command.CreateParameter();
                        param.ParameterName = "@timeStamp";
                        param.Value = entry.TimeStamp;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@rawUrl";
                        param.Value = entry.RawUrl;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@path";
                        param.Value = entry.Path;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@httpMethod";
                        param.Value = entry.HttpMethod;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@httpStatus";
                        param.Value = entry.HttpStatus;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@userName";
                        param.Value = entry.UserName;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@authType";
                        param.Value = entry.AuthenticationType;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@contentType";
                        param.Value = entry.ContentType;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@userAgent";
                        param.Value = entry.UserAgent;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@hostAddress";
                        param.Value = entry.HostAddress;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@hostName";
                        param.Value = entry.HostName;
                        command.Parameters.Add(param);

                        param = command.CreateParameter();
                        param.ParameterName = "@referrer";
                        param.Value = entry.Referrer;
                        command.Parameters.Add(param);

                        connection.Open();
                        var rowCount = command.ExecuteNonQuery();
                        if (rowCount != 1)
                        {
                            using (var logger = new LoggerConfiguration()
                                .ReadFrom
                                .AppSettings()
                                .CreateLogger())
                            {
                                logger.Information("Could not log audit trail for {@auditEntry}", entry);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                using (var logger = new LoggerConfiguration()
                    .ReadFrom
                    .AppSettings()
                    .CreateLogger())
                {
                    logger.Error(exception, "Failed to log audit trail for {@auditEntry}", entry);
                }
            }
        }
    }
}