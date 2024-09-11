using System.Globalization;
using NetLink.Models;

namespace NetLink.Statistics;

public interface IStatisticsService
{
    double GetAverageValue(List<RecordedValue> recordedValues);
    double GetMedianValue(List<RecordedValue> recordedValues);
    double GetStandardDeviation(List<RecordedValue> recordedValues);
    double GetVariance(List<RecordedValue> recordedValues);
    double GetMaxValue(List<RecordedValue> recordedValues);
    double GetMinValue(List<RecordedValue> recordedValues);
}

internal class StatisticsService : IStatisticsService
{
    public double GetAverageValue(List<RecordedValue> recordedValues)
    {
        return recordedValues.Average(x => x.Value);
    }

    public double GetMedianValue(List<RecordedValue> recordedValues)
    {
        recordedValues.Sort((x, y) => x.Value.CompareTo(y.Value));
        var count = recordedValues.Count;
        if (count % 2 == 0)
        {
            return (recordedValues[count / 2 - 1].Value + recordedValues[count / 2].Value) / 2;
        }

        return recordedValues[count / 2].Value;
    }

    public double GetStandardDeviation(List<RecordedValue> recordedValues)
    {
        var average = GetAverageValue(recordedValues);
        var sum = recordedValues.Sum(x => Math.Pow(x.Value - average, 2));
        return Math.Sqrt(sum / recordedValues.Count);
    }

    public double GetVariance(List<RecordedValue> recordedValues)
    {
        var average = GetAverageValue(recordedValues);
        var sum = recordedValues.Sum(x => Math.Pow(x.Value - average, 2));
        return sum / recordedValues.Count;
    }

    public double GetMaxValue(List<RecordedValue> recordedValues)
    {
        return recordedValues.Max(x => x.Value);
    }

    public double GetMinValue(List<RecordedValue> recordedValues)
    {
        return recordedValues.Min(x => x.Value);
    }
}