using SearchFight.Configuration;
using SearchFight.Configuration.Wrapper;
using SearchFight.Core;
using System;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("You must enter 2 terms at least");
                return;
            }

            var config = new SearchFightSectionWrapper(SearchFightSection.Configuration);
            var searcherLoader = new SearcherLoader(config);
            var searchProcess = new SearchProcess(searcherLoader, new WinnerSearchCalculator());

            var processResult = searchProcess.Run(args).Result;

            Display(processResult);

            Console.ReadLine();
        }

        static void Display(TotalSearchResult totalSearchResult)
        {
            Console.WriteLine("====== Process Report ======");
            totalSearchResult.SearchResults.ForEach(sr => 
            {
                Console.Write($"[{sr.SearchTerm}] => ");
                foreach (var searcherResult in sr.SearcherResults)
                {
                    Console.Write($"{searcherResult.SearcherName}: {searcherResult.Total} ");
                }
                Console.WriteLine("");
            });

            totalSearchResult.IndividualWinners.ForEach(iw => 
            {
                Console.WriteLine($"{iw.SearcherName} winner: {iw.SearchTerm}");
            });

            Console.WriteLine($"Total winner: {totalSearchResult.GlobalWinner}");
            Console.WriteLine("============================");
        }
    }
}
