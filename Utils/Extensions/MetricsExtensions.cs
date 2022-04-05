namespace Utils.Extensions
{
    using Prometheus;

    public static class MetricsExtensions
    {
        public static void IncSuccessful(this Counter counter, double count = 1D)
        {
            counter.WithLabels("successful").Inc(count);
        }
        
        public static void IncUnsuccessful(this Counter counter, double count = 1D)
        {
            counter.WithLabels("unsuccessful").Inc(count);
        }
    }
}