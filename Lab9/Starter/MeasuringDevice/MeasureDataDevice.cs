using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;
using System.IO;

namespace MeasuringDevice
{
    // TODO: Modify this class to implement the IDisposable interface.
    // TODO: Modify this class to implement the ILoggingMeasuringDevice interface instead of the IMeasuingDevice interface.
    public abstract class MeasureDataDevice : IMeasuringDevice
    {
        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        public abstract decimal MetricValue();

        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to imperial units.</returns>
        public abstract decimal ImperialValue();

        /// <summary>
        /// Starts the measuring device.
        /// </summary>
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);

            // TODO: Add code to open a logging file and write an initial entry.
            GetMeasurements();
        }

        /// <summary>
        /// Stops the measuring device.
        /// </summary>
        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            // TODO: Add code to write a message to the log file.
        }

        /// <summary>
        /// Enables access to the raw data from the device in whatever units are native to the device.
        /// </summary>
        /// <returns>The raw data from the device in native format.</returns>
        public int[] GetRawData()
        {
            return dataCaptured;
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();

                while (true)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller.TakeMeasurement();
                    mostRecentMeasure = dataCaptured[x];

                    // TODO: Add code to log each time a measurement is taken.

                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
        }

        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController controller;
        protected DeviceType measurementType;

        // TODO: Add fields necessary to support logging.

        // TODO: Add methods to implement the ILoggingMeasuringDevice interface.
    }
}
