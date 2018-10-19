using ManagedLibnfc.PInvoke;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagedLibnfc
{
    public class NfcDevice : IDisposable
    {
        private IntPtr devicePointer;
        private bool disposed = false;

        string _name;

        public string Name
        {
            get
            {
                if (_name == null)
                    _name = Libnfc.DeviceGetName(devicePointer);
                return _name;
            }
        }

        public NfcDevice(IntPtr devicePointer)
        {
            this.devicePointer = devicePointer;
        }

        public void InitiatorInit()
        {
            var result = Libnfc.InitiatorInit(devicePointer);
            if (result < 0)
            {
                Perror("nfc_initiator_init");
                throw new Exception("nfc_initiator_init");
            }
        }

        public void DeviceSetPropertyBool(NfcProperty property, bool enable)
        {
            var result = Libnfc.DeviceSetPropertyBool(devicePointer, property, enable);
            if (result < 0)
            {
                Perror("nfc_device_set_property_bool");
                throw new Exception("nfc_device_set_property_bool");
            }
        }

        public int InitiatorTransceiveBitsTimed(byte[] pbtTx, uint szTxBits, byte[] pbtTxPar, byte[] pbtRx, uint szRx, byte[] pbtRxPar, ref uint cycles) =>
            Libnfc.InitiatorTransceiveBitsTimed(devicePointer, pbtTx, szTxBits, pbtTxPar, pbtRx, szRx, pbtRxPar, ref cycles);

        public int InitiatorTransceiveBits(byte[] pbtTx, uint szTxBits, byte[] pbtTxPar, byte[] pbtRx, uint szRx, byte[] pbtRxPar) =>
            Libnfc.InitiatorTransceiveBits(devicePointer, pbtTx, szTxBits, pbtTxPar, pbtRx, szRx, pbtRxPar);

        public int InitiatorTransceiveBytesTimed(byte[] pbtTx, uint szTx, byte[] pbtRx, uint szRx, ref uint cycles) =>
            Libnfc.InitiatorTransceiveBytesTimed(devicePointer, pbtTx, szTx, pbtRx, szRx, ref cycles);

        public int InitiatorTransceiveBytes(byte[] pbtTx, uint szTx, byte[] pbtRx, uint szRx, int timeout) =>
            Libnfc.InitiatorTransceiveBytes(devicePointer, pbtTx, szTx, pbtRx, szRx, timeout);

        public void Perror(string s) =>
            Libnfc.Perror(devicePointer, s);

        public void Close() => Dispose();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    // component.Dispose();
                }
                Libnfc.Close(devicePointer);
                devicePointer = IntPtr.Zero;
                disposed = true;
            }
        }

        ~NfcDevice()
        {
            Dispose(false);
        }
    }
}
