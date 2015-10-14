using System;

namespace VehicleStats.Core.Extraction
{
    public class ExtractionArguments : IExtractionArguments
    {
        public const int MaxDuration = 15;

        private ExtractionArguments()
        {}

        public string Make { get; set; }

        public string Model { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public static IExtractionArguments Create(string make, string model, int from, int to)
        {
            if (make != null && make.Trim() == string.Empty) 
                throw new ArgumentException("Make cannot be null or an empty string", "make");

            if (model != null && model.Trim() == string.Empty) 
                throw new ArgumentException("Make cannot be null or an empty string", "model");

            ExtractionArguments args = new ExtractionArguments() { Make = make, Model = model, From = from, To = to };
            args.CheckYearRange();
            return args;
        }

        private void CheckYearRange()
        {
            if (To - From > MaxDuration)
                throw new ArgumentException("Year duration should be less than " + MaxDuration.ToString(), "from or to");
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", Make.Replace("{", string.Empty).Replace("}", string.Empty), Model, From, To);
        }
    }
}
