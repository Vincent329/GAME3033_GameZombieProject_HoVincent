using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private void OnEnter(InputValue value)
    {
        SceneLoadManager.Instance.LoadMenuScreen();
    }
}
