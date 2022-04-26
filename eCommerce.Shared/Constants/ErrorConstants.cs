using System;
namespace eCommerce.Shared.Constants
{
	public class ErrorConstants
	{
		//Common
		public const string APIPermissionDenied = "Permission Denied";

		//User
		public const string APILoginError = "Username or password is incorrect";
		public const string APIUserRegisterError = "Register Unsuccessfully";
		public const string APIUserUpdateError = "Update User Unsuccessfully";
		public const string APIUserDeleteError = "Delete User Unsuccessfully";

		//Brand
		public const string APIGetBrandError = "Get Brand Unsuccessfully";

		//Product
		public const string APIGetProductByIdError = "Get Product By Id Unsuccessfully";
		public const string APIGetProductImageByIdError = "Get Product Image By Id Unsuccessfully";
		public const string APIGetProductByCategoryError = "Get Product By Category Unsuccessfully";
		public const string APICreateProductError = "Create Product Unsuccessfully";
		public const string APIUpdateProductError = "Update Product Unsuccessfully";
		public const string APIDeleteProductError = "Delete Product Unsuccessfully";

		//Category
		public const string APIGetCategoryError = "Get Category Unsuccessfully";
		public const string APIGetCategoryByIdError = "Get Category By Id Unsuccessfully";
		public const string APICreateCategoryError = "Create Category Unsuccessfully";
		public const string APIUpdateCategoryError = "Update Category Unsuccessfully";
		public const string APIDeleteCategoryError = "Delete Category Unsuccessfully";

		//Rating
		public const string APICreateRatingError = "Rating Unsuccessfully";

		//Order
		public const string APIGetOrderError = "Get Order Unsuccessfully";
		public const string APIOrderError = "Order Unsuccessfully";

	}
}

