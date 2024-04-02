﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AdaroConnect.Application.AppConsole.Entities;

public partial class GeneralLedgerAccount
{
    public Guid Id { get; set; }

    public int? TenantId { get; set; }

    public string FundsCenter { get; set; }

    public string FundsCenterDescription { get; set; }

    public Guid CostCenterId { get; set; }

    public double? ConsumableBudget { get; set; }

    public double? ConsumedBudget { get; set; }

    public double? AvailableAmount { get; set; }

    public double? CurrentBudget { get; set; }

    public double? CommitmentActuals { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual CostCenter CostCenter { get; set; }
}