#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCars.Data;
using MyCars.Models;
using MyCars.Services;

namespace MyCars.Areas.Cars.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext ctx;
        private readonly ImageService imageService;

        public CreateModel(ApplicationDbContext ctx, ImageService imageService)
        {
            this.ctx = ctx;
            this.imageService = imageService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
{
    var emptyCar = new Car();

    if (null != Car.Image)
        emptyCar.Image = await imageService.UploadAsync(Car.Image);

    if (await TryUpdateModelAsync(emptyCar, "car",
        c => c.CodeVIN, c => c.Year, c => c.Brand, c => c.Model, c => c.Trim,
        c => c.PurchaseDate, c => c.PurchasePrice, c => c.Repairs, c => c.RepairCosts,
        c => c.SaleAvailableDate, c => c.SalePrice, c => c.SaleDate, c => c.Sold))
    {
        ctx.Cars.Add(emptyCar);
        await ctx.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    return Page();
}

    }
}
