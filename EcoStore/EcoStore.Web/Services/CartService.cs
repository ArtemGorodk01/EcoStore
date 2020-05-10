using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using EcoStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class CartService : ICartService
    {
        private IEcoStoreUnitOfWork _unitOfWork;
        private Cart _cart = new Cart();

        public CartService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
