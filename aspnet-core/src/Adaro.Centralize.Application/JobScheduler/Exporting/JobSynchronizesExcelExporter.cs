using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.JobScheduler.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.JobScheduler.Exporting
{
    public class JobSynchronizesExcelExporter : MiniExcelExcelExporterBase, IJobSynchronizesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public JobSynchronizesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetJobSynchronizeForViewDto> jobSynchronizes)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var jobSynchronize in jobSynchronizes)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("JobName"), jobSynchronize.JobSynchronize.JobName},
                        {L("JobType"), jobSynchronize.JobSynchronize.JobType},
                        {L("DataSource"), jobSynchronize.JobSynchronize.DataSource},
                        {L("LastStatus"), jobSynchronize.JobSynchronize.LastStatus},
                        {L("LastUpdate"), jobSynchronize.JobSynchronize.LastUpdate},

                    });
            }

            return CreateExcelPackage("JobSynchronizesList.xlsx", items);

        }
    }
}