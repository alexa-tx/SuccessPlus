using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuccessPlus.Model;
using SuccessPlus.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuccessPlus.View.Tests
{
    [TestClass()]
    public class StudentsTests
    {
        [TestMethod()]
        public void IsEventStatTest()
        {
            var a = 3.312;
            bool expected = true;
            bool actual = Students.IsEventStat(a);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsEventStatTestFalse()
        {
            var a = -3.213;
            bool expected = false;
            bool actual = Students.IsEventStat(a);
            Assert.AreEqual(expected, actual);
        }
    }
}