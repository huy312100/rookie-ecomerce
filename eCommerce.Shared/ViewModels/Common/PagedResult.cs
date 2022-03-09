using System;
namespace eCommerce.Shared.ViewModels.Common
{
	public class PagedResult<T>:PagedResultBase
	{
		public List<T> Items { set; get; }
	}
}

