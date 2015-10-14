using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleStats.Core.Statistics
{
    public class StatisticsPrinter
    {
        public static void Print(IStatistics statistics)
        {
            var sb = new StringBuilder();
            sb.AppendLine("==================================================================================");
            sb.AppendFormat("Statistics for {0} {1}\n", statistics.Make, statistics.Model);

            sb.AppendFormat("Highest Price:      {0}\n", statistics.HighestPrice);
            sb.AppendFormat("Lowest Price:       {0}\n", statistics.LowestPrice);
            sb.AppendFormat("Mean Price:         {0}\n", Math.Round(statistics.MeanPrice, 2));
            sb.AppendFormat("Median Price:       {0}\n", statistics.MedianPrice);
            sb.AppendFormat("Mode Price:         {0}\n", statistics.ModePrice);
            sb.AppendFormat("Standard Deviation: {0}\n", Math.Round(statistics.StandardDeviation, 2));
            sb.AppendFormat("Sample Size:        {0}\n", statistics.SampleSize);

            sb.AppendFormat("Mean Price Per Year\n");
            statistics.MeanPriceByYear.ToList().ForEach(y => sb.AppendFormat("  {0}      {1, -10}    sample size: {2}\n", y.Item1, Math.Round(y.Item2, 2), y.Item3));

            sb.AppendFormat("Depreciation By Year\n");
            statistics.DepreciationByYear.ToList().ForEach(y => sb.AppendFormat("  {0}      {1, -10}    sample size: {2}\n", y.Item1, Math.Round(y.Item2, 2), y.Item3));

            sb.AppendFormat("Depreciation By Year Cumulative\n");
            statistics.DepreciationByYearCumulative.ToList().ForEach(y => sb.AppendFormat("  {0}      {1}\n", y.Item1, Math.Round(y.Item2, 2)));

            sb.AppendFormat("Outlier vehicles\n");
            statistics.OutlierVehicles.GroupBy(v => v.Vehicle.Year)
                .ToList()
                .ForEach(y => sb.AppendFormat("  {0}      {1}\n", y.Key, y.Count()));
            sb.AppendFormat("==================================================================================\n");

            Console.WriteLine(sb);
           
        }

        public static void SaveToCsv(IStatistics statistics, string sourceSystem, string rootPath)
        {
            if (!Directory.Exists(rootPath))
                throw new DirectoryNotFoundException(rootPath);

            
            var sb = new StringBuilder();
            sb.AppendFormat("Statistics for {0} {1}\n", statistics.Make, statistics.Model);

            sb.AppendFormat("Highest Price,{0}\n", statistics.HighestPrice);
            sb.AppendFormat("Lowest Price,{0}\n", statistics.LowestPrice);
            sb.AppendFormat("Mean Price,{0}\n", Math.Round(statistics.MeanPrice, 2));
            sb.AppendFormat("Median Price,{0}\n", statistics.MedianPrice);
            sb.AppendFormat("Mode Price,{0}\n", statistics.ModePrice);
            sb.AppendFormat("Standard Deviation,{0}\n", Math.Round(statistics.StandardDeviation, 2));
            sb.AppendFormat("Sample Size,{0}\n", statistics.SampleSize);
            sb.AppendFormat("Outlier vehicles\n");
            statistics.OutlierVehicles.GroupBy(v => v.Vehicle.Year)
                .ToList()
                .ForEach(y => sb.AppendFormat("{0},{1}\n", y.Key, y.Count()));
            

            var filename = string.Format("{0}\\{1}_SUMMARY {2} {3}.csv", rootPath, sourceSystem, statistics.Make, statistics.Model);
            File.WriteAllText(filename, sb.ToString());
            sb.Clear();
            

            statistics.MeanPriceByYear.OrderByDescending(s => s.Item1).ToList().ForEach(y => sb.AppendFormat("{0},{1},sample size,{2}\n", y.Item1, Math.Round(y.Item2, 2), y.Item3));
            filename = string.Format("{0}\\{1}_MeanPricePerYear {2} {3}.csv", rootPath, sourceSystem, statistics.Make, statistics.Model);
            File.WriteAllText(filename, sb.ToString());
            sb.Clear();

            statistics.DepreciationByYear.OrderByDescending(s => s.Item1).ToList().ForEach(y => sb.AppendFormat("{0},{1},sample size,{2}\n", y.Item1, Math.Round(y.Item2, 2), y.Item3));
            filename = string.Format("{0}\\{1}_DepreciationPerYear {2} {3}.csv", rootPath, sourceSystem, statistics.Make, statistics.Model);
            File.WriteAllText(filename, sb.ToString());
            sb.Clear();

            statistics.DepreciationByYearCumulative.OrderByDescending(s => s.Item1).ToList().ForEach(y => sb.AppendFormat("{0},{1}\n", y.Item1, Math.Round(y.Item2, 2)));
            filename = string.Format("{0}\\{1}_DepreciationPerYearCumulative {2} {3}.csv", rootPath, sourceSystem, statistics.Make, statistics.Model);
            File.WriteAllText(filename, sb.ToString());
            sb.Clear();
            
        }
         
    }
}
