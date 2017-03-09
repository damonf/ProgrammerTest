using System.Xml.XPath;

namespace ProgrammerTest
{
    /// <summary>
    /// An XML document to be processed.
    /// </summary>
    public class WorkItem
    {
        public IXPathNavigable XmlDocument { get; set; }
        public string OutFile { get; set; }
    }
}
