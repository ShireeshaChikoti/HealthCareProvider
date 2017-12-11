using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using DataAccess;

namespace HealthCareProviderWebAPI.Controllers
{
    /// <summary>
    /// Provider controller
    /// </summary>
    public class providersController : ApiController
    {
        // GET api/providers
        public HttpResponseMessage Get(int? min_discharges = null, int? max_discharges = null, decimal? max_average_covered_charges = null,
            decimal? min_average_covered_charges = null, decimal? min_average_medicare_payments = null,
            decimal? max_average_medicare_payments = null, string state = null)
        {
            ProviderDetail objProviderDetail = new ProviderDetail();

            // Load Search criteria based on input query string parameters
            SearchCriteria criteria = new SearchCriteria();
            if (min_discharges.HasValue)
                criteria.min_discharges = min_discharges.Value;

            if (max_discharges.HasValue)
                criteria.max_discharges = max_discharges.Value;

            if (min_average_covered_charges.HasValue)
                criteria.min_average_covered_charges = min_average_covered_charges.Value;

            if (max_average_covered_charges.HasValue)
                criteria.max_average_covered_charges = max_average_covered_charges.Value;

            if (min_average_medicare_payments.HasValue)
                criteria.min_average_medicare_payments = min_average_medicare_payments.Value;

            if (max_average_medicare_payments.HasValue)
                criteria.max_average_medicare_payments = max_average_medicare_payments.Value;

            if (!string.IsNullOrWhiteSpace(state))
                criteria.state = state;

            string result = objProviderDetail.GetFilteredProviderData(criteria);
            if (!string.IsNullOrWhiteSpace(result))
            {
                // Create response object
                var response = Request.CreateResponse(HttpStatusCode.OK);

                // Call Data layer method to return filtered provider Data
                response.Content = new StringContent(result, Encoding.UTF8, "application/json");
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }
    }
}
