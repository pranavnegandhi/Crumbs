using System.Configuration;

namespace Notadesigner.Crumbs.Configuration
{
    /// <summary>
    /// Implements a configuration element that groups the HTTP
    /// method names in the configuration file.
    /// </summary>
    public class MethodsElement : ConfigurationElement
    {
        /// <summary>
        /// All properties exposed by this element.
        /// </summary>
        private static ConfigurationPropertyCollection _properties;

        /// <summary>
        /// The get property under this element.
        /// </summary>
        private static ConfigurationProperty _getMethod;

        /// <summary>
        /// The put property under this element.
        /// </summary>
        private static ConfigurationProperty _putMethod;

        /// <summary>
        /// The post property under this element.
        /// </summary>
        private static ConfigurationProperty _postMethod;

        /// <summary>
        /// The delete property under this element.
        /// </summary>
        private static ConfigurationProperty _deleteMethod;

        /// <summary>
        /// The head property under this element.
        /// </summary>
        private static ConfigurationProperty _headMethod;

        /// <summary>
        /// The options property under this element.
        /// </summary>
        private static ConfigurationProperty _optionsMethod;

        /// <summary>
        /// Static constructor to initialise this element.
        /// </summary>
        static MethodsElement()
        {
            _getMethod = new ConfigurationProperty("get", typeof(HttpMethodElement), null, ConfigurationPropertyOptions.IsRequired);
            _putMethod = new ConfigurationProperty("put", typeof(HttpMethodElement), null);
            _postMethod = new ConfigurationProperty("post", typeof(HttpMethodElement), null);
            _deleteMethod = new ConfigurationProperty("delete", typeof(HttpMethodElement), null);
            _headMethod = new ConfigurationProperty("head", typeof(HttpMethodElement), null);
            _optionsMethod = new ConfigurationProperty("options", typeof(HttpMethodElement), null);
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_getMethod);
            _properties.Add(_putMethod);
            _properties.Add(_postMethod);
            _properties.Add(_deleteMethod);
            _properties.Add(_headMethod);
            _properties.Add(_optionsMethod);
        }

        /// <summary>
        /// Configures how HTTP GET requests are processed.
        /// </summary>
        [ConfigurationProperty("get", IsRequired = true)]
        public HttpMethodElement Get
        {
            get
            {
                return (HttpMethodElement)base[_getMethod];
            }
        }

        /// <summary>
        /// Configures how HTTP PUT requests are processed.
        /// </summary>
        [ConfigurationProperty("put", IsRequired = false)]
        public HttpMethodElement Put
        {
            get
            {
                return (HttpMethodElement)base[_putMethod];
            }
        }

        /// <summary>
        /// Configures how HTTP POST requests are processed.
        /// </summary>
        [ConfigurationProperty("post", IsRequired = false)]
        public HttpMethodElement Post
        {
            get
            {
                return (HttpMethodElement)base[_postMethod];
            }
        }

        /// <summary>
        /// Configures how HTTP DELETE requests are processed.
        /// </summary>
        [ConfigurationProperty("delete", IsRequired = false)]
        public HttpMethodElement Delete
        {
            get
            {
                return (HttpMethodElement)base[_deleteMethod];
            }
        }

        /// <summary>
        /// Configures how HTTP HEAD requests are processed.
        /// </summary>
        [ConfigurationProperty("head", IsRequired = false)]
        public HttpMethodElement Head
        {
            get
            {
                return (HttpMethodElement)base[_headMethod];
            }
        }

        /// <summary>
        /// Configures how HTTP OPTIONS requests are processed.
        /// </summary>
        [ConfigurationProperty("options", IsRequired = false)]
        public HttpMethodElement Options
        {
            get
            {
                return (HttpMethodElement)base[_optionsMethod];
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