using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test4 : MonoBehaviour
{
    private AAA Actions;


    private void Awake()
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
