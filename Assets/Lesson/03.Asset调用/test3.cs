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
        Debug.Log("跳");
    }


    void Update()
    {
        // 读取每帧 的值
        Vector2 moveAmount = moveAction.ReadValue<Vector2>();
        Debug.Log(moveAmount);
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
