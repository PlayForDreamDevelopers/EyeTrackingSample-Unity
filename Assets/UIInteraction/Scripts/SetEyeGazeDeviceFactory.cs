using UnityEngine;
using YVR.Interaction.Runtime;

namespace YVR.Samples.EyeTracking.UIInteraction
{
    public class SetEyeGazeDeviceFactory : MonoBehaviour
    {
        public EyeGazeInput eyeGazeInput;

        private void Start()
        {
            InputModalityManager.instance.SetInputDeviceMode(new EyeGazeDeviceFactory());
            eyeGazeInput.SwitchHandInputType(EyeGazeInput.HandType.Both);
        }
    }
}