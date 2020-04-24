using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptPlayer.Shared
{
    public class Osr : Device
    {
        private bool _initialized;
        private OsrSerial _controller;

        public Osr(OsrSerial controller)
        {
            _controller = controller;
            Name = "OSR";
        }

        public async Task<bool> SetPosition(byte position, byte speed)
        {
            try
            {
                _controller.SendPulse(position, speed);
                
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                OnDisconnected(e);
                return false;
            }
        }


        public async Task<bool> Initialize()
        {
            if (_initialized) return true;

            /*IBuffer buffer = GetBuffer(0);

            GattCommunicationStatus status = await _commandCharacteristics.WriteValueAsync(buffer);

            if (status != GattCommunicationStatus.Success)
                return false;

            _notifyCharacteristics.ValueChanged += NotifyCharacteristicsOnValueChanged;
            */
            _initialized = true;
            return true;
        }

        public override async Task Set(DeviceCommandInformation information)
        {
            await SetPosition(information.PositionToTransformed, information.SpeedTransformed);
        }

        public override async Task Set(IntermediateCommandInformation information)
        {
            return;
            // Does not apply
        }

        protected override void StopInternal()
        {
            // Not available
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
