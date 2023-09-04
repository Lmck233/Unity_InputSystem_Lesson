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
