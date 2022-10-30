using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;
using System.IO;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : ILoggingMeasuringDevice, IDisposable
    {

        public abstract decimal MetricValue();

        public abstract decimal ImperialValue();

        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);

            if (loggingFileWriter == null)
            {
                if (!File.Exists(loggingFileName))
                {
                    loggingFileWriter = File.CreateText(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Created");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
                else
                {
                    loggingFileWriter = new StreamWriter(loggingFileName);
                    loggingFileWriter.WriteLine("Log file status checked - Opened");
                    loggingFileWriter.WriteLine("Collecting Started");
                }
            }
            else
            {
                loggingFileWriter.WriteLine("Log file status checked - Already open");
                loggingFileWriter.WriteLine("Collecting Started");
            }
            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }

            if (loggingFileWriter != null)
            {
                loggingFileWriter.WriteLine("Collecting Stopped");
            }
        }

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

                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];

                    if (loggingFileWriter != null)
                    {
                        loggingFileWriter.WriteLine("Measurement Taken: {0}", mostRecentMeasure.ToString());
                    }

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
        protected string loggingFileName;
        private TextWriter loggingFileWriter;

        public string GetLoggingFile()
        {
            return loggingFileName;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (loggingFileWriter != null)
                {
                    loggingFileWriter.WriteLine("Object Disposed");
                    loggingFileWriter.Flush();
                    loggingFileWriter.Close();
                    loggingFileWriter = null;
                }
            }
        }
    }
}
