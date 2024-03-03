using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCars.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(17)]
        [Display(Name = "Code VIN")]
        public string CodeVIN { get; set; }

        [Range(1990, 2024, ErrorMessage = "Year must be 1990 or later")]
        [Display(Name = "Année")]
        public int Year { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Marque")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Modèle")]
        public string Model { get; set; }

        [StringLength(50)]
        [Display(Name = "Finition")]
        public string Trim { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date d'achat")]
        public DateTime PurchaseDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue)]
        [Display(Name = "Prix d'achat")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "Réparations")]
        public string Repairs { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue)]
        [Display(Name = "Coûts de réparation")]
        public decimal RepairCosts { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de disponibilité à la vente")]
        public DateTime SaleAvailableDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue)]
        [Display(Name = "Prix de vente")]
        public decimal SalePrice { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de vente")]
        public DateTime? SaleDate { get; set; }

        [Display(Name = "Vendue")]
        public bool Sold { get; set; }

        public virtual Image? Image { get; set; }

    }
}
