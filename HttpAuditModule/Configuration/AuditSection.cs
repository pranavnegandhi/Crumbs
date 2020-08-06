using System.Configuration;

namespace Notadesigner.Crumbs.Configuration
{
    /// <summary>
    /// Implements a custom section for this module in the app.config / web.config file.
    /// </summary>
    public class AuditSection : ConfigurationSection
    {
        /// <summary>
        /// The name of the node used for this section.
        /// </summary>
        public static readonly string SectionName = "auditing";

        /// <summary>
        /// All properties exposed by this section.
        /// </summary>
        private static ConfigurationPropertyCollection _properties;

        /// <summary>
        /// The methods property under this section.
        /// </summary>
        private static ConfigurationProperty _methodsElement;

        /// <summary>
        /// The log property under this section.
        /// </summary>
        private static ConfigurationProperty _logElement;

        /// <summary>
        /// Static constructor to initialise this section.
        /// </summary>
        static AuditSection()
        {
            _methodsElement = new ConfigurationProperty("methods", typeof(MethodsElement), null, ConfigurationPropertyOptions.IsRequired);
            _logElement = new ConfigurationProperty("log", typeof(LogElement), null, ConfigurationPropertyOptions.IsRequired);
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_methodsElement);
            _properties.Add(_logElement);
        }

        /// <summary>
        /// References the <c>methods</c> node in the configuration file.
        /// </summary>
        [ConfigurationProperty("methods", IsRequired = true)]
        public MethodsElement Methods
        {
            get
            {
                return (MethodsElement)base["methods"];
            }
        }

        /// <summary>
        /// Reference to the <c>log</c> node in the configuration file.
        /// </summary>
        [ConfigurationProperty("log", IsRequired = true)]
        public LogElement Log
        {
            get
            {
                return (LogElement)base["log"];
            }
        }
    }
}