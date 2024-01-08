using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adaro.Centralize.MultiTenancy.HostDashboard.Dto;

namespace Adaro.Centralize.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}