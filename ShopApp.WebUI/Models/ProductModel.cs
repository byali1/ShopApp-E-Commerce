using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopApp.Entities;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        //[Required]
        //[StringLength(100,ErrorMessage = "Max 100 karakter girilebilir.")]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Fiyat bilgisi giriniz.")]
        [Range(0.1, 999999)]
        
        public decimal? Price { get; set; } //Decimal default olarak 0'dır. Nulleable yapıldığında required işlevsel olur.

        [StringLength(5000,ErrorMessage = "Max 5000 karakter girilebilir.")]
        public string Description { get; set; }
        public List<Category> SelectedCategories{ get; set; }
    }
}
