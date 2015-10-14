using System;
using System.Collections.Generic;
using System.Linq;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Statistics
{
    public class Statistics : IStatistics
    {
        private readonly IList<IVehicle> _vehicles;
        private readonly IList<OutlierVehicle> _outlierVehicles;
        
        public Statistics(IList<IVehicle> vehicles, bool removeOutliers = true)
        {
            if (vehicles == null || !vehicles.Any())
                throw new ArgumentException("Vehicles is null or count is 0. Must be greater than 0");
            
            _vehicles = vehicles;
            _outlierVehicles = new List<OutlierVehicle>();
            Make = _vehicles.First().Make;
            Model = _vehicles.First().Model;
            
            if (removeOutliers)
            {
                RemoveOutliers();
            }
        }

        public IEnumerable<IVehicle> Vehicles
        {
            get { return _vehicles.OrderByDescending(o => o.Year); }
        }

        public IList<OutlierVehicle> OutlierVehicles
        {
            get { return _outlierVehicles; }
        }


        private void RemoveOutliers()
        {
            var zeroPriced = _vehicles.Where(p => p.Price == 0);
            zeroPriced.ToList().ForEach(v => RemoveVehicleAndAddAsOutlier(v, 0, OutlierType.ZeroPrice));

            // http://www.ehow.com/how_5201412_calculate-outliers.html
            foreach (var year in _vehicles.GroupBy(v => v.Year).Select(y => y.Key))
            {
                var orderedVehiclesByYear = _vehicles.Where(v => v.Year == year).OrderBy(v => v.Price).ToList();
                if (orderedVehiclesByYear.Count <= 3)
                    continue;

                var count = orderedVehiclesByYear.Count();
                var isEven = count % 2 == 0;
                int upperTwentyFithIndex = (int)(count - (count * .25));
                int lowerTwentyFithIndex = (int)(count * .25);

                double upperTwentyFithPrice;
                double lowerTwentyFithPrice;
                if (isEven)
                {
                    upperTwentyFithPrice = (orderedVehiclesByYear[upperTwentyFithIndex - 1].Price +
                                            orderedVehiclesByYear[upperTwentyFithIndex].Price) / 2;
                    lowerTwentyFithPrice = (orderedVehiclesByYear[lowerTwentyFithIndex - 1].Price +
                                            orderedVehiclesByYear[lowerTwentyFithIndex].Price) / 2;
                }
                else
                {
                    upperTwentyFithPrice = orderedVehiclesByYear[upperTwentyFithIndex - 1].Price;
                    lowerTwentyFithPrice = orderedVehiclesByYear[lowerTwentyFithIndex - 1].Price;
                }

                var iq = upperTwentyFithPrice - lowerTwentyFithPrice;
                var upperRange = upperTwentyFithPrice + (iq * 3);
                var lowerRange = lowerTwentyFithPrice - (iq * 3);

                orderedVehiclesByYear.Where(v => v.Price >= upperRange).ToList().ForEach(p => RemoveVehicleAndAddAsOutlier(p, upperRange, OutlierType.Above));
                orderedVehiclesByYear.Where(v => v.Price <= lowerRange).ToList().ForEach(p => RemoveVehicleAndAddAsOutlier(p, lowerRange, OutlierType.Under));
            }
        }

        public double MeanPrice
        {
            get { return _vehicles.Average(p => p.Price); }
        }

        public double MedianPrice
        {
            get
            {
                var orderedVehicles = _vehicles.OrderBy(p => p.Price);
                int totalNumber = _vehicles.Count();
                int position = totalNumber / 2;

                double median;
                if (totalNumber % 2 == 0)
                {
                    median = (orderedVehicles.ElementAt(position).Price + orderedVehicles.ElementAt(position - 1).Price) / 2;
                }
                else
                {
                    median = orderedVehicles.ElementAt(position).Price;
                }

                return median;
            }
        }

        public double ModePrice
        {
            get
            {
                return _vehicles.GroupBy(p => p.Price)
                    .OrderByDescending(group => group.Count())
                    .Select(p => p.Key)
                    .FirstOrDefault();

            }
        }

        public double StandardDeviation
        {
            get
            {
                double average = MeanPrice;
                double sumOfSquaresOfDifferences = _vehicles.Select(val => (val.Price - average) * (val.Price - average)).Sum();
                double sd = Math.Sqrt(sumOfSquaresOfDifferences / _vehicles.Count());
                return sd;
            }
        }


        public double LowestPrice
        {
            get { return _vehicles.OrderBy(p => p.Price).First().Price; }
        }

        public double HighestPrice
        {
            get { return _vehicles.OrderByDescending(p => p.Price).First().Price; }
        }

        public string Make { get; private set; }
        public string Model { get; private set; }


        public int SampleSize
        {
            get { return _vehicles.Count(); }
        }

        public IEnumerable<Tuple<int, double, int>> MeanPriceByYear
        {
            get
            {
                return _vehicles.GroupBy(vehicle => vehicle.Year)
                    .Select(group => new Tuple<int, double, int>(group.Key, group.Average(vehicle => vehicle.Price), group.Count()))
                    .OrderByDescending(t => t.Item1);
            }
        }

        public IEnumerable<Tuple<int, double, int>> DepreciationByYear
        {
            get 
            {
                var meanPricePerYear = MeanPriceByYear;
                var meanPricePerYear2 = MeanPriceByYear;

                var joinedMeanPricePerYear2 = from p in meanPricePerYear
                    join i in meanPricePerYear2 on p.Item1 equals i.Item1 - 1 into ps
                    from i in ps.DefaultIfEmpty()
                    select new Tuple<int, double, double, int>(p.Item1, p.Item2, i != null ? i.Item2 : 0, p.Item3);
 
                var results = joinedMeanPricePerYear2.Select(p => new Tuple<int, double, int>(p.Item1, -1 * CalculateDepreciation(p.Item3, p.Item2), p.Item4));
                return results;
            }
        }

        public IEnumerable<Tuple<int, double>> DepreciationByYearCumulative
        {
            get
            {
                var newVehicleMeanPrice = MeanPriceByYear.FirstOrDefault();
                return _vehicles
                    .GroupBy(vehicle => vehicle.Year)
                    .OrderByDescending(p => p.Key)
                    .Select(group => new Tuple<int, double>(group.Key, -1 * CalculateDepreciation(newVehicleMeanPrice.Item2, group.Average(v => v.Price))));
            }
        }

        private void RemoveVehicleAndAddAsOutlier(IVehicle vehicle, double limit, OutlierType outlierType)
        {
            _vehicles.Remove(vehicle);
            _outlierVehicles.Add(new OutlierVehicle(vehicle, outlierType, limit));

            
        }

        private double CalculateDepreciation(double year1, double year2)
        {
            if (year1 == 0)
                return 0;

            return (year2 - year1) / year1 * -100;
        }
    }
}
