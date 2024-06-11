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
    public class HomePageTests
    {
        
        [TestMethod()]
        public void GetCurrentUserTest()
        {
            ;
        }

        [TestMethod()]
        public void LoadUserFioTest()
        {
            User user = new User()
            {
                FirstName = "Абоба",
                LastName = "Олеговна"
                
            };
            string expected = "Олеговна Абоба";
            string actual = HomePage.LoadUserFio(user);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void LoadUserFioTest1()
        {
            User user = new User()
            {
                FirstName = "Николай",
                LastName = "Воронков"

            };
            string expected = "Воронков Николай";
            string actual = HomePage.LoadUserFio(user);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void LoadUserFioTestFalse()
        {
            User user = new User()
            {
                FirstName = "Николай",
                LastName = "Воронков"

            };
            string expected = "Воронков Николайq";
            string actual = HomePage.LoadUserFio(user);
            if(actual == expected)
                Assert.Fail(actual);
            
        }
        [TestMethod()]
        public void LoadUserFioTestFalse2()
        {
            User user = new User()
            {
                FirstName = "Николай",
                LastName = "Воронков"

            };
            string expected = "Николай Воронков";
            string actual = HomePage.LoadUserFio(user);
            if (actual == expected)
                Assert.Fail(actual);

        }

    }
}