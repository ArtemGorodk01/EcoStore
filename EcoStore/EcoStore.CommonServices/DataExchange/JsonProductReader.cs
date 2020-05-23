using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace EcoStore.CommonServices.DataExchange
{
    public class JsonProductReader : IDataReader<Product>
    {
        private StreamReader reader;

        public JsonProductReader(StreamReader reader)
        {
            this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public IEnumerable<Product> ReadData()
        {
            var buffer = reader.ReadToEnd();
            var products = JsonSerializer.Deserialize(buffer, typeof(List<Product>)) as List<Product>;
            if (products != null)
            {
                return products;
            }

            return new List<Product>();
        }
    }
}
