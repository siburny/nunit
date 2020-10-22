// ***********************************************************************
// Copyright (c) 2015 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NUnit.TestData.RepeatingTests
{
    public class RetrySucceedsOnFirstTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void SucceedsEveryTime()
        {
            Count++;
            Assert.IsTrue(true);
        }
    }

    public class RetryFailsEveryTimeFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void FailsEveryTime()
        {
            Count++;
            Assert.IsFalse(true);
        }
    }

    public class RetryFailsEveryTimeWithExceptionFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3, typeof(Exception))]
        public void FailsEveryTime()
        {
            Count++;
            throw new Exception("Deliberate Exception");
        }
    }

    public class RetrySucceedsOnSecondTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void SucceedsOnSecondTry()
        {
            Count++;

            if (Count < 2)
                Assert.IsTrue(false);
        }
    }

    public class RetrySucceedsOnSecondTryWithExceptionFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3, typeof(Exception))]
        public void SucceedsOnSecondTry()
        {
            Count++;

            if (Count < 2)
                throw new Exception("Deliberate Exception");
        }
    }

    public class RetrySucceedsOnThirdTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void SucceedsOnThirdTry()
        {
            Count++;

            if (Count < 3)
                Assert.IsTrue(false);
        }
    }

    public class RetrySucceedsOnThirdTryWithExceptionFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3, typeof(Exception))]
        public void SucceedsOnThirdTry()
        {
            Count++;

            if (Count < 3)
                throw new Exception("Deliberate Exception");
        }
    }

    public class RetryWithIgnoreAttributeFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3), Ignore("Ignore this test")]
        public void RepeatShouldIgnore()
        {
            Assert.Fail("Ignored test executed");
        }
    }

    public class RetryIgnoredOnFirstTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;
            Assert.Ignore("Ignoring");
        }
    }

    public class RetryIgnoredOnSecondTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;

            if (Count < 2)
                Assert.Fail("Failed");

            Assert.Ignore("Ignoring");
        }
    }

    public class RetryIgnoredOnThirdTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;

            if (Count < 3)
                Assert.Fail("Failed");

            Assert.Ignore("Ignoring");
        }
    }

    public class RetryErrorOnFirstTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;
            throw new Exception("Deliberate Exception");
        }
    }

    public class RetryErrorOnSecondTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;

            if (Count < 2)
                Assert.Fail("Failed");

            throw new Exception("Deliberate Exception");
        }
    }

    public class RetryErrorOnThirdTryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void Test()
        {
            Count++;

            if (Count < 3)
                Assert.Fail("Failed");

            throw new Exception("Deliberate Exception");
        }
    }

    public class RetryTestWithCategoryFixture : RepeatingTestsFixtureBase
    {
        [Test, Retry(3), Category("SAMPLE")]
        public void TestWithCategory()
        {
            Count++;
            Assert.IsTrue(true);
        }
    }

    public class RetryTestCaseFixture : RepeatingTestsFixtureBase
    {
        [Retry(3)]
        [TestCase(0)]
        public void FailsEveryTime(int unused)
        {
            Count++;
            Assert.IsTrue(false);
        }
    }

    public sealed class RetryWithoutSetUpOrTearDownFixture
    {
        public int Count { get; private set; }

        [Test, Retry(3)]
        public void SucceedsOnThirdTry()
        {
            Count++;

            if (Count < 3)
                Assert.Fail();
        }

        [Test, Retry(3)]
        public void FailsEveryTime()
        {
            Count++;
            Assert.Fail();
        }

        [Test, Retry(3)]
        public void ErrorsOnFirstTry()
        {
            Count++;
            throw new Exception("Deliberate exception");
        }
    }

    public class RetryTestVerifyAttempt : RepeatingTestsFixtureBase
    {
        [Test, Retry(3)]
        public void NeverPasses()
        {
            Count = TestContext.CurrentContext.CurrentRepeatCount;
            Assert.Fail("forcing a failure so we retry maximum times");
        }

        [Test, Retry(3)]
        public void PassesOnLastRetry()
        {
            Assert.That(Count, Is.EqualTo(TestContext.CurrentContext.CurrentRepeatCount), "expected CurrentRepeatCount to be incremented only after first attempt");
            if (Count < 2) // second Repeat is 3rd Retry (i.e. end of attempts)
            {
                Count++;
                Assert.Fail("forced failure so we will use maximum number of Retries for PassesOnLastRetry");
            }
        }
    }
}
