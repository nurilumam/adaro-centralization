namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetMaterialForViewDto
    {
        public MaterialDto Material { get; set; }

        public string MaterialGroupDisplayProperty { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

    }
}