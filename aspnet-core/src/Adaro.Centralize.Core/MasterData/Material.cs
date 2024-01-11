using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Adaro.Centralize.MasterData
{
    [Table("Materials")]
    public class Material : CreationAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string MaterialNo { get; set; }

        [Required]
        public virtual string MaterialName { get; set; }

        public virtual string MaterialNameSAP { get; set; }

        [Required]
        public virtual string Description { get; set; }

        public virtual string Size { get; set; }

        [Required]
        public virtual string UoM { get; set; }

        public virtual string Brand { get; set; }
        //File

        public virtual Guid? ImageMain { get; set; } //File, (BinaryObjectId)

        public virtual Guid? MaterialGroupId { get; set; }

        [ForeignKey("MaterialGroupId")]
        public MaterialGroup MaterialGroupFk { get; set; }

        public virtual Guid? UNSPSCId { get; set; }

        [ForeignKey("UNSPSCId")]
        public UNSPSC UNSPSCFk { get; set; }

        public virtual Guid? GeneralLedgerMappingId { get; set; }

        [ForeignKey("GeneralLedgerMappingId")]
        public GeneralLedgerMapping GeneralLedgerMappingFk { get; set; }

    }
}