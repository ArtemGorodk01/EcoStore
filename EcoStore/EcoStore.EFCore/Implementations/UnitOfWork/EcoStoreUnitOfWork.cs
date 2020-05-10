using EcoStore.EFCore.Context;
using EcoStore.EFCore.Implementations.Repositories;
using EcoStore.EFCore.Interfaces.Repositories;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoStore.EFCore.Implementations.UnitOfWork
{
    public class EcoStoreUnitOfWork : IEcoStoreUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; } = new CategoryRepository();

        public IDeliveryRepository DeliveryRepository { get; } = new DeliveryRepository();

        public IOrderRepository OrderRepository { get; } = new OrderRepository();

        public IProductRepository ProductRepository { get; } = new ProductRepository();

        public IStorageRepository StorageRepository { get; } = new StorageRepository();

        public IStoreRepository StoreRepository { get; } = new StoreRepository();

        public IUserRepository UserRepository { get; } = new UserRepository();

        public IVendorRepository VendorRepository { get; } = new VendorRepository();
    }
}
