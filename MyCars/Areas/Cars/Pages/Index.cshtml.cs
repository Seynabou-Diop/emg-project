#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCars.Data;
using MyCars.Models;

namespace MyCars.Areas.Cars.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext ctx;

        public IndexModel(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IList<Car> Car { get;set; }

        public async Task OnGetAsync()
        {
            Car = await ctx.Cars.Include(c => c.Image).ToListAsync();
        }
    }
}
