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
