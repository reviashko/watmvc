using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUrlManager
    {
        string GetRealUrl(string virtualUrl);
        string Check301(string url);
        string GetVirtualUrl(int id, byte urlKind);
    }
}
