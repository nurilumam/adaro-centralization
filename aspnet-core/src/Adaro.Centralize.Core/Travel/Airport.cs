using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.Travel
{
    [Table("Airports")]
    public class Airport : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(AirportConsts.MaxAirportNameLength, MinimumLength = AirportConsts.MinAirportNameLength)]
        public virtual string AirportName { get; set; }

        [Required]
        [StringLength(AirportConsts.MaxIATALength, MinimumLength = AirportConsts.MinIATALength)]
        public virtual string IATA { get; set; }

        [StringLength(AirportConsts.MaxCityLength, MinimumLength = AirportConsts.MinCityLength)]
        public virtual string City { get; set; }

        [StringLength(AirportConsts.MaxCategoryLength, MinimumLength = AirportConsts.MinCategoryLength)]
        public virtual string Category { get; set; }

    }
}