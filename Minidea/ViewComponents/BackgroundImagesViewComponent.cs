using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;
using Minidea.ViewModels;

namespace Minidea.ViewComponents
{
    public class BackgroundImagesViewComponent : ViewComponent
    {
        private readonly Db_MinideaContext _context;

        public BackgroundImagesViewComponent(Db_MinideaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var backgroundImages = _context.BackgroundImages.Where(b => b.IsActive == true).OrderByDescending(b => b.Id);
            return View(await Task.FromResult(backgroundImages));
        }
    }
}
