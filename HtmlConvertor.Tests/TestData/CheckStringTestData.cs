using System.Collections;
using System.Collections.Generic;

namespace EasyHtmlConverter.Tests.TestData
{
    public class CheckStringTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null };
            yield return new object[] { "" };
            yield return new object[] { string.Empty };
            yield return new object[] { "  " };
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}