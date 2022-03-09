using System;
using Microsoft.EntityFrameworkCore;
using eCommerce.BackendApi.Data.EF;
using eCommerce.BackendApi.Interfaces;
using eCommerce.BackendApi.Models;
using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Ratings;

namespace eCommerce.BackendApi.Services
{
	public class RatingService : IRatingService
	{
		private readonly ApplicationDbContext _dbContext;

		public RatingService(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
        }

		public async Task<int> AddRating(RatingCreateRequest req)
        {

            if (req.Star > 5 || req.Star < 1) return 0;

            var rating = new Rating()
            {
                Star = req.Star,
                Comment = req.Comment,
                CreatedDate = DateTime.Now,
                ProductId = req.ProductId,
                UserId = req.UserId
            };
            _dbContext.Ratings.Add(rating);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PagedResult<RatingVM>> GetRatingByProduct(PagingRequest req, int productId)
        {
            var query = (from r in _dbContext.Ratings
                         join u in _dbContext.Users
                         on r.UserId equals u.Id
                         where r.ProductId == productId
                         select new { r, u });

            int totalRow = query.Count();

            var data = await query.Skip((req.PageIndex - 1) * req.PageSize).Take(req.PageSize)
            .Select(res => new RatingVM()
            {
                Id = res.r.Id,
                Star = (int)res.r.Star,
                Comment = res.r.Comment,
                CreatedDate = DateTime.Now,
                ProductId = res.r.ProductId,
                UserId = res.r.UserId,
                FirstName = res.u.FirstName,
                LastName = res.u.LastName,
                Username = res.u.UserName,
                ImageUrl = res.u.ImageUrl
            }).ToListAsync();

            var pagedResult = new PagedResult<RatingVM>()
            {
                TotalRecords = totalRow,
                PageSize = req.PageSize,
                PageIndex = req.PageIndex,
                Items = data
            };

            return pagedResult;
        }

        //public async Task<List<RatingVM>> GetRatingByProduct(int productId)
        //{
        //    var query = (from r in _dbContext.Ratings
        //                 join u in _dbContext.Users
        //                 on r.UserId equals u.Id
        //                 where r.ProductId == productId
        //                 select new { r, u });


        //    var data = await query.Select(res => new RatingVM()
        //    {
        //        Id = res.r.Id,
        //        Star = (int)res.r.Star,
        //        Comment = res.r.Comment,
        //        CreatedDate = DateTime.Now,
        //        ProductId = res.r.ProductId,
        //        UserId = res.r.UserId,
        //        FirstName = res.u.FirstName,
        //        LastName = res.u.LastName,
        //        Username = res.u.UserName,
        //        ImageUrl = res.u.ImageUrl
        //    }).ToListAsync();

        //    return data;
        //}
    }
}

