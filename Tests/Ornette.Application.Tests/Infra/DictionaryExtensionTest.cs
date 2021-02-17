using FluentAssertions;
using Ornette.Application.Infra;
using System.Collections.Generic;
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
        public void DuplicateExcept_Copy_Original_but_Provided_Key(Dictionary<string, string> source, string removeKey, IDictionary<string, string> expected)
        {
            var result = source.DuplicateExcept(removeKey);
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetDuplicateExceptTestData))]
        public void DuplicateExcept_Do_Not_Change_Original(Dictionary<string, string> source, string removeKey, IDictionary<string, string> _)
        {
            var original = new Dictionary<string, string>(source);
            source.DuplicateExcept(removeKey);
            source.Should().BeEquivalentTo(original);
        }

        public static IEnumerable<object[]> GetConvertTestData()
        {
            yield return new object[]
            {
                new Dictionary<string, string[]>()
                {
                    { "a", new []{"A", "a"}}
                },
                new Dictionary<string, List<string>>()
                {
                    { "a", new List<string>(){"A", "a"}}
                }
            };
            yield return new object[]
            {
                new Dictionary<string, string[]>()
                {
                    { "a", new []{"A", "a", "1"}},
                    { "b", new []{"B", "b", "2"}}
                },
                new Dictionary<string, List<string>>()
                {
                    { "a", new List<string>(){"A", "a", "1"}},
                    { "b", new List<string>(){"B", "b", "2"}}
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetConvertTestData))]
        public void Convert_Array_Transform_Dictionary(Dictionary<string, string[]> source, Dictionary<string, List<string>> expected)
        {
            var result = source.Convert();
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetConvertTestData))]
        public void Convert_List_Transform_Dictionary(Dictionary<string, string[]> expected, Dictionary<string, List<string>> source)
        {
            var result = source.Convert();
            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetMergeArrayTestData()
        {
            yield return new object[]
            {
                new Dictionary<string, string[]>()
                {
                    {"a", new[] {"A", "a"}},
                    {"z", new[] {"Z"}}
                },
                new Dictionary<string, string[]>()
                {
                    {"a", new[] {"A", "aa"}},
                    {"c", new[] {"C"}}
                },
                new Dictionary<string, List<string>>()
                {
                    {"a", new List<string>() {"A", "a", "A", "aa"}},
                    {"c", new List<string>() {"C"}},
                    {"z", new List<string>() {"Z"}}
                }
            };
        }


        [Theory]
        [MemberData(nameof(GetMergeArrayTestData))]
        public void Merge_Array_Merge_Dictionaries(Dictionary<string, string[]> source, Dictionary<string, string[]> with, Dictionary<string, List<string>> expected)
        {
            var result = source.Merge(with);
            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetMergeListTestData()
        {
            yield return new object[]
            {
                new Dictionary<string, List<string>>()
                {
                    {"a", new List<string>() {"A", "a"}},
                    {"z", new List<string>()  {"Z"}}
                },
                new Dictionary<string, string[]>()
                {
                    {"a", new[] {"A", "aa"}},
                    {"c", new[] {"C"}}
                },
                new Dictionary<string, List<string>>()
                {
                    {"a", new List<string>() {"A", "a", "A", "aa"}},
                    {"c", new List<string>() {"C"}},
                    {"z", new List<string>() {"Z"}}
                }
            };
        }


        [Theory]
        [MemberData(nameof(GetMergeListTestData))]
        public void Merge_List_Merge_Dictionaries(Dictionary<string, List<string>> source, Dictionary<string, string[]> with, Dictionary<string, List<string>> expected)
        {
            var result = source.Merge(with);
            result.Should().BeEquivalentTo(expected);
        }
    }
}
