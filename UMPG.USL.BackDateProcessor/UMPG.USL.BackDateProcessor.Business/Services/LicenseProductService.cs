using UMPG.USL.BackDateProcessor.Business.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UMPG.USL.Common;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.BackDateProcessor.Business.Services
{
    public class LicenseProductService : ILicenseProductService
    {
        private readonly IDataHarmonizationLogManager _logManager;

        public LicenseProductService(IDataHarmonizationLogManager dataHarmonizationLogManager)
        {
            _logManager = dataHarmonizationLogManager;
        }

        public List<LicenseProduct> GetProductsNew(int licenseId)
        {
            var apiUrl = ConfigHelper.GetAppSettingValue("apiServiceUrl", true);
            apiUrl = string.Format(apiUrl + "/api/licenseProductCTRL/licenseproducts/GetProductsNew/{0}", licenseId);
            return SendGetRequest(apiUrl);
        }

        private List<LicenseProduct> SendGetRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var responseStream = request.GetResponse().GetResponseStream();

            var resultLicenseProducts = new List<LicenseProduct>();
            if (responseStream != null)
            {
                using (var stream = new StreamReader(responseStream))
                {
                    var response = stream.ReadToEnd();
                    JsonConvert.PopulateObject(response, resultLicenseProducts);
                }
            }
            return resultLicenseProducts;
        }
    }
}