using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnicomWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomWeb.Controllers.Tests
{
    [TestClass()]
    public class OutletDestinationValidationControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllRoutesTest()
        {
            OutletDestinationValidationController cl = new OutletDestinationValidationController();
            var r =  cl.GetAllRoutes();
            Assert.IsNotNull(r);
        }

        [TestMethod()]
        public void GetNodesForJsTree()
        {
            //OutletDestinationValidationController cl = new OutletDestinationValidationController();
            //var r =cl.GetRoutes();
            //var nodes =  cl.GetRouteTreeList(r, Guid.Empty);
            //Assert.AreNotEqual(nodes.Count(), 0);
        }
    }
}