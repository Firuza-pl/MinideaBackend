using Microsoft.AspNetCore.Mvc;
using Minidea.DAL;

namespace Minidea.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly Db_MinideaContext _context;

        public ContactViewComponent(Db_MinideaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contact = _context.StaticDatas.Where(p => p.IsActive).OrderByDescending(p => p.Id).FirstOrDefault();

            return View(await Task.FromResult(contact));
        }
    }
}
