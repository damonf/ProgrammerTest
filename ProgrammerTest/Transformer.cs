using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ProgrammerTest
{
    public interface ITransformer
    {
        void Transform(IXPathNavigable xPathDocument, string outfile);
    }

    /// <summary>
    /// Converts the XML document to HTML file.
    /// </summary>
    public class Transformer : ITransformer
    {
        private readonly XslCompiledTransform _xslTransform;

        public Transformer(XslCompiledTransform xslTransform)
        {
            if (xslTransform == null)
                throw new ArgumentNullException(nameof(xslTransform));

            _xslTransform = xslTransform;
        }

        public void Transform(IXPathNavigable xPathDocument, string outfile)
        {
            using (var writer = new XmlTextWriter(outfile, null))
            {
                _xslTransform.Transform(xPathDocument, null, writer);
            }
        }
    }
}
