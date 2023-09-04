# Unity_InputSystem_Lesson
InputSystem 学习demo
<a name="Rz8jZ"></a>
# 安装
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693752301705-91c32f52-91e9-420d-96fe-3eab869ec90b.png#averageHue=%234b4a4a&clientId=u661d5e24-6121-4&from=paste&height=556&id=u4b69269f&originHeight=695&originWidth=1180&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=81779&status=done&style=none&taskId=ua25d5fdf-e65c-4383-9242-5e80250574b&title=&width=944)

概念<br />![](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693761517405-6d404290-adfa-44a8-9b28-9a3317c9bd0a.png#averageHue=%23ece7e2&clientId=u661d5e24-6121-4&from=paste&id=ua3b2d980&originHeight=505&originWidth=899&originalType=url&ratio=1.25&rotation=0&showTitle=false&status=done&style=none&taskId=ud18fa80f-3be7-47e0-b0c7-7cbb966e8cb&title=)


<a name="rWH2r"></a>
# 一、创建 Input Actions
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693752450364-888b75ee-63bc-4fbf-858d-369a6a2150b1.png#averageHue=%23d7d6d5&clientId=u661d5e24-6121-4&from=paste&height=616&id=u92ea0f85&originHeight=948&originWidth=606&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=70246&status=done&style=none&taskId=u585a0b5d-96c7-479d-b757-38ad909af3d&title=&width=393.8000183105469)
<a name="WYuTZ"></a>
## Input Actions概念及结构关系
在我们InputSystem中所用到结构关系为<br />InputSystem=>Input Actions=>Action Maps=>Actions

<a name="AfOih"></a>
### ActionMaps （表）
简单来说我们可以将InputActions视为我们项目里其中一个控制器的输入操作管理集，而ActionMaps则为该控制器其中的一个输入映射集。<br />![9045e468c0c4075b27e3e31c2177ebef_f1c9f0648cd14deaa9467224e684cace.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688849540381-0586c567-6c46-41c3-8314-f9a0675acb42.png#averageHue=%233a3837&clientId=u3e546a71-161f-4&from=paste&height=716&id=uf7cd49fd&originHeight=716&originWidth=1521&originalType=binary&ratio=1&rotation=0&showTitle=false&size=88719&status=done&style=none&taskId=uf7e19548-51a7-4e4a-8e07-e1de88930c8&title=&width=1521)


![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688849948757-332fbabe-ec20-4e47-a5cc-9189b4aa18e8.png#averageHue=%23383838&clientId=u3e546a71-161f-4&from=paste&height=575&id=u957d5ff9&originHeight=575&originWidth=956&originalType=binary&ratio=1&rotation=0&showTitle=false&size=44884&status=done&style=none&taskId=u933d189d-6b27-4ae2-a456-f4511331706&title=&width=956)

<a name="q72J2"></a>
### Actions （动作）
而Actions则为ActionMaps里其中一个动作输入映射

<a name="LJxDC"></a>
### ActionProperties （值类型）
在Actions中也有许多参数，其中ActionType则是我们最常用到。其概念为我们该动作输入映射的类型，有以下三种类型

1.Button 默认设置，包括按钮或者按键触发的一次性动作<br />2.Value 提供一种连续状态变化事件，如果设置了多个输入，就会切换到最主要的一个。用它进行模拟控制，比如移动。<br />3.Pass Through 和Value很相似，但它不会像Value一样（如果有多个输入时，不会只选择最主要的那个，而把其他的输入忽略）

在使用Value或者Pass Through Types时，你会看到一个额外的选项 Control Type为该Value的返回值类型



<a name="ipdGL"></a>
# 二、获取输入
<a name="sXE7K"></a>
## 1.[直接读取装置状态](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Workflow-Direct.html)
:::success
这是最简单、最直接的工作流程，但最不灵活。如果您想使用一种类型的设备快速实现，这将非常有用。如果要为用户提供多种类型的输入，或者要面向多个平台，则它可能不是最佳选择。<br />可以通过引用设备的[控件](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/Controls.html)并读取它们当前生成的值，使用如下代码直接从连接的设备读取值：
:::
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test1 : MonoBehaviour
{
    Gamepad gamepad; //手柄
    Keyboard keyboard; //键盘
    Mouse mouse; //鼠标
    Pointer pointer; //指针

    void Start()
    {
        gamepad = Gamepad.current; //手柄
        keyboard = Keyboard.current; //键盘
        mouse = Mouse.current; //鼠标
        pointer = Pointer.current; //指针
    }

    // Update is called once per frame
    void Update()
    {
        if (gamepad != null)
        {
            Debug.Log(gamepad.leftStick.ReadValue());//手柄遥感的偏移
            if (gamepad.bButton.wasPressedThisFrame)
                Debug.Log("按下B键");
        }
        
        if (keyboard != null)
        {
            //执行顺序 isPressed = false ->
            //按下：wasPressedThisFrame = true ->
            //中途：isPressed = true ->
            //松开：wasReleasedThisFrame = true ->
            //结束 isPressed = false
            if (keyboard.wKey.wasPressedThisFrame)
                Debug.Log("w键按下（一直按住w键的话，也只执行一次）");
            if (keyboard.wKey.wasReleasedThisFrame)
                Debug.Log("w键松开");
            Debug.Log("是否按住w键：" + keyboard.wKey.isPressed);
        }
        
        
        if (mouse != null)
        {
            Debug.Log(mouse.scroll.ReadValue());//滚轮的滚动值，向前滚Y的值为正，向后滚为负
 
            if (mouse.leftButton.wasPressedThisFrame)
                Debug.Log("按鼠标左键");
            if (mouse.rightButton.wasPressedThisFrame)
                Debug.Log("按鼠标右键");
            if (mouse.middleButton.wasPressedThisFrame)
                Debug.Log("按滚轮键");
        }
        
        
        if (pointer != null)
        {
            Debug.Log("位移量 "+pointer.delta.ReadValue());//与上一帧的偏移
            Debug.Log("在空间中的坐标 "+pointer.position.ReadValue());//在空间中的坐标
        }
    }
}
```

![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688887753881-8a9a3097-db88-424a-a46c-71f466c12a00.png#averageHue=%23272726&clientId=u3e546a71-161f-4&from=paste&height=226&id=u808dd440&originHeight=226&originWidth=549&originalType=binary&ratio=1&rotation=0&showTitle=false&size=18819&status=done&style=none&taskId=u644eea9c-f740-416c-b80e-a686183399b&title=&width=549)<br />![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688888597849-e6efb96a-0663-484d-b95e-7a7b07277d3b.png#averageHue=%233b3a3a&clientId=u3e546a71-161f-4&from=paste&height=401&id=u79d6758d&originHeight=401&originWidth=759&originalType=binary&ratio=1&rotation=0&showTitle=false&size=33837&status=done&style=none&taskId=u2af53da7-d610-4db2-9b5f-437cc878eab&title=&width=759)

<a name="qZrdq"></a>
## 2. 使用嵌入式操作<br />![10dd1f72e1ef97f4f5a6c99eb30e12d6_Workflow-Embedded.svg](https://cdn.nlark.com/yuque/0/2023/svg/26209514/1688889405767-39f13de5-a0aa-42ef-9d10-6306f01075a9.svg#clientId=u3e546a71-161f-4&from=paste&height=362&id=ue0ebfb47&originHeight=362&originWidth=764&originalType=binary&ratio=1&rotation=0&showTitle=false&size=18764&status=done&style=none&taskId=u43e8cf9c-a29f-4011-9dee-453ff89326a&title=&width=764)

![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693765115392-d17c8a13-e311-4744-9307-0aa4acb12fa0.png#averageHue=%23232b29&clientId=u661d5e24-6121-4&from=paste&height=231&id=uc85629c5&originHeight=289&originWidth=698&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=48583&status=done&style=none&taskId=uef3938fb-72bf-4f10-9df0-e9b5cfbfc92&title=&width=558.4)


![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693768545467-3ed5d88c-cec4-428a-9131-d9f2af51c6e2.png#averageHue=%233b3a39&clientId=u661d5e24-6121-4&from=paste&height=407&id=u6eb39620&originHeight=509&originWidth=1758&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=71818&status=done&style=none&taskId=u07079880-0340-43a0-a9e1-206e1418445&title=&width=1406.4)

```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test2 : MonoBehaviour
{
    public InputAction MoveAction;

    public InputAction JunpAction;


    private void Start()
    {
        JunpAction.performed += Junp;
    }

    private void Junp(InputAction.CallbackContext obj)
    {
        Debug.Log("跳了");
    }

    void Update()
    {
        // 读取每帧 的值
        Vector2 moveAmount = MoveAction.ReadValue<Vector2>();
        Debug.Log(moveAmount);
    }
    
    //操作必须启用 禁用
    public void OnEnable()
    {
        MoveAction.Enable();
        JunpAction.Enable();
    }

    public void OnDisable()
    {
        MoveAction.Disable();
        JunpAction.Disable();
    }
}

```
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688894678790-365c9113-808a-4568-b2e0-3bb3c3880fac.png#averageHue=%233e3e3e&clientId=u3e546a71-161f-4&from=paste&height=161&id=uebd8bd62&originHeight=161&originWidth=361&originalType=binary&ratio=1&rotation=0&showTitle=false&size=12922&status=done&style=none&taskId=u163c4f84-9b8f-43f0-a7d2-51d636499ff&title=&width=361)

<a name="szQAC"></a>
## 3.资产调用
<a name="TchIi"></a>
### 3.1 在检查器中引用动作资源
要通过**检查器参考**使用动作资源：

1. 在脚本中创建一个字段。public InputActionsAsset
2. 在检查器中[指定引用](https://docs.unity3d.com/Manual/EditingValueProperties.html#ref-assign-dnd)。
3. 使用字符串按名称访问脚本中的操作。

![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693767275190-41b687b0-f5ca-40aa-8c80-a7d93ceb8e93.png#averageHue=%23383838&clientId=u661d5e24-6121-4&from=paste&height=563&id=ufceb45bd&originHeight=704&originWidth=1180&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=42250&status=done&style=none&taskId=u73bc9f34-676b-454e-91c8-2fbf586ef61&title=&width=944)

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test3 : MonoBehaviour
{
    //表
    public InputActionAsset actions;
    //动作
    private InputAction moveAction;
    void Start()
    {
        // 获得move映射
        moveAction = actions.FindActionMap("PC").FindAction("Move");

        // 订阅跳方法
        actions.FindActionMap("PC").FindAction("Junp").performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        //跳
    }


    void Update()
    {
        //得到输入值
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
    }
    
    void OnEnable()
    {
        actions.FindActionMap("PC").Enable();
    }
    void OnDisable()
    {
        actions.FindActionMap("PC").Disable();
    }
}

```

<a name="wbwqk"></a>
### 3.2 通过 C# 包装器引用操作资产
若要通过 **C# 包装器**使用操作资源，请执行以下操作：

1. 在项目窗口中选择您的动作资源
2. 在检查器中，启用**“生成 C# 类**”，然后选择**“应用**”。应在项目窗口中看到与操作资源同名的 C# 资源。
3. 在脚本中创建操作 C# 类的实例。
4. 使用操作 C# 类的 API 访问脚本中的操作。


![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693768675749-0ea05e3a-aef1-48da-a148-1ada3a0c8262.png#averageHue=%2369625d&clientId=u661d5e24-6121-4&from=paste&height=636&id=u468f442d&originHeight=795&originWidth=1452&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=136712&status=done&style=none&taskId=ubd4d5e79-709e-41c7-a4d1-be21824cab1&title=&width=1161.6)
```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test4 : MonoBehaviour
{
    private AAA Actions;


    private void Start()
    {
        //创建实例
        Actions = new AAA();
        //订阅跳方法
        Actions.PC.Junp.performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        Debug.Log("跳");
    }
    
    void Update()
    {
        // 读取每帧 的值
        Vector2 moveAmount = Actions.PC.Move.ReadValue<Vector2>();
        Debug.Log(moveAmount);
    }
    
    void OnEnable()
    {
        Actions.PC.Enable();
    }
    void OnDisable()
    {
        Actions.PC.Disable();
    }
    
}

```



<a name="eFVLw"></a>
## 4.播放器输入组件（Player Input）
<a name="Ppvlk"></a>
### 4.1 Send Messager(通知)
:::info
选择这个模式，这个物体的脚本添加生命周期方法。会自动调用这个方法
:::
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884203966-40bc3814-771b-4e2b-8bbc-a9ff745a38cc.png#averageHue=%23454141&clientId=u3e546a71-161f-4&from=paste&height=339&id=u2f804ff6&originHeight=339&originWidth=439&originalType=binary&ratio=1&rotation=0&showTitle=false&size=43391&status=done&style=none&taskId=u8d9a337d-b4fe-45d5-abf0-c77f28c2050&title=&width=439)<br />![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884220325-c3d620fc-a429-488f-9dba-f1eb7279bdb7.png#averageHue=%23272726&clientId=u3e546a71-161f-4&from=paste&height=417&id=ue929c4cc&originHeight=417&originWidth=482&originalType=binary&ratio=1&rotation=0&showTitle=false&size=32961&status=done&style=none&taskId=uba167537-aa01-4956-8b2e-7d997b10b07&title=&width=482)![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884236523-326f31b8-765a-4305-ba14-856a4ba251d4.png#averageHue=%23414040&clientId=u3e546a71-161f-4&from=paste&height=170&id=uc92d0c2c&originHeight=170&originWidth=393&originalType=binary&ratio=1&rotation=0&showTitle=false&size=21962&status=done&style=none&taskId=u85564920-8a9d-42b2-90eb-d05125206ba&title=&width=393)

<a name="CfpfU"></a>
### 4.2 Broadcast Messages （广播）
:::success
一样使用周期方法调用<br />子物体挂载也能触发（通知肢体发生动作？
:::
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884562255-935960ae-21a9-4e37-8c22-764fbc9234bd.png#averageHue=%23676766&clientId=u3e546a71-161f-4&from=paste&height=260&id=ua89171b5&originHeight=260&originWidth=385&originalType=binary&ratio=1&rotation=0&showTitle=false&size=34760&status=done&style=none&taskId=ub7fc447a-e4be-48c4-a18f-bc6231c3b2c&title=&width=385)<br />![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884721178-c0563edd-6920-4e5d-8f11-a327e5dd0ea7.png#averageHue=%23312e2e&clientId=u3e546a71-161f-4&from=paste&height=112&id=u86f58ddf&originHeight=112&originWidth=255&originalType=binary&ratio=1&rotation=0&showTitle=false&size=4080&status=done&style=none&taskId=ud7342414-257d-4365-80ef-ec467ac0706&title=&width=255)![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884778621-d8408809-35be-446f-9d24-5e206443d6ef.png#averageHue=%23414141&clientId=u3e546a71-161f-4&from=paste&height=614&id=ub230419a&originHeight=614&originWidth=404&originalType=binary&ratio=1&rotation=0&showTitle=false&size=61791&status=done&style=none&taskId=ua6d8c2c1-46c5-43fd-acd1-b0171288b28&title=&width=404)

<a name="J2kaG"></a>
### 4.3 Invoke Unity Events （事件）
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688884851888-78238412-acb2-47d8-a3f0-44187ceacdab.png#averageHue=%233f3f3f&clientId=u3e546a71-161f-4&from=paste&height=718&id=u5c2bf4a4&originHeight=718&originWidth=395&originalType=binary&ratio=1&rotation=0&showTitle=false&size=47120&status=done&style=none&taskId=u3f86acc2-7541-4681-ac54-c4e1dec7f49&title=&width=395)
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test6 : MonoBehaviour
{
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump:");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move:"+context.ReadValue<Vector2>());
    }
}

```
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693821423666-fe7534e3-a95a-4d3e-8e3e-743e98d75f46.png#averageHue=%23363635&clientId=uf69ee0bb-605b-4&from=paste&height=184&id=u6958a14c&originHeight=230&originWidth=586&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=29469&status=done&style=none&taskId=ud1721986-947f-48bb-8a0c-e61577f5645&title=&width=468.8)
:::success
会有三次调用<br />分别是 Started 开始 、和回调 performed 正在执行 、 canceled 结束
:::

<a name="sQRM6"></a>
#### 只执行一次
:::info
添加参数
:::
![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1693821524593-10b9bbf4-46a2-4aee-b48e-7fa5200e263b.png#averageHue=%238cae96&clientId=uf69ee0bb-605b-4&from=paste&height=148&id=u0034b7a3&originHeight=185&originWidth=686&originalType=binary&ratio=1.25&rotation=0&showTitle=false&size=30233&status=done&style=none&taskId=u6fadd3f0-98b9-43e5-8206-9381c44a09f&title=&width=548.8)<br />![image.png](https://cdn.nlark.com/yuque/0/2023/png/26209514/1688886062625-c6eb5d87-b437-4f38-a31f-51dc218d687f.png#averageHue=%23272626&clientId=u3e546a71-161f-4&from=paste&height=459&id=u846b5e17&originHeight=459&originWidth=576&originalType=binary&ratio=1&rotation=0&showTitle=false&size=46330&status=done&style=none&taskId=u7f791b54-1d68-4daf-aa00-42aead4af65&title=&width=576)
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test6 : MonoBehaviour
{
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Jump:"+context.ReadValueAsButton());
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.phase== InputActionPhase.Performed)
        {
            Debug.Log("Move:"+context.ReadValue<Vector2>());
        }
        
    }
}

```

