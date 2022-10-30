using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using DeviceControl;

namespace MeasuringDevice
{
    public class MeasureMassDevice : MeasureDataDevice
    {

        public MeasureMassDevice(Units deviceUnits, string logFileName)
        {
            unitsToUse = deviceUnits;
            measurementType = DeviceType.MASS;
            loggingFileName = logFileName;
        }

        public override decimal MetricValue()
        {
            decimal metricMostRecentMeasure;

            if (unitsToUse == Units.Metric)
            {
                metricMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
            }
            else
            {
                decimal decimalImperialValue = Convert.ToDecimal(mostRecentMeasure);
                decimal conversionFactor = 0.4536M;
                metricMostRecentMeasure = decimalImperialValue * conversionFactor;
            }

            return metricMostRecentMeasure;
        }

        public override decimal ImperialValue()
        {
            decimal imperialMostRecentMeasure;

            if (unitsToUse == Units.Imperial)
            {
                imperialMostRecentMeasure = Convert.ToDecimal(mostRecentMeasure);
            }
            else
            {
                decimal decimalMetricValue = Convert.ToDecimal(mostRecentMeasure);
                decimal conversionFactor = 2.2046M;
                imperialMostRecentMeasure = decimalMetricValue * conversionFactor;
            }

            return imperialMostRecentMeasure;
        }
    }
}
