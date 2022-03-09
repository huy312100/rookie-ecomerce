using eCommerce.Shared.ViewModels.Common;
using eCommerce.Shared.ViewModels.Products;

namespace eCommerce.BackendApi.Interfaces
{
	public interface IProductService
	{
		//Task<List<ProductVM>> GetAllProducts();
		Task<ProductVM> GetProductById(int id);
		Task<ProductImageVM> GetImageById(int id);
		Task<PagedResult<ProductVM>> GetProductPaging(PagingRequest req);
		Task<List<ProductVM>> GetProductByCategory(PagingRequest req, int categoryId);
		Task<int> CreateProduct(ProductCreateRequest req);
		Task<int> DeleteProduct(int productId);
	}
}

