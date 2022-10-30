using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;

namespace Fabrikam.Devices.MeasuringDevices
{
    class LengthMeasuringDevice : IControllableDevice
    {
        Random random;

        public LengthMeasuringDevice()
        {
            random = new Random();
        }

        public void StartDevice()
        {        
        }

        public void StopDevice()
        {
        }

        public int GetLatestMeasure()
        {
            return random.Next(1000);
        }
    }
}
