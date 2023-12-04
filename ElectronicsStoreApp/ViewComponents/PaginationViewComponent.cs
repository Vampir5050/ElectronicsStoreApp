using ElectronicsStoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStoreApp.ViewComponents
{
    public class PaginationViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages, int limit, int? tagId, int? categoryId)
        {
            PaginationViewModel paginationViewModel = new PaginationViewModel()
            {
                TotalPages = totalPages,
                CurrentPage = currentPage,
                LimitItem = limit,
                TagId = tagId,
                CategoryId = categoryId

            };
            return View("Pagination", paginationViewModel);
        }
    }
}
