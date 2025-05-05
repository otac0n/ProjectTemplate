// Copyright © John Gietzen. All Rights Reserved. This source is subject to the LICENSE license. Please see license.md for more information.

namespace Farkle.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class Class1Tests
    {
        [Test]
        public void Who_When_What()
        {
            var subject = new Class1();
            Assert.That(subject, Is.Not.Null);
        }
    }
}
