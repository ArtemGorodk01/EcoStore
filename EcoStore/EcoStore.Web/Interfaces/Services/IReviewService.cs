using EcoStore.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoStore.Web.Interfaces.Services
{
    public interface IReviewService
    {
        Task<bool> DeleteReview(int userMarkProductId);
        Task<List<UserMarkProduct>> GetReviews(int page, int reviewsPerPage);
        Task<int> GetReviewsPages(int reviewPerPage);
    }
}
