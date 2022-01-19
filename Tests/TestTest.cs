using System;
using MvvmCross.Tests;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestTest : MvxIoCSupportingTest
    {
        [Test]
        public void Pass()
        {
            Setup(); // Mvx
            Assert.True(true);
        }

        [Test]
        public void Fail()
        {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore()
        {
            Assert.True(false);
        }
    }
}
