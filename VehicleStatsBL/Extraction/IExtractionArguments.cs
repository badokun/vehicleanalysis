namespace VehicleStats.Core.Extraction
{
    public interface IExtractionArguments
    {
        string Make { get;  }

        string Model { get;   }

        int From { get;   }

        int To { get;  }
    }

   
}
