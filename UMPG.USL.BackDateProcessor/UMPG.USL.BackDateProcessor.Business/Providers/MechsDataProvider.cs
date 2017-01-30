using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UMPG.USL.Common.Transport;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.BackDateProcessor.Business.Providers
{
    public class MechsDataProvider : IMechsDataProvider
    {
        private readonly IRecsRequestHandler _recsRequestHandler;
        private readonly string _mechsServiceApiUrl = ConfigurationManager.AppSettings["apiServiceUrl"];


        public MechsDataProvider(IRecsRequestHandler recsRequestHandler)
        {
            _recsRequestHandler = recsRequestHandler;
        }

        public List<RecsProductChanges> ValidateLicense(int licenseId)
        {
            var url = _buildRequestUrl(_mechsServiceApiUrl, licenseId);
            return _recsRequestHandler.Get<List<RecsProductChanges>>(url);
        }


        private string _buildRequestUrl(string url, int licenseId)
        {
            return url + "/api/dataHarmonCTRL/methods/CheckLicenseBackDateProblems/" + licenseId.ToString();
        }


    }
}
