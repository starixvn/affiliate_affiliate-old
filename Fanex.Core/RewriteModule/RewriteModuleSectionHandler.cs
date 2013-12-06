/// <summary>
/// Summary description for RewriteModuleSectionHandler
/// </summary>
namespace Fanex.Core.RewriteModule
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Xml;

    public class RewriteModuleSectionHandler : IConfigurationSectionHandler
    {
        private XmlNode _XmlSection;        
        private string _RewriteBase;        
        private bool _RewriteOn;         
        public XmlNode XmlSection        {            get { return _XmlSection; }        }         
        public string RewriteBase        {            get { return _RewriteBase; }        }         
        public bool RewriteOn        {            get { return _RewriteOn; }        }        
        public object Create(object parent, object configContext, System.Xml.XmlNode section)        
        {            
            // set base path for rewriting module to            
            // application root            
            _RewriteBase = HttpContext.Current.Request.ApplicationPath + "/";             
            // process configuration section             
            // from web.config            
            try            
            {                
                _XmlSection = section;                
                _RewriteOn = Convert.ToBoolean(section.SelectSingleNode("rewriteOn").InnerText);            
            }     
            catch (Exception ex)            
            {                
                throw (new Exception("Error while processing RewriteModule configuration section.", ex));            
            }            
            return this;       
        }
    }
}
