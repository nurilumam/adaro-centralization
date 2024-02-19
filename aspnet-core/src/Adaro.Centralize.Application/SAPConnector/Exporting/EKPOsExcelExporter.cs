using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class EKPOsExcelExporter : MiniExcelExcelExporterBase, IEKPOsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EKPOsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEKPOForViewDto> ekpOs)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var ekpo in ekpOs)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("MANDT"), ekpo.EKPO.MANDT},
                        {L("EBELN"), ekpo.EKPO.EBELN},
                        {L("EBELP"), ekpo.EKPO.EBELP},
                        {L("UNIQUEID"), ekpo.EKPO.UNIQUEID},
                        {L("LOEKZ"), ekpo.EKPO.LOEKZ},
                        {L("AEDAT"), ekpo.EKPO.AEDAT},
                        {L("TXZ01"), ekpo.EKPO.TXZ01},
                        {L("MATNR"), ekpo.EKPO.MATNR},
                        {L("BUKRS"), ekpo.EKPO.BUKRS},
                        {L("BEDNR"), ekpo.EKPO.BEDNR},
                        {L("MATKL"), ekpo.EKPO.MATKL},
                        {L("INFNR"), ekpo.EKPO.INFNR},
                        {L("IDNLF"), ekpo.EKPO.IDNLF},
                        {L("KTMNG"), ekpo.EKPO.KTMNG},
                        {L("MENGE"), ekpo.EKPO.MENGE},
                        {L("MEINS"), ekpo.EKPO.MEINS},
                        {L("BPRME"), ekpo.EKPO.BPRME},
                        {L("BPUMZ"), ekpo.EKPO.BPUMZ},
                        {L("BPUMN"), ekpo.EKPO.BPUMN},
                        {L("UMREZ"), ekpo.EKPO.UMREZ},
                        {L("UMREN"), ekpo.EKPO.UMREN},
                        {L("NETPR"), ekpo.EKPO.NETPR},
                        {L("PEINH"), ekpo.EKPO.PEINH},
                        {L("NETWR"), ekpo.EKPO.NETWR},
                        {L("BRTWR"), ekpo.EKPO.BRTWR},
                        {L("AGDAT"), ekpo.EKPO.AGDAT},
                        {L("WEBAZ"), ekpo.EKPO.WEBAZ},
                        {L("MWSKZ"), ekpo.EKPO.MWSKZ},
                        {L("BONUS"), ekpo.EKPO.BONUS},
                        {L("INSMK"), ekpo.EKPO.INSMK},
                        {L("SPINF"), ekpo.EKPO.SPINF},
                        {L("PRSDR"), ekpo.EKPO.PRSDR},
                        {L("BWTAR"), ekpo.EKPO.BWTAR},
                        {L("BWTTY"), ekpo.EKPO.BWTTY},
                        {L("ABSKZ"), ekpo.EKPO.ABSKZ},
                        {L("PSTYP"), ekpo.EKPO.PSTYP},
                        {L("KNTTP"), ekpo.EKPO.KNTTP},
                        {L("KONNR"), ekpo.EKPO.KONNR},
                        {L("KTPNR"), ekpo.EKPO.KTPNR},
                        {L("PACKNO"), ekpo.EKPO.PACKNO},
                        {L("ANFNR"), ekpo.EKPO.ANFNR},
                        {L("BANFN"), ekpo.EKPO.BANFN},
                        {L("BNFPO"), ekpo.EKPO.BNFPO},

                    });
            }

            return CreateExcelPackage("EKPOsList.xlsx", items);

        }
    }
}