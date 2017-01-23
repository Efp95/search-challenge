using FluentAssertions;
using Moq;
using SearchFight.Configuration.Wrapper;
using SearchFight.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SearchFight.Tests.Core
{
    public class SearcherLoaderTest
    {
        private Mock<ISearchFightSectionWrapper> _searchFightSectionWrapperMock;

        public SearcherLoaderTest()
        {
            _searchFightSectionWrapperMock = new Mock<ISearchFightSectionWrapper>();
        }

        [Fact]
        public void It_Should_ReturnEmptyList_When_EmptyWrapperIsSent()
        {
            var seacherLoader = new SearcherLoader(_searchFightSectionWrapperMock.Object);
            var result = seacherLoader.Handle();

            result.Should().NotBeNull();
        }

        [Fact]
        public void It_Should_ReturnSearcherList_When_ValidWrapperIsSent()
        {
            var wrappedSearchers = new List<SearcherElementWrapper>
            {
                new SearcherElementWrapper
                {
                    Type = "SearchFight.Tests.Stubs.Searchers.SimpleTestSearcher, SearchFight.Tests"
                },
                new SearcherElementWrapper
                {
                    Type = "SearchFight.Tests.Stubs.Searchers.FailTestSearcher, SearchFight.Tests"
                }
            };

            _searchFightSectionWrapperMock.Setup(sfs => sfs.Searchers)
                .Returns(wrappedSearchers);

            var searcherLoader = new SearcherLoader(_searchFightSectionWrapperMock.Object);
            var loadedSearchers = searcherLoader.Handle();

            loadedSearchers.Should().NotBeNull();
            loadedSearchers.Count().Should().Be(wrappedSearchers.Count);
        }

        [Fact]
        public void It_Should_ThrowInvalidOperationException_When_InvalidTypeIsTriedToBeInstantiated()
        {
            var wrappedSearchers = new List<SearcherElementWrapper>
            {
                new SearcherElementWrapper
                {
                    Type = "InvalidNamespace.InvalidClass, InvalidAssembly"
                }
            };

            _searchFightSectionWrapperMock.Setup(sfs => sfs.Searchers)
                .Returns(wrappedSearchers);

            var searcherLoader = new SearcherLoader(_searchFightSectionWrapperMock.Object);
            Assert.Throws<InvalidOperationException>(() => searcherLoader.Handle());
        }
    }
}
