namespace Adaro.Centralize.Finance.Dtos
{
    public class GetTransferBudgetDetailForViewDto
    {
        public TransferBudgetDetailDto TransferBudgetDetail { get; set; }

        public string CostCenterDisplayProperty { get; set; }

        public string GeneralLedgerAccountFundsCenter { get; set; }

    }
}