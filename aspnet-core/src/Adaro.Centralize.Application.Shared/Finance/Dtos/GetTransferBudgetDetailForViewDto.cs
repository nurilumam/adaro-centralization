namespace Adaro.Centralize.Finance.Dtos
{
    public class GetTransferBudgetDetailForViewDto
    {
        public TransferBudgetDetailDto TransferBudgetDetail { get; set; }

        public string CostCenterCostCenterName { get; set; }

        public string GeneralLedgerMappingGLAccount { get; set; }

    }
}