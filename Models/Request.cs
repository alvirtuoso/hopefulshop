using System.Xml.Serialization;

namespace shop.Models
{
    public class Request
    {
        public bool IsValid { get; set; }
        //[XmlArrayItem("Error")]
        //public AmazonError[] Errors { get; set; }
    }
}
