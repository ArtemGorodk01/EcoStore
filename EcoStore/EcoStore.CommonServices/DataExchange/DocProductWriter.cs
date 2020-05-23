using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EcoStore.CommonServices.DataExchange
{
    public class DocProductWriter : IDataWriter<Product>
    {
        public StreamWriter writer;

        public DocProductWriter(StreamWriter writer)
        {
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        public void WriteData(IEnumerable<Product> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            data.ToList().ForEach(p => WriteProduct(p));
            writer.Flush();
        }

        private void WriteProduct(Product product)
        {
            writer.WriteLine($"Title: {product.Title}; price: {product.Price}; description: {product.Description}");
        }
    }
}
