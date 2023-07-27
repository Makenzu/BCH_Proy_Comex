using BCH.Comex.Common.Exceptions;
using System.Diagnostics;

namespace BCH.Comex.Common.Utility
{
    internal static class PerformanceCountersHelper
    {

        private const string LOCAL_MACHINE_NAME = ".";

        internal static void CreateCategory(string categoryName, params CounterCreationData[] counters)
        {
            PerformanceCounterCategory.Create(categoryName, categoryName, PerformanceCounterCategoryType.MultiInstance, new CounterCreationDataCollection(counters));
        }

        internal static CounterCreationData CreateCounter(string counterName, string counterHelp, PerformanceCounterType type)
        {
            return new CounterCreationData()
            {
                CounterName = counterName,
                CounterHelp = counterHelp,
                CounterType = type,
            };
        }


        internal static PerformanceCounter GetCounter(string name, string category)
        {
            if (!PerformanceCounterCategory.Exists(category))
                throw new InstrumentationException(Messages.PerformanceCounterHelper_CategoryNotFound, name, category);

            if (PerformanceCounterCategory.CounterExists(name, category))
            {
                return new PerformanceCounter()
                {
                    CategoryName = category,
                    ReadOnly = false,
                    CounterName = name,
                    MachineName = LOCAL_MACHINE_NAME
                };
            }
            else
            {
                throw new InstrumentationException(Messages.PerformanceCounterHelper_CounterNotFound, name, category);
            }
        }
    }
}
