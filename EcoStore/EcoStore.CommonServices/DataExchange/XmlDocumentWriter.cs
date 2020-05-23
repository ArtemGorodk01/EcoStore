using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EcoStore.CommonServices.DataExchange
{
    public class XmlProductWriter : IDataWriter<Product>
    {
        private StreamWriter writer;

        public XmlProductWriter(StreamWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void WriteData(IEnumerable<Product> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var xmlSerializer = new XmlSerializer(typeof(List<Product>));
            xmlSerializer.Serialize(writer, data.ToList());
            writer.Flush();
        }
    }
}
