using System;
using System.Collections.Generic;
using UnityEngine;

public class InputActive : MonoBehaviour
{
    public static InputActive Instance { get; private set; }

    public GameController Input { get; set; }

    public Action Dash_Handler { get; set; }
    public Action<Vector3> Movement_Handler { get; set; }

    private void Start()
    {
        Instance = this;
        Input = new();
        Input.Enable();

        Input.GamePlay.Dash.performed += (e) =>
        {
            Dash_Handler();
        };
    }

    private void Update()
    {
        var axisX = Input.GamePlay.Movement.ReadValue<Vector2>().x;
        var axisZ = Input.GamePlay.Movement.ReadValue<Vector2>().y;
        Movement_Handler(new Vector3(axisX, 0f, axisZ));

        /*if (Input.GamePlay.Dash.ReadValue<float>() >0f)
        {
            Debug.Log("Turu");
        }*/
        
    }
}
