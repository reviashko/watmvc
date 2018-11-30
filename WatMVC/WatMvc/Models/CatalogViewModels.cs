using Domain.Entities;
using System.Collections.Generic;

namespace WatMvc.Models
{
    public class CatalogViewModels : IViewModel
    {
        public List<CatalogMenuItem> MenuItems { get; set; }
        public List<Product> Products { get; set; }
        public List<PagerItem> Pages { get; set; }
        public int Menu_id { get; set; }
    }
}
