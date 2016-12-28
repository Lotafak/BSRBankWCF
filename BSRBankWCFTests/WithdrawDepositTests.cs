using System;
using BSRBankWCF.Models.WithdrawDepositImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BSRBankWCFTests
{
    [TestClass]
    public class WithdrawDepositTests
    {
        [TestMethod]
        public void DepositAmountGetTest()
        {
            var listExamples = new[]
{
                new Deposit(200, "nvm", "1236561283"), 
                new Deposit(2.11M, "nvm", "1236561283"), 
                new Deposit(0.00M, "nvm", "1236561283"), 
            };
            var listAnswers = new[]
            {
                200,
                2.11M,
                0.00M
            };
            for ( var i = 0; i < listExamples.Length; i++ )
            {
                Assert.AreEqual(listExamples[i].Amount, listAnswers[i]);
            }
        }

        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod]
        //public void AmountLessThanZeroDepositTest()
        //{
        //    var dep = new Deposit(-20M, "nvm", "1236561283");
        //}

        [TestMethod]
        public void WithdrawAmountGetTest()
        {
            var listExamples = new[]
            {
                new Withdraw(200, "nvm", "1236561283"),
                new Withdraw(2.11M, "nvm", "1236561283"),
                new Withdraw(0.00M, "nvm", "1236561283")
            };
            var listAnswers = new[]
            {
                -200,
                -2.11M,
                -0.00M
            };
            for (var i = 0; i < listExamples.Length; i++)
            {
                Assert.AreEqual(listExamples[i].Amount, listAnswers[i]);
            }
        }

        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod]
        //public void AmountLessThanZeroWithdrawTest()
        //{
        //    var dep = new Withdraw(-20M, "nvm", "1236561283");
        //}
    }
}
