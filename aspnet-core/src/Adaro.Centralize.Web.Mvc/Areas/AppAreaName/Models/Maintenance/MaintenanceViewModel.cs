using System.Collections.Generic;
using Adaro.Centralize.Caching.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
        
        public bool CanClearAllCaches { get; set; }
    }
}