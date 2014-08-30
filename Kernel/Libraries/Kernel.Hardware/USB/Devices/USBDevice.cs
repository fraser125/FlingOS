﻿#region Copyright Notice
// ------------------------------------------------------------------------------ //
//                                                                                //
//               All contents copyright � Edward Nutting 2014                     //
//                                                                                //
//        You may not share, reuse, redistribute or otherwise use the             //
//        contents this file outside of the Fling OS project without              //
//        the express permission of Edward Nutting or other copyright             //
//        holder. Any changes (including but not limited to additions,            //
//        edits or subtractions) made to or from this document are not            //
//        your copyright. They are the copyright of the main copyright            //
//        holder for all Fling OS files. At the time of writing, this             //
//        owner was Edward Nutting. To be clear, owner(s) do not include          //
//        developers, contributors or other project members.                      //
//                                                                                //
// ------------------------------------------------------------------------------ //
#endregion
    
using System;
using Kernel.FOS_System.Collections;

namespace Kernel.Hardware.USB.Devices
{
    /// <summary>
    /// Represents a USB device.
    /// </summary>
    public class USBDevice : Device
    {
        /// <summary>
        /// The device info that specifies the physical device to communicate with.
        /// </summary>
        protected USBDeviceInfo DeviceInfo;

        /// <summary>
        /// Initialises a new USB device with specified USB device info. Includes adding it to Devices lists in
        /// device manager and USB manager.
        /// </summary>
        /// <param name="aDeviceInfo">The device info of the physical device.</param>
        public USBDevice(USBDeviceInfo aDeviceInfo)
        {
            DeviceInfo = aDeviceInfo;

            DeviceManager.Devices.Add(this);
            USBManager.Devices.Add(this);
        }

        /// <summary>
        /// Destroys the USB device including removing it from Devices lists in device manager and USB manager.
        /// </summary>
        public virtual void Destroy()
        {
            DeviceManager.Devices.Remove(this);
            USBManager.Devices.Remove(this);
        }
    }
    /// <summary>
    /// Stores information which describes a particular, physical USB device.
    /// </summary>
    public class USBDeviceInfo : FOS_System.Object
    {
        /// <summary>
        /// The port number of the device.
        /// </summary>
        public byte portNum;
        /// <summary>
        /// The device address.
        /// </summary>
        public byte address;
        /// <summary>
        /// The host controller to which the device is attached.
        /// </summary>
        public HCIs.HCI hc;
        /// <summary>
        /// The list of "Endpoint"s that has been detected on the USB device.
        /// </summary>
        public List Endpoints;

        /// <summary>
        /// The USb specification number reported by the device.
        /// </summary>
        public ushort usbSpec;
        /// <summary>
        /// The USb class code reported by the device.
        /// </summary>
        public byte usbClass;
        /// <summary>
        /// The USB sub-class code reported by the device.
        /// </summary>
        public byte usbSubclass;
        /// <summary>
        /// The USB protocol version number reported by the device.
        /// </summary>
        public byte usbProtocol;
        /// <summary>
        /// The device Vendor ID.
        /// </summary>
        public ushort vendor;
        /// <summary>
        /// The device product ID.
        /// </summary>
        public ushort product;
        /// <summary>
        /// The device release number.
        /// </summary>
        public ushort releaseNumber;
        /// <summary>
        /// The manufacturer's string Id.
        /// </summary>
        public byte manufacturerStringID;
        /// <summary>
        /// The product's string Id.
        /// </summary>
        public byte productStringID;
        /// <summary>
        /// The serial number string Id.
        /// </summary>
        public byte serNumberStringID;
        /// <summary>
        /// The number of configurations reported by the device.
        /// </summary>
        public byte numConfigurations;

        /// <summary>
        /// The interface class code reported by the device.
        /// </summary>
        public byte InterfaceClass;
        /// <summary>
        /// The interface sub-class code reported by the device.
        /// </summary>
        public byte InterfaceSubclass;

        /// <summary>
        /// The decoded serial number string from the device.
        /// </summary>
        public FOS_System.String SerialNumber;

        /// <summary>
        /// The mass storage bulk-transfer interface number, if any and only if the device is an MSD.
        /// </summary>
        public byte MSD_InterfaceNum;
        /// <summary>
        /// The mass storage bulk-transfer IN-direction enpdoint Id, if any and only if the device is an MSD.
        /// </summary>
        public byte MSD_INEndpointID;
        /// <summary>
        /// The mass storage bulk-transfer OUT-direction enpdoint Id, if any and only if the device is an MSD.
        /// </summary>
        public byte MSD_OUTEndpointID;

        /// <summary>
        /// Initialises a new USB Device Info but does not attempt to get/fill out the information.
        /// That job is left to the USB manager.
        /// </summary>
        /// <param name="aPortNum">The port number of the physical device.</param>
        /// <param name="aHC">The host controller which the device is connected to.</param>
        public USBDeviceInfo(byte aPortNum, HCIs.HCI aHC)
        {
            portNum = aPortNum;
            hc = aHC;
        }

        /// <summary>
        /// Frees the port and destroys the USB device instance.
        /// </summary>
        public void FreePort()
        {
            HCIs.HCPort port = hc.GetPort(portNum);
            if (port.device != null)
            {
                port.device.Destroy();
                port.device = null;
            }
            port.deviceInfo = null;
            port.connected = false;
            port.speed = HCIs.USBPortSpeed.UNSET;
        }
    }
}