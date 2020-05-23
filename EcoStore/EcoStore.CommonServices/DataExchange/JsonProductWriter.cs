using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EcoStore.CommonServices.DataExchange
{
    public class JsonProductWriter : IDataWriter<Product>
    {
        private StreamWriter writer;

        public JsonProductWriter(StreamWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void WriteData(IEnumerable<Product> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var stringValue = JsonSerializer.Serialize(data.ToList(), typeof(List<Product>));
            writer.Write(stringValue);
            writer.Flush();
        }
    }
}
