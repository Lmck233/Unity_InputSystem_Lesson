using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class test5 : MonoBehaviour
{
    void OnMove(InputValue value)
    {
        Debug.Log("move"+value.Get<Vector2>());
    }

    void OnJunp(InputValue value)
    {
        Debug.Log("Jump"+value.isPressed);
    }
    
}
