using System;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;

namespace eCommerce.CustomerSite.Interfaces
{
	public interface IRatingService
	{
		Task<PagedResult<RatingVM>> GetRatingByProduct(PagingRequest req, int categoryId);
		Task<bool> CreateRating(RatingCreateRequest req);
	}
}

