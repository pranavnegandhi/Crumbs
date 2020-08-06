# Crumbs
An IIS module to provide audit logging facilities for incoming HTTP requests. The audit trail is generated after the request handler as completed generating its output, so that the response status can be retrieved.

## Usage

###SQL Server
This module requires a table named HttpAuditEntry under the audit schema in your selected database catalogue. A SQL script to generate this table is included in the root of the repository.

###Integration with IIS
HttpAuditModule declares some custom configuration nodes in the application configuration file (web.config or app.config). In order to use them, the section has to be declared in configSections.

	<section name="auditing" type="Notadesigner.Crumbs.Configuration.AuditSection, HttpAuditModule" requirePermission="false" />

After the section is added, the the following nodes should be added under the <configuration> node of the file.

	<auditing>
		<log connectionStringName="DefaultConnectionString" />
		<methods>
			<get isEnabled="false" />
			<post isEnabled="true" />
		</methods>
	</auditing>

The connectionStringName attribute of the log node declares the name of the connection string to be used by the module. HttpAuditModule is written to utilise ADO.NET and connects to SQL Server. Remember that the value of the connectionStringName attribute should be the name of the connection string, and not a connection string itself.

The methods node consists of up to 6 child nodes, one for each type of HTTP request. Their names are get, put, post, delete, head and options. Each child node has an isEnabled attribute that can store a boolean value. Requests made using any HTTP method are logged only if the value of this attribute is true.

Only the get node is mandatory, and the default value of isEnabled for all nodes is false.

Finally, the module has to be added to your site through the IIS Manager, or by adding an entry to the modules element in web.config directly.

	<modules>
		<add name="HttpAuditModule" type="Notadesigner.Crumbs.HttpAuditModule, HttpAuditModule" preCondition="managedHandler" />
	</modules>

After the module is added, it is automatically picked up by the ASP.NET pipeline and invoked when a request is being fulfilled. All requests are logged in the HttpAuditEntry table for later use in reports.

