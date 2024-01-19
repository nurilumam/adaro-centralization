using Adaro.Centralize.MasterDataRequest;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterDataRequest
{
    [Table("MaterialRequests")]
    public class MaterialRequest : CreationAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string RequestNo { get; set; }

        public virtual MaterialRequestStatus RequestStatus { get; set; }

        [Required]
        public virtual string MaterialName { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        public virtual string Purpose { get; set; }

        public virtual string MaterialType { get; set; }

        public virtual string Category { get; set; }

        public virtual string Size { get; set; }

        public virtual string UoM { get; set; }

        public virtual string PackageSize { get; set; }

        [Required]
        public virtual string GeneralLedger { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Weight { get; set; }
        //File

        public virtual Guid? Picture { get; set; } //File, (BinaryObjectId)

        public virtual string ImageColletion { get; set; }

        public virtual Guid? UNSPSCId { get; set; }

        [ForeignKey("UNSPSCId")]
        public UNSPSC UNSPSCFk { get; set; }

        public virtual Guid? ValuationClassId { get; set; }

        [ForeignKey("ValuationClassId")]
        public GeneralLedgerMapping ValuationClassFk { get; set; }

    }
}