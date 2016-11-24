using Microsoft.VisualStudio.TestTools.UnitTesting;
using BSRBankWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSRBankWCF.Tests
{
    [TestClass()]
    public class AccountUtilsTests
    {
        [TestMethod()]
        public void createAccountIdTest()
        {
            Assert.AreEqual(AccountUtils.CreateAccountId(15),"0000000000000015");
            Assert.AreEqual(AccountUtils.CreateAccountId(150),"0000000000000150");
            Assert.AreEqual(AccountUtils.CreateAccountId(96),"0000000000000096");
            Assert.AreEqual(AccountUtils.CreateAccountId(1500),"0000000000001500");
            Assert.AreEqual(AccountUtils.CreateAccountId(1),"0000000000000001");
            Assert.AreEqual(AccountUtils.CreateAccountId(15),"0000000000000015");
        }

        [TestMethod]
        public void createAccountNumberTest()
        {
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 15)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 1)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 150000)));
        }
    }
}