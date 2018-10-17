using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ManagedLibnfc.PInvoke
{
    public static class Libnfc
    {
        /// <summary>
        /// Initialize libnfc.
        /// This function must be called before calling any other libnfc function
        /// </summary>
        /// <param name="context">Output location for NfcContext</param>
        [DllImport("libnfc", EntryPoint = "nfc_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NfcInit(out IntPtr context);

        /// <summary>
        /// Deinitialize libnfc.
        /// Should be called after closing all open devices and before your application terminates.
        /// </summary>
        /// <param name="context">The context to deinitialize</param>
        [DllImport("libnfc", EntryPoint = "nfc_exit", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NfcExit(IntPtr context);

        /// <summary>
        /// Open a NFC device.
        /// If <paramref name="connstring"/> is null, the first available device from NfcListDevices function is used.
        /// 
        /// If <paramref name="connstring"/> is set, this function will try to claim the right device using information provided by <paramref name="connstring"/>.
        /// 
        /// When it has successfully claimed a NFC device, memory is allocated to save the device information.
        /// It will return a pointer to a NfcDevice struct.
        /// This pointer should be supplied by every next functions of libnfc that should perform an action with this device.
        /// </summary>
        /// <remarks>
        /// Depending on the desired operation mode, the device needs to be configured by using NfcInitiatorInit() or NfcTargetInit(),
        /// optionally followed by manual tuning of the parameters if the default parameters are not suiting your goals.
        /// </remarks>
        /// <param name="context">The context to operate on</param>
        /// <param name="connstring">The device connection string if specific device is wanted, null otherwise</param>
        /// <returns>Returns pointer to a NfcDevice struct if successfull; otherwise returns null value.</returns>
        [DllImport("libnfc", EntryPoint = "nfc_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr NfcOpen(IntPtr context, string connstring);

        /// <summary>
        /// Close from a NFC device.
        /// Initiator's selected tag is closed and the device, including allocated NfcDevice struct, is released.
        /// </summary>
        /// <param name="pnd">NfcDevice struct pointer that represent currently used device</param>
        [DllImport("libnfc", EntryPoint = "nfc_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void NfcClose(IntPtr pnd);
    }
}
