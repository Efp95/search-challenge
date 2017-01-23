using FluentAssertions;
using SearchFight.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SearchFight.Tests.Core
{
    public class WinnerSearchCalculatorTest
    {
        [Fact]
        public void It_Should_ReturnEmptyResult_When_EmptyListIsSent()
        {
            var winnerSearchCalculator = new WinnerSearchCalculator();
            var totalResult = winnerSearchCalculator.Handle(new List<SearchResult>());

            totalResult.Should().NotBeNull();
            totalResult.SearchResults.Count.Should().Be(default(int));
            totalResult.IndividualWinners.Count.Should().Be(default(int));
            totalResult.GlobalWinner.Should().Be(string.Empty);
        }

        [Fact]
        public void It_Should_ThrowArgumentNullException_When_NullIsSent()
        {
            var winnerSearchCalculator = new WinnerSearchCalculator();
            
            Assert.Throws<ArgumentNullException>(() => winnerSearchCalculator.Handle(null));
        }

        [Fact]
        public void It_Should_ReturnGreatestValuesAsWinners_When_ValidCollectionIsSent()
        {
            var searchResults = new List<SearchResult>
            {
                new SearchResult { SearchTerm = ".net", SearcherResults = new List<SearcherResponse>
                    {
                        new SearcherResponse { SearchTerm = ".net", SearcherName = "SimpleTestSearcher", Total = 1500 },
                        new SearcherResponse { SearchTerm = ".net", SearcherName = "DummyTestSearcher", Total = 1350 }
                    }
                },
                new SearchResult { SearchTerm = "java", SearcherResults = new List<SearcherResponse>
                    {
                        new SearcherResponse { SearchTerm = "java", SearcherName = "SimpleTestSearcher", Total = 1050 },
                        new SearcherResponse { SearchTerm = "java", SearcherName = "DummyTestSearcher", Total = 1700 }
                    }
                }
            };

            var winnerSearchCalculator = new WinnerSearchCalculator();
            var totalResult = winnerSearchCalculator.Handle(searchResults);

            totalResult.Should().NotBeNull();
            totalResult.SearchResults.Count.Should().Be(searchResults.Count);

            totalResult.IndividualWinners.First().SearcherName.Should().Be("SimpleTestSearcher");
            totalResult.IndividualWinners.First().SearchTerm.Should().Be(".net");

            totalResult.IndividualWinners.Last().SearcherName.Should().Be("DummyTestSearcher");
            totalResult.IndividualWinners.Last().SearchTerm.Should().Be("java");

            totalResult.GlobalWinner.Should().Be(".net");
        }
    }
}
