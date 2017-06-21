using Domain.Entities;
using System.Collections.Generic;

namespace WatMvc.Models
{
    public class ProductViewModels : IViewModel
    {
        public List<CatalogMenuItem> MenuItems { get; set; }
        public Product Product { get; set; }
    }
}
