using System.Globalization;
using System.Text;
using NUnit.Framework;
using UrUtils.Extensions;

namespace UrUtils.Tests.Extensions
{
    [TestFixture]
    public class StringBuilderExtensionsTest
    {
        StringBuilder StringBuilder;

        [SetUp]
        public void Init()
        {
            StringBuilder = new StringBuilder();
        }

        [TestCase(int.MinValue)]
        [TestCase(-1337)]
        [TestCase(-1)]
        [TestCase(-0)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(1000)]
        [TestCase(100500)]
        [TestCase(int.MaxValue)]
        public void AppendNonAlloc_Int_Valid(int value)
        {
            StringBuilder.AppendNonAlloc(value);

            Assert.AreEqual(value.ToString(CultureInfo.InvariantCulture), StringBuilder.ToString());
        }

        [TestCase(long.MinValue)]
        [TestCase(int.MinValue)]
        [TestCase(-1337)]
        [TestCase(-1)]
        [TestCase(-0)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(42)]
        [TestCase(1000)]
        [TestCase(100500)]
        [TestCase(int.MaxValue)]
        [TestCase(long.MaxValue)]
        public void AppendNonAlloc_Long_Valid(long value)
        {
            StringBuilder.AppendNonAlloc(value);

            Assert.AreEqual(value.ToString(CultureInfo.InvariantCulture), StringBuilder.ToString());
        }
    }
}
