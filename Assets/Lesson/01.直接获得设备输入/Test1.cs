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