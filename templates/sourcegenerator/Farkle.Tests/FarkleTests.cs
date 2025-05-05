// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Tests
{
    using Farkle.Tests.Generated;
    using NUnit.Framework;

    public class FarkleTests
    {
        [Test]
        public void TestCase1()
        {
            var output = FarkleFiles.TestCase1;
            Assert.That(output, Contains.Substring("Hello, world!"));
        }
    }
}
