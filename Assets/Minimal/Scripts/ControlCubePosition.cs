using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace PFDM.Samples.EyeTracking.Minimal
{
    public class ControlCubePosition : MonoBehaviour
    {
        private InputDevice m_EyeDevice;
        public GameObject target;

        private void Start()
        {
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.EyeTracking, devices);
            if (devices.Count > 0) m_EyeDevice = devices[0];

            InputDevices.deviceConnected += OnDeviceConnected;
        }

        private void OnDeviceConnected(InputDevice inputDevice)
        {
            if ((inputDevice.characteristics & InputDeviceCharacteristics.EyeTracking) == 0) return;
            m_EyeDevice = inputDevice;
        }

        private void Update()
        {
            if (!m_EyeDevice.isValid) return;
            m_EyeDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            m_EyeDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

            Debug.Log($"sss {{m_EyeDevice.name}}, and position is {position}, rotation is {rotation}");

            target.transform.position = position + rotation * Vector3.forward;
        }

        private void OnDestroy() { InputDevices.deviceConnected -= OnDeviceConnected; }
    }
}