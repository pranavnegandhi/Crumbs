using System.Configuration;

namespace Notadesigner.Crumbs.Configuration
{
    /// <summary>
    /// Implements a configuration element for a single HTTP method.
    /// </summary>
    public class HttpMethodElement : ConfigurationElement
    {
        /// <summary>
        /// All properties exposed by this element.
        /// </summary>
        private static ConfigurationPropertyCollection _properties;

        /// <summary>
        /// The isEnabled property of this element.
        /// </summary>
        private static ConfigurationProperty _isEnabled;

        /// <summary>
        /// Static constructor to initialise this element.
        /// </summary>
        static HttpMethodElement()
        {
            _isEnabled = new ConfigurationProperty("isEnabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired);
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_isEnabled);
        }

        /// <summary>
        /// Configures whether the request for this method is audited or not.
        /// </summary>
        [ConfigurationProperty("isEnabled", DefaultValue = false, IsRequired = true)]
        public bool IsEnabled
        {
            get
            {
                return (bool)base[_isEnabled];
            }
        }

        /// <summary>
        /// Gets the collection of properties.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _properties;
            }
        }
    }
}