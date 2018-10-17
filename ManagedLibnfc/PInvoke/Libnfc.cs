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
    }
}
