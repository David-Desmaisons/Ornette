using System.Collections.Generic;
using FluentAssertions;
using Ornette.Application.Infra;
using Xunit;

namespace Ornette.Application.Tests.Infra
{
    public class DictionaryExtensionTest
    {

        public static IEnumerable<object[]> GetDuplicateExceptTestData()
        {
            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "a", "A"}
                },
                "a",
                new Dictionary<string, string>()
            };

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "a", "A"},
                    { "b", "B"},
                    { "c", "C"}
                },
                "b",
                new Dictionary<string, string>()
                {
                    { "a", "A"},
                    { "c", "C"}
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetDuplicateExceptTestData))]
        public void DuplicateExcept_Copy_Original_but_Provided_Key(Dictionary<string, string> source, string removeKey, IDictionary<string,string> expected)
        {
            var result = source.DuplicateExcept(removeKey);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetDuplicateExceptTestData))]
        public void DuplicateExcept_DO_Change_Original(Dictionary<string, string> source, string removeKey, IDictionary<string, string> _)
        {
            var original = new Dictionary<string, string>(source);
            source.DuplicateExcept(removeKey);
            source.Should().BeEquivalentTo(original);
        }
    }
}
