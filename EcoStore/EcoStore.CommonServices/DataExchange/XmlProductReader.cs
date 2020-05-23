using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EcoStore.CommonServices.DataExchange
{
    public class XmlProductReader : IDataReader<Product>
    {
        private StreamReader reader;

        public XmlProductReader(StreamReader reader)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IEnumerable<Product> ReadData()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Product>));
            var importedProducts = xmlSerializer.Deserialize(reader) as List<Product>;
            if (importedProducts != null)
            {
                return importedProducts;
            }

            return new List<Product>();
        }
    }
}
