using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass()]
    public class ProviderDetailTests
    {
        [TestMethod()]
        public void GetFilteredProviderData_ResultNotNull_Test()
        {
            ProviderDetail objProviderDetail = new ProviderDetail();
            SearchCriteria criteria = new SearchCriteria();
            criteria.min_discharges = 14;
            criteria.max_discharges = 20;
            criteria.min_average_covered_charges = 8000;
            criteria.max_average_covered_charges = 8600;
            criteria.min_average_medicare_payments = 4000;
            criteria.max_average_medicare_payments = 5000;
            criteria.state = "AL";
            string result = objProviderDetail.GetFilteredProviderData(criteria);
            
            // Above criteria should return records
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result));
        }

        [TestMethod()]
        public void GetFilteredProviderData_ResultNull_Test()
        {
            ProviderDetail objProviderDetail = new ProviderDetail();
            SearchCriteria criteria = new SearchCriteria();
            criteria.min_discharges = 21;
            criteria.max_discharges = 20;
            string result = objProviderDetail.GetFilteredProviderData(criteria);

            // Above criteria should return null as max discharges is less than min discharges
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }
    }
}