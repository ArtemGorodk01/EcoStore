using System.Collections.Generic;

namespace EcoStore.CommonServices.DataExchange
{
    public interface IDataReader<T>
    {
        IEnumerable<T> ReadData();
    }
}
