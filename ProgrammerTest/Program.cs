using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace ProgrammerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            const string sourceDir = @"..\..\..\Data\Computers\";
            const string outputDir = @"..\..\..\Data\Output";
            const string transformerPath = @"..\..\..\Resources\Computer.xslt";

            try
            {
                var queue = new WorkQueue();
                var documents = new DocumentSource(sourceDir);
                var producer = new Producer(queue, documents, outputDir);
                var transformer = new Transformer(CreateTransformer(transformerPath));
                var consumer = new Consumer(queue, transformer);

                Console.WriteLine("Processing...");

                Task.Run(() =>
                {
                    try
                    {
                        consumer.Consume();
                        Console.WriteLine("Complete\r\nPress any key to exit.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                });

                producer.Produce();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static XslCompiledTransform CreateTransformer(string filename)
        {
            using (var reader = XmlReader.Create(filename, 
                new XmlReaderSettings {DtdProcessing = DtdProcessing.Parse}))
            {
                var xslTrans = new XslCompiledTransform();
                xslTrans.Load(reader);

                return xslTrans;
            }
        }
    }
}
