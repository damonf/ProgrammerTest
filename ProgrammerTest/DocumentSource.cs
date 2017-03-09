using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.XPath;

namespace ProgrammerTest
{
    /// <summary>
    /// Loads the XML documents from the specified directory.
    /// </summary>
    public class DocumentSource : IEnumerable<IXPathNavigable>
    {
        private const string SourcePattern = "*.xml";
        private readonly string _sourceDir;

        public DocumentSource(string sourceDir)
        {
            if (sourceDir == null)
                throw new ArgumentNullException(nameof(sourceDir));

            _sourceDir = sourceDir;
        }

        public IEnumerator<IXPathNavigable> GetEnumerator()
        {
            return
                Directory.EnumerateFiles(_sourceDir, SourcePattern)
                    .Select(filePath => new XPathDocument(filePath))
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
