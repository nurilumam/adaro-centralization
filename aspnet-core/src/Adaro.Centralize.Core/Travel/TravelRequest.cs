using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.Travel
{
    [Table("TravelRequests")]
    public class TravelRequest : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string RequestNo { get; set; }

        public virtual TravelStatus TravelStatus { get; set; }

        public virtual TravelType TravelType { get; set; }

        public virtual DateTime RequestDate { get; set; }

        public virtual DateTime RequestPlanDate { get; set; }

        public virtual string Camp { get; set; }

        public virtual string TransportBus { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual long? UserTravel { get; set; }

        [ForeignKey("UserTravel")]
        public User UserTravelFk { get; set; }

        public virtual Guid? AirportFrom { get; set; }

        [ForeignKey("AirportFrom")]
        public Airport AirportFromFk { get; set; }

        public virtual Guid? AirportTo { get; set; }

        [ForeignKey("AirportTo")]
        public Airport AirportToFk { get; set; }

        public virtual long? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreatedByFk { get; set; }

    }
}