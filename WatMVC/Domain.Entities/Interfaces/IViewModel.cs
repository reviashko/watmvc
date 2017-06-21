using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface IViewModel
    {
        List<CatalogMenuItem> MenuItems { get; set; }
    }
}
