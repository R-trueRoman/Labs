using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ContosoDevices = Contoso.MeasuringDevices;
using FabrikamDevices = Fabrikam.Devices.MeasuringDevices;

namespace DeviceControl
{
    public class DeviceController : IDisposable
    {
        private IControllableDevice device;

        public static DeviceController StartDevice(DeviceType MeasurementType)
        {
            DeviceController controller = new DeviceController();
            switch (MeasurementType)
            {
                case DeviceType.LENGTH:
                    controller.device = new FabrikamDevices.LengthMeasuringDevice();
                    break;
                case DeviceType.MASS:
                    controller.device = new ContosoDevices.MassMeasuringDevice();
                    break;
            }
            if (controller.device != null)
            {
                controller.device.StartDevice();
            }

            return controller;
        }

        public void StopDevice()
        {
            device.StopDevice();
        }

        public int TakeMeasurement()
        {
            return device.GetLatestMeasure();
        }

        public void Dispose()
        {
        }
    }

    public enum DeviceType
    {
        MASS, LENGTH
    }
}
