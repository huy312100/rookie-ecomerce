using eCommerce.BackendApi.Services;
using Moq;
using eCommerce.BackendApi.Interfaces;

namespace eCommerce.UnitTest
{
    public static class FileStorageService
    {
        public static IFileStorageService IStorageService()
        {
            var fileStorageService = Mock.Of<IFileStorageService>();

            return fileStorageService;
        }
    }
}