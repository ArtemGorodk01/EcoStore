using EcoStore.EFCore.Entities;
using EcoStore.EFCore.Interfaces.UnitOfWork;
using EcoStore.Web.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoStore.Web.Services
{
    public class ReviewService : IReviewService
    {
        private IEcoStoreUnitOfWork _unitOfWork;

        public ReviewService(IEcoStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteReview(int userMarkProductId)
        {
            return await _unitOfWork.ProductRepository.DeleteReview(userMarkProductId);
        }

        public async Task<List<UserMarkProduct>> GetReviews(int page, int reviewsPerPage)
        {
            return await _unitOfWork.ProductRepository.GetReviews(page, reviewsPerPage);
        }

        public async Task<int> GetReviewsPages(int reviewPerPage)
        {
            return await _unitOfWork.ProductRepository.GetReviewCount(reviewPerPage);
        }
    }
}
