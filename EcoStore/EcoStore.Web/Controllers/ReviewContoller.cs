using System.Threading.Tasks;
using EcoStore.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class ReviewContoller : Controller
    {
        private const int _reviewCount = 20;
        private IReviewService _reviewService;

        public ReviewContoller(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Admin")]
        [Route("review/list/{page}")]
        public async Task<IActionResult> List(int page)
        {
            var pages = await _reviewService.GetReviewsPages(_reviewCount);
            var list = await _reviewService.GetReviews(page, _reviewCount);
            ViewBag.Pages = pages;
            ViewBag.List = list;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("review/delete/{reviewId}")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            await _reviewService.DeleteReview(reviewId);
            return Redirect("~/review/list/1");
        }
    }
}
