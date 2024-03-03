#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyCars.Data; 
using MyCars.Models;
using MyCars.Services;


namespace MyCars.Areas.Cars.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext ctx;
        private ImageService imageService;

        public DeleteModel(ApplicationDbContext ctx, ImageService imageService)
        {
            this.ctx = ctx;
            this.imageService = imageService;
        }

        [BindProperty]
        public Car Car { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? hasErrorMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await ctx.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Car == null)
            {
                return NotFound();
            }

            if (hasErrorMessage.GetValueOrDefault())
            {
                ErrorMessage = $"Une erreur est survenue lors de la tentative de suppression de {Car.Brand} {Car.Model} ({Car.Id})";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carToDelete = await ctx.Cars
                .Include(c => c.Image)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (carToDelete == null)
            {
                return NotFound();
            }

            try
            {
                imageService.DeleteUploadedFile(carToDelete.Image);
                ctx.Cars.Remove(carToDelete);
                await ctx.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception)
            {
                return RedirectToAction("./Delete", new { id, hasErrorMessage = true });
            }
        }
    }
}
