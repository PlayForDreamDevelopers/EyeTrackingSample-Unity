using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Core;

namespace YVR.Interaction.Runtime.InputDeviceModeFactory
{
    public class EyeGazeDeviceFactory : BaseInputDeviceFactory
    {
        private ActiveInputDevice m_Device;

        public override InputMode UpdateDevice()
        {
            YVRPlugin.Instance.GetCurrentInputDevice(ref m_Device);
            currentMode = m_Device switch
            {
                ActiveInputDevice.None => InputMode.HMD,
                ActiveInputDevice.ControllerActive => InputMode.Controller,
                ActiveInputDevice.HandTrackingActive => InputMode.EyeGaze,
                _ => InputMode.HMD
            };

            return currentMode;
        }
    }
}