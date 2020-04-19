using EcoStore.EFCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoStore.EFCore.Interfaces.UnitOfWork
{
    public interface IEcoStoreUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IDeliveryRepository DeliveryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IStorageRepository StorageRepository { get; }
        IStoreRepository StoreRepository { get; }
        IUserRepository UserRepository { get; }
        IVendorRepository VendorRepository { get; }
    }
}
