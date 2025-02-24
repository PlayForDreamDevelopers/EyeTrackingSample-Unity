# 眼动的使用方法

### 1.在Home的设置中启动眼动并校准眼动

设置的路径为**设置-交互控制-眼部控制-勾选眼动追踪和眼动滤波**，开启之后，请在**设置-交互控制-校准眼动与手势**或者**快速连按3次电源键**校准一次眼动

### 2.项目中引入YVR眼动相关的Packages
可以直接修改项目路径下**Packages/manifest.json**中的内容
分别添加YVR Core，YVR Utilities，YVR InteractionToolkit
"com.yvr.core": "git@github.com:PlayForDreamDevelopers/com.yvr.core-mirror.git?path=/com.yvr.core#406b9732a487db863068ebce8d1ca04d353a7c2c",
"com.yvr.interaction": "git@github.com:PlayForDreamDevelopers/com.yvr.interaction-mirror.git?path=/com.yvr.interaction#f8c28e4eb72d7cb3d8deb95deb0954728790c593",
"com.yvr.utilities": "git@github.com:PlayForDreamDevelopers/com.yvr.utilities-mirror.git?path=/com.yvr.utilities#19dc78e2ca77472a8d35f47142e9b2da86743bff"
> **Tips**:如果后续Packages的版本有更新，请自行修改地址中#后面的commidId

也可以通过Unity的**Window-Package Manager**左上角的“+”号，选择**Add package from git URL...**添加以下路径
git@github.com:PlayForDreamDevelopers/com.yvr.core-mirror.git?path=/com.yvr.core#406b9732a487db863068ebce8d1ca04d353a7c2c
git@github.com:PlayForDreamDevelopers/com.yvr.interaction-mirror.git?path=/com.yvr.interaction#f8c28e4eb72d7cb3d8deb95deb0954728790c593
git@github.com:PlayForDreamDevelopers/com.yvr.utilities-mirror.git?path=/com.yvr.utilities#19dc78e2ca77472a8d35f47142e9b2da86743bff

### 3.把XROrigin预制体拖入场景中
在Project中搜索**XROrigin**，搜索范围选择**All**，将找到的XROrigin拖入到场景中

### 4.设置支持眼动的InputDeviceMode
首先创建一个继承自**YVR.Interaction.Runtime.InputDeviceModeFactory.BaseInputDeviceFactory**的类
参考代码
```C#
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
```
调用**InputModalityManager.instance.SetInputDeviceMode**将Factory注册，并设置EyeGazeInput对应的HandInputType
参考代码
```C#
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
```

### 5.修改InputModule
讲EventSystem中的InputModule修改为**XRUIInputModule**

### 6.修改Canvas的Raycaster
找到需要交互的Canvas，将Canvas中的Raycaster修改为**TrackedDeviceGraphicRaycaster**

### 7.检查XROrigin上YVRManager的选项
确保EyeTrackingSupport不是**None**，由于眼动需要手势交互配合，确保HandTrackingSupport不是**Controllers Only**