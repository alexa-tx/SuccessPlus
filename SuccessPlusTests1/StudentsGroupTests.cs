using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuccessPlus.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.View.Tests
{
    [TestClass()]
    public class StudentsGroupTests
    {
        
        [TestMethod()]
        public void IsAdminTest()
        {
            var userRole = 3; 
            bool expected = true;
            bool actual = StudentsGroup.IsAdmin(userRole);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsAdminTestFalse()
        {
            var userRole = 2;
            bool expected = false;
            bool actual = StudentsGroup.IsAdmin(userRole);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsAdminTestFalse2()
        {
            var userRole = 1;
            bool expected = false;
            bool actual = StudentsGroup.IsAdmin(userRole);
            Assert.AreEqual(expected, actual);
        }
    }
}