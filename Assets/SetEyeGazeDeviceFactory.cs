using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YVR.Interaction.Runtime;
using YVR.Interaction.Runtime.InputDeviceModeFactory;

public class SetEyeGazeDeviceFactory : MonoBehaviour
{
    public EyeGazeInput eyeGazeInput;

    // Start is called before the first frame update
    void Start()
    {
        InputModalityManager.instance.SetInputDeviceMode(new EyeGazeDeviceFactory());
        eyeGazeInput.SwitchHandInputType(EyeGazeInput.HandType.Both);
    }
}
