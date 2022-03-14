using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IRatingService
	{
		Task<int> AddRating(RatingCreateRequest req);
		Task<PagedResult<RatingVM>> GetRatingByProduct(PagingRequest req, int productId);
        //Task<List<RatingVM>> GetRatingByProduct(int productId);
    }
}

