using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DeviceControl;

namespace Contoso.MeasuringDevices
{
    class MassMeasuringDevice : IControllableDevice
    {
        Random random;

        public MassMeasuringDevice()
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
            return random.Next(1390);
        }
    }
}
