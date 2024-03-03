#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCars.Data; 
using MyCars.Models;
using MyCars.Services;

namespace MyCars.Areas.Cars.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext ctx;
        private readonly ImageService imageService;

        public EditModel(ApplicationDbContext ctx, ImageService imageService)
        {
            this.ctx = ctx;
            this.imageService = imageService;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await ctx.Cars
                .Include(c => c.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Car == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var carToUpdate = await ctx.Cars
                .Include(c => c.Image)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carToUpdate == null)
                return NotFound();

            var uploadedImage = Car.Image;

            if (null != uploadedImage)
            {
                uploadedImage = await imageService.UploadAsync(uploadedImage);

                if (carToUpdate.Image != null)
                {
                    imageService.DeleteUploadedFile(carToUpdate.Image);
                    carToUpdate.Image.Name = uploadedImage.Name;
                    carToUpdate.Image.Path = uploadedImage.Path;
                }
                else
                    carToUpdate.Image = uploadedImage;
            }

            if (await TryUpdateModelAsync(carToUpdate, "car",
                c => c.Brand, c => c.Model, c => c.Trim, c => c.PurchaseDate, c => c.PurchasePrice,
                c => c.Repairs, c => c.RepairCosts, c => c.SaleAvailableDate, c => c.SalePrice,
                c => c.SaleDate, c => c.Sold))
            {
                await ctx.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool CarExists(int id)
        {
            return ctx.Cars.Any(e => e.Id == id);
        }
    }
}
