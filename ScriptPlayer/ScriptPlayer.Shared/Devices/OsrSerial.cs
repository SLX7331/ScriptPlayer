using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading;

namespace ScriptPlayer.Shared
{
    public class OsrSerial : DeviceController
    {
        private string comPort = "None";
        private string baudRate = "115200";
        protected string serialReport;

        private readonly object _discoverylocker = new object();
        private bool _discover;

        // Serial Stuff
        private SerialPort serial;
        public SerialPort Serial { get => serial; set => serial = value; }
        public string ComPort { get => comPort; set => comPort = value; }
        public string BaudRate { get => baudRate; set => baudRate = value; }

        public override void ScanForDevices()
        {
            Start();
        }
        
        public OsrSerial()
        {
            // Create the serial port with basic settings
            

    }

    public void Start()
        {
            lock (_discoverylocker)
            {
                if (_discover)
                    return;
            
                StartSerial();

                if (_discover)
                {
                    Osr osr = new Osr(this);
                    OnDeviceFound(osr);
                }
            }
        }
        
        public void Stop()
        {
            lock (_discoverylocker)
            {
                if (!_discover)
                    return;

                StopSerial();
                
                _discover = false;
            }
        }

        public static bool IsOsrPaired()
        {
            //TODO
            return false;
        }

        #region T-Code

        // Function to start serial
        private void StartSerial()
        {
            if (ComPort != "None")
            {
                // Add extra characters for COM ports > 9
                if (ComPort.Substring(0, 3) == "COM")
                {
                    if (ComPort.Length != 4)
                    {
                        ComPort = "\\\\.\\" + ComPort;
                    }
                }

                // Open the serial connection
                try
                {
                    serial = new SerialPort(ComPort, Convert.ToInt32(BaudRate), Parity.None, 8, StopBits.One);
                    serial.Open();
                    serial.ReadTimeout = 10;
                    _discover = true;
                }
                catch (Exception exc) {
                    _discover = false;
                }

            }
        }

        // Function to stop serial
        private void StopSerial()
        {
            if (serial != null && serial.IsOpen) serial.Close();
        }


        public void SendPulse(byte position, byte speed)
        {
            string pulsestring = String.Format("L0{0}S{1}", position, speed*100);
            Console.WriteLine(" T-code:\n " + pulsestring);
            serial.WriteLine(pulsestring);
        }

        #endregion
    }
}
