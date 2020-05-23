using EcoStore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcoStore.CommonServices.DataExchange
{
    public interface IDataWriter<T>
    {
        void WriteData(IEnumerable<Product> products);
    }
}
