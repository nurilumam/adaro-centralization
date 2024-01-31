using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Samples.NetCore.BapiExamples.Models;

namespace AdaroConnect.Samples.NetCore.BapiExamples
{
    public sealed class VendorManager
    {
        private readonly IServiceProvider _serviceProvider;

        public VendorManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public VendorBapiOutputParameter GetVerdorsByCompanyCode(string companyCode)
        {
            using IRfcConnection connection = _serviceProvider.GetService<IRfcConnection>();
            connection?.Connect();

            var inputParameter = new VendorBapiInputParameter
            {
                CompanyCode = companyCode
            };
            using IReadBapi<VendorBapiOutputParameter> rfcFunction = _serviceProvider.GetService<IReadBapi<VendorBapiOutputParameter>>();
            VendorBapiOutputParameter result = rfcFunction?.GetBapi(connection, "BBP_VENDOR_GETLIST", inputParameter);
            return result;
        }

        public void Print(VendorBapiOutputParameter vendor)
        {
            if (vendor == null)
            {
                Console.WriteLine("Vendor Not Found!");
                return;
            }

            Console.WriteLine($"======= Vendor List ================");
            Console.WriteLine($"= TotalRecordCount={vendor.Vendors.Count()}. Printed top 10 record =");
            Console.WriteLine("".PadLeft(20, '='));
            foreach (var vendorItem in vendor.Vendors.Take(10))
                Console.WriteLine($"{vendorItem.VendorNo} - {vendorItem.Name}");
        }
    }
}
