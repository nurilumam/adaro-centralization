namespace Adaro.Centralize.MasterDataRequest.Dtos
{
    public class GetMaterialRequestForViewDto
    {
        public MaterialRequestDto MaterialRequest { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

    }
}