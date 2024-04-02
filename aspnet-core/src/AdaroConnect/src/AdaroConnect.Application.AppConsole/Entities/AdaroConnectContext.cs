﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdaroConnect.Application.AppConsole.Entities;

public partial class AdaroConnectContext : DbContext
{
    public AdaroConnectContext(DbContextOptions<AdaroConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostCenter> CostCenters { get; set; }

    public virtual DbSet<Ekko> Ekkos { get; set; }

    public virtual DbSet<Ekpo> Ekpos { get; set; }

    public virtual DbSet<GeneralLedgerAccount> GeneralLedgerAccounts { get; set; }

    public virtual DbSet<Zmm020r> Zmm020rs { get; set; }

    public virtual DbSet<Zmm021r> Zmm021rs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostCenter>(entity =>
        {
            entity.HasIndex(e => e.CostCenterCode, "IX_CostCenters").IsUnique();

            entity.HasIndex(e => e.TenantId, "IX_CostCenters_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ControllingArea)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CostCenterCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CostCenterName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CostCenterShort).HasMaxLength(100);
            entity.Property(e => e.DepartmentName)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.Period)
                .IsRequired()
                .HasMaxLength(10);
        });

        modelBuilder.Entity<Ekko>(entity =>
        {
            entity.ToTable("EKKO");

            entity.HasIndex(e => e.TenantId, "IX_EKKO_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Abgru)
                .HasMaxLength(500)
                .HasColumnName("ABGRU");
            entity.Property(e => e.Adrnr)
                .HasMaxLength(500)
                .HasColumnName("ADRNR");
            entity.Property(e => e.Aedat).HasColumnName("AEDAT");
            entity.Property(e => e.Autlf)
                .HasMaxLength(500)
                .HasColumnName("AUTLF");
            entity.Property(e => e.Bedat).HasColumnName("BEDAT");
            entity.Property(e => e.Bsakz)
                .HasMaxLength(500)
                .HasColumnName("BSAKZ");
            entity.Property(e => e.Bsart)
                .HasMaxLength(500)
                .HasColumnName("BSART");
            entity.Property(e => e.Bstyp)
                .HasMaxLength(500)
                .HasColumnName("BSTYP");
            entity.Property(e => e.Bukrs)
                .HasMaxLength(500)
                .HasColumnName("BUKRS");
            entity.Property(e => e.Bwbdt).HasColumnName("BWBDT");
            entity.Property(e => e.Ebeln)
                .HasMaxLength(500)
                .HasColumnName("EBELN");
            entity.Property(e => e.Ekgrp)
                .HasMaxLength(500)
                .HasColumnName("EKGRP");
            entity.Property(e => e.Ekorg)
                .HasMaxLength(500)
                .HasColumnName("EKORG");
            entity.Property(e => e.Ernam)
                .HasMaxLength(500)
                .HasColumnName("ERNAM");
            entity.Property(e => e.Frggr)
                .HasMaxLength(500)
                .HasColumnName("FRGGR");
            entity.Property(e => e.Frgke)
                .HasMaxLength(500)
                .HasColumnName("FRGKE");
            entity.Property(e => e.Frgsx)
                .HasMaxLength(500)
                .HasColumnName("FRGSX");
            entity.Property(e => e.Frgzu)
                .HasMaxLength(500)
                .HasColumnName("FRGZU");
            entity.Property(e => e.Gwldt).HasColumnName("GWLDT");
            entity.Property(e => e.Ihran).HasColumnName("IHRAN");
            entity.Property(e => e.Inco1)
                .HasMaxLength(500)
                .HasColumnName("INCO1");
            entity.Property(e => e.Inco2)
                .HasMaxLength(500)
                .HasColumnName("INCO2");
            entity.Property(e => e.Kalsm)
                .HasMaxLength(500)
                .HasColumnName("KALSM");
            entity.Property(e => e.Kdatb).HasColumnName("KDATB");
            entity.Property(e => e.Kdate).HasColumnName("KDATE");
            entity.Property(e => e.Knumv)
                .HasMaxLength(500)
                .HasColumnName("KNUMV");
            entity.Property(e => e.Konnr)
                .HasMaxLength(500)
                .HasColumnName("KONNR");
            entity.Property(e => e.Kufix)
                .HasMaxLength(500)
                .HasColumnName("KUFIX");
            entity.Property(e => e.Kunnr)
                .HasMaxLength(500)
                .HasColumnName("KUNNR");
            entity.Property(e => e.Lblif)
                .HasMaxLength(500)
                .HasColumnName("LBLIF");
            entity.Property(e => e.Lifnr)
                .HasMaxLength(500)
                .HasColumnName("LIFNR");
            entity.Property(e => e.Loekz)
                .HasMaxLength(500)
                .HasColumnName("LOEKZ");
            entity.Property(e => e.Lponr).HasColumnName("LPONR");
            entity.Property(e => e.Mandt)
                .HasMaxLength(500)
                .HasColumnName("MANDT");
            entity.Property(e => e.Pincr).HasColumnName("PINCR");
            entity.Property(e => e.Procstat)
                .HasMaxLength(500)
                .HasColumnName("PROCSTAT");
            entity.Property(e => e.Reswk)
                .HasMaxLength(500)
                .HasColumnName("RESWK");
            entity.Property(e => e.Statu)
                .HasMaxLength(500)
                .HasColumnName("STATU");
            entity.Property(e => e.Submi)
                .HasMaxLength(500)
                .HasColumnName("SUBMI");
            entity.Property(e => e.Unsez)
                .HasMaxLength(500)
                .HasColumnName("UNSEZ");
            entity.Property(e => e.Waers)
                .HasMaxLength(500)
                .HasColumnName("WAERS");
            entity.Property(e => e.Weakt)
                .HasMaxLength(500)
                .HasColumnName("WEAKT");
            entity.Property(e => e.Wkurs)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("WKURS");
            entity.Property(e => e.Zbd1p)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ZBD1P");
            entity.Property(e => e.Zbd1t)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ZBD1T");
            entity.Property(e => e.Zbd2p)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ZBD2P");
            entity.Property(e => e.Zbd2t)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ZBD2T");
            entity.Property(e => e.Zbd3t)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ZBD3T");
            entity.Property(e => e.Zterm)
                .HasMaxLength(500)
                .HasColumnName("ZTERM");
        });

        modelBuilder.Entity<Ekpo>(entity =>
        {
            entity.ToTable("EKPO");

            entity.HasIndex(e => e.TenantId, "IX_EKPO_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Abskz)
                .HasMaxLength(500)
                .HasColumnName("ABSKZ");
            entity.Property(e => e.Aedat).HasColumnName("AEDAT");
            entity.Property(e => e.Agdat).HasColumnName("AGDAT");
            entity.Property(e => e.Anfnr)
                .HasMaxLength(500)
                .HasColumnName("ANFNR");
            entity.Property(e => e.Banfn)
                .HasMaxLength(500)
                .HasColumnName("BANFN");
            entity.Property(e => e.Bednr)
                .HasMaxLength(500)
                .HasColumnName("BEDNR");
            entity.Property(e => e.Bnfpo).HasColumnName("BNFPO");
            entity.Property(e => e.Bonus)
                .HasMaxLength(500)
                .HasColumnName("BONUS");
            entity.Property(e => e.Bprme)
                .HasMaxLength(500)
                .HasColumnName("BPRME");
            entity.Property(e => e.Bpumn)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BPUMN");
            entity.Property(e => e.Bpumz)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BPUMZ");
            entity.Property(e => e.Brtwr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BRTWR");
            entity.Property(e => e.Bukrs)
                .HasMaxLength(500)
                .HasColumnName("BUKRS");
            entity.Property(e => e.Bwtar)
                .HasMaxLength(500)
                .HasColumnName("BWTAR");
            entity.Property(e => e.Bwtty)
                .HasMaxLength(500)
                .HasColumnName("BWTTY");
            entity.Property(e => e.Ebeln)
                .HasMaxLength(500)
                .HasColumnName("EBELN");
            entity.Property(e => e.Ebelp).HasColumnName("EBELP");
            entity.Property(e => e.Ematn)
                .HasMaxLength(500)
                .HasColumnName("EMATN");
            entity.Property(e => e.Idnlf)
                .HasMaxLength(500)
                .HasColumnName("IDNLF");
            entity.Property(e => e.Infnr)
                .HasMaxLength(500)
                .HasColumnName("INFNR");
            entity.Property(e => e.Insmk)
                .HasMaxLength(500)
                .HasColumnName("INSMK");
            entity.Property(e => e.Knttp)
                .HasMaxLength(500)
                .HasColumnName("KNTTP");
            entity.Property(e => e.Konnr)
                .HasMaxLength(500)
                .HasColumnName("KONNR");
            entity.Property(e => e.Ktmng).HasColumnName("KTMNG");
            entity.Property(e => e.Ktpnr).HasColumnName("KTPNR");
            entity.Property(e => e.Lgort)
                .HasMaxLength(500)
                .HasColumnName("LGORT");
            entity.Property(e => e.Loekz)
                .HasMaxLength(500)
                .HasColumnName("LOEKZ");
            entity.Property(e => e.Mandt)
                .HasMaxLength(500)
                .HasColumnName("MANDT");
            entity.Property(e => e.Matkl)
                .HasMaxLength(500)
                .HasColumnName("MATKL");
            entity.Property(e => e.Matnr)
                .HasMaxLength(500)
                .HasColumnName("MATNR");
            entity.Property(e => e.Meins)
                .HasMaxLength(500)
                .HasColumnName("MEINS");
            entity.Property(e => e.Menge).HasColumnName("MENGE");
            entity.Property(e => e.Mwskz)
                .HasMaxLength(500)
                .HasColumnName("MWSKZ");
            entity.Property(e => e.Netpr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NETPR");
            entity.Property(e => e.Netwr)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NETWR");
            entity.Property(e => e.Packno)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PACKNO");
            entity.Property(e => e.Peinh)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("PEINH");
            entity.Property(e => e.Prsdr)
                .HasMaxLength(500)
                .HasColumnName("PRSDR");
            entity.Property(e => e.Pstyp)
                .HasMaxLength(500)
                .HasColumnName("PSTYP");
            entity.Property(e => e.Spinf)
                .HasMaxLength(500)
                .HasColumnName("SPINF");
            entity.Property(e => e.Statu)
                .HasMaxLength(500)
                .HasColumnName("STATU");
            entity.Property(e => e.Txz01)
                .HasMaxLength(500)
                .HasColumnName("TXZ01");
            entity.Property(e => e.Umren)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UMREN");
            entity.Property(e => e.Umrez)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("UMREZ");
            entity.Property(e => e.Uniqueid)
                .HasMaxLength(500)
                .HasColumnName("UNIQUEID");
            entity.Property(e => e.Webaz)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("WEBAZ");
            entity.Property(e => e.Werks)
                .HasMaxLength(500)
                .HasColumnName("WERKS");
        });

        modelBuilder.Entity<GeneralLedgerAccount>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FundsCenter).HasMaxLength(255);
            entity.Property(e => e.FundsCenterDescription).HasMaxLength(300);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CostCenter).WithMany(p => p.GeneralLedgerAccounts)
                .HasForeignKey(d => d.CostCenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneralLedgerAccounts_CostCenters");
        });

        modelBuilder.Entity<Zmm020r>(entity =>
        {
            entity.ToTable("ZMM020R");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccountAssignment).HasMaxLength(5);
            entity.Property(e => e.Asset).HasMaxLength(50);
            entity.Property(e => e.CostCenter).HasMaxLength(20);
            entity.Property(e => e.CostCenterDescription).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(500);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.DeletionIndicator).HasMaxLength(5);
            entity.Property(e => e.DocumentId)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DocumentType)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DocumentTypeText)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.EntrySheet).HasMaxLength(50);
            entity.Property(e => e.FirstApprovalName).HasMaxLength(255);
            entity.Property(e => e.FundsCenter).HasMaxLength(50);
            entity.Property(e => e.GoodsReceipt).HasMaxLength(50);
            entity.Property(e => e.ItemCategory).HasMaxLength(5);
            entity.Property(e => e.ItemRequisition)
                .IsRequired()
                .HasMaxLength(5);
            entity.Property(e => e.LastApprovalName).HasMaxLength(255);
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.MaterialGroup).HasMaxLength(50);
            entity.Property(e => e.OutlineAgreement).HasMaxLength(255);
            entity.Property(e => e.Plant).HasMaxLength(10);
            entity.Property(e => e.PrincAgreementItem).HasMaxLength(255);
            entity.Property(e => e.ProcessingStatus).HasMaxLength(50);
            entity.Property(e => e.ProcessingStatusCode).HasMaxLength(50);
            entity.Property(e => e.PurchaseGroup).HasMaxLength(255);
            entity.Property(e => e.PurchaseRequisition)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PurchasingDocument).HasMaxLength(50);
            entity.Property(e => e.PurchasingInfoRec).HasMaxLength(10);
            entity.Property(e => e.ReleaseIndicator).HasMaxLength(50);
            entity.Property(e => e.Requisitioner).HasMaxLength(100);
            entity.Property(e => e.RequisitionerName).HasMaxLength(500);
            entity.Property(e => e.Service).HasMaxLength(50);
            entity.Property(e => e.ServiceItem).HasMaxLength(5);
            entity.Property(e => e.ServiceShortText).HasMaxLength(500);
            entity.Property(e => e.ShortText).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StorageLocation).HasMaxLength(10);
            entity.Property(e => e.SupplierCode).HasMaxLength(100);
            entity.Property(e => e.SupplierName).HasMaxLength(255);
            entity.Property(e => e.UnitOfMeasure).HasMaxLength(50);
            entity.Property(e => e.UnitOfMeasureService).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Wbselement)
                .HasMaxLength(255)
                .HasColumnName("WBSElement");
        });

        modelBuilder.Entity<Zmm021r>(entity =>
        {
            entity.ToTable("ZMM021R");

            entity.HasIndex(e => e.DocumentId, "IX_ZMM021R").IsUnique();

            entity.HasIndex(e => e.TenantId, "IX_ZMM021R_TenantId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccountAssignment).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ApprovalStatus).HasMaxLength(50);
            entity.Property(e => e.AssetNo).HasMaxLength(255);
            entity.Property(e => e.BuyerCode).HasMaxLength(50);
            entity.Property(e => e.BuyerName).HasMaxLength(255);
            entity.Property(e => e.CollectiveNumber).HasMaxLength(50);
            entity.Property(e => e.CostCenter).HasMaxLength(50);
            entity.Property(e => e.CostCenterDescription).HasMaxLength(255);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.DeletionIndicator).HasMaxLength(255);
            entity.Property(e => e.DocumentId)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FuelAllocation).HasMaxLength(255);
            entity.Property(e => e.FundCenter).HasMaxLength(255);
            entity.Property(e => e.Item)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.ItemCategory).HasMaxLength(50);
            entity.Property(e => e.ItemNo).HasMaxLength(50);
            entity.Property(e => e.ItemPr)
                .HasMaxLength(255)
                .HasColumnName("ItemPR");
            entity.Property(e => e.LineNumber).HasMaxLength(10);
            entity.Property(e => e.MaterialGroup).HasMaxLength(255);
            entity.Property(e => e.MaterialService).HasMaxLength(50);
            entity.Property(e => e.OrderUnit).HasMaxLength(50);
            entity.Property(e => e.OurReference).HasMaxLength(255);
            entity.Property(e => e.OutlineAgreement).HasMaxLength(255);
            entity.Property(e => e.Period).HasMaxLength(50);
            entity.Property(e => e.Picdept)
                .HasMaxLength(255)
                .HasColumnName("PICDept");
            entity.Property(e => e.Picsect)
                .HasMaxLength(255)
                .HasColumnName("PICSect");
            entity.Property(e => e.Plant).HasMaxLength(50);
            entity.Property(e => e.PoapprovalName)
                .HasMaxLength(255)
                .HasColumnName("POApprovalName");
            entity.Property(e => e.PofirstApprovalDate).HasColumnName("POFirstApprovalDate");
            entity.Property(e => e.PolastApprovalDate).HasColumnName("POLastApprovalDate");
            entity.Property(e => e.Postatus)
                .HasMaxLength(50)
                .HasColumnName("POStatus");
            entity.Property(e => e.PrfinalFirstApprovalDate).HasColumnName("PRFinalFirstApprovalDate");
            entity.Property(e => e.PrfinalLastApprovalDate).HasColumnName("PRFinalLastApprovalDate");
            entity.Property(e => e.PurchaseRequisition).HasMaxLength(255);
            entity.Property(e => e.PurchasingDocType)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.PurchasingDocTypeDescription).HasMaxLength(50);
            entity.Property(e => e.PurchasingDocument)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PurchasingGroup).HasMaxLength(50);
            entity.Property(e => e.ReleaseIndicator).HasMaxLength(50);
            entity.Property(e => e.Rfqno)
                .HasMaxLength(255)
                .HasColumnName("RFQNo");
            entity.Property(e => e.ShortText).HasMaxLength(500);
            entity.Property(e => e.SupplierCode).HasMaxLength(50);
            entity.Property(e => e.SupplierName).HasMaxLength(255);
            entity.Property(e => e.TaxCode).HasMaxLength(50);
            entity.Property(e => e.Wbselement)
                .HasMaxLength(255)
                .HasColumnName("WBSElement");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}