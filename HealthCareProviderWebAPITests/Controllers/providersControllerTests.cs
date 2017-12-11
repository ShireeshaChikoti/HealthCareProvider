using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCareProviderWebAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HealthCareProviderWebAPI.Controllers.Tests
{
    [TestClass()]
    public class providersControllerTests
    {
        [TestMethod()]
        public void Get_StatusCodeOk_Test()
        {
            var controller = new providersController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Get(10,11);
            
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod()]
        public void Get_StatusCodeNoContent_Test()
        {
            var controller = new providersController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Get(12, 11);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}