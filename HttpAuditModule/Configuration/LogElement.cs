using System.Configuration;

namespace Notadesigner.Crumbs.Configuration
{
    /// <summary>
    /// Implements an element for the logging configuration.
    /// </summary>
    public class LogElement : ConfigurationElement
    {
        /// <summary>
        /// All properties exposed by this element.
        /// </summary>
        private static ConfigurationPropertyCollection _properties;

        /// <summary>
        /// The name of the connection string configuration element to be used by this module.
        /// </summary>
        private static ConfigurationProperty _connectionStringName;

        /// <summary>
        /// Static constructor to initialise this element.
        /// </summary>
        static LogElement()
        {
            _connectionStringName = new ConfigurationProperty("connectionStringName", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_connectionStringName);
        }

        /// <summary>
        /// Stores the name of the connection string used by this module.
        /// </summary>
        [ConfigurationProperty("connectionStringName", IsRequired = true)]
        public string ConnectionStringName
        {
            get
            {
                return (string)base[_connectionStringName];
            }
        }
    }
}