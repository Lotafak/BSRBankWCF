﻿using BSRBankWCF;
using BSRBankWCF.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSRBankWCFTests
{
    [TestClass]
    public class AccountUtilsTests
    {
        [TestMethod]
        public void CreateAccountIdTest()
        {
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(15), "0000000000000015");
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(150), "0000000000000150");
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(96), "0000000000000096");
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(1500), "0000000000001500");
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(1), "0000000000000001");
            Assert.AreEqual(AccountUtils.CreateBankAccountNumberFromId(15), "0000000000000015");
        }

        [TestMethod]
        public void CreateAccountNumberTest()
        {
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 15)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 1)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 150000)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber(
                AccountUtils.CreateAccountNumber("00109562", 00150000)));
            Assert.IsTrue(AccountUtils.ValidateAccountNumber("02001095620000000000000001"));
        }

        [TestMethod]
        public void LoginFromCredentialsTest()
        {
            Assert.AreEqual(AccountUtils.LoginFromCredentials(AccountUtils.Base64Encode("acc:pass")), "acc");
            Assert.AreEqual(AccountUtils.LoginFromCredentials(AccountUtils.Base64Encode("x:x")), "x");
        }
    }
}