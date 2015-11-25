using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdeaManMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaManMVC.Models;
using Moq;

namespace IdeaManMVC.Controllers.Tests
{
    [TestClass()]
    public class IdeaEntryControllerTests
    {
        [TestInitialize]
        public void FakesInitialize()
        {
            var mock = new Mock<ApplicationDbContext>();
            //mock.Setup(o=>o.Users.r)
        }

        [TestMethod()]
        public void IdeaEntryControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DetailsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }
    }
}