using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActive : MonoBehaviour
{
    public StateUnit State;
    public float Speed = 10f;
    public bool IsDash;
    public bool IsWalk;

    public enum StateUnit
    {
        Default,
        Idle, Run, Dash, Jump,
        Melee, Range, Death,
    }

    private CharacterController body;
    private Vector3 MoveDirect;

    private void Action(Vector3 vector)
    {
        MoveDirect = vector;
        if (vector == Vector3.zero)
        {
            Idle();
        }
        else
        {
            if (IsWalk)
            {
                Walk();
            }
            else
            {
                Run();
                if (IsDash)
                {
                    Dash();
                }
            }
           
            body.Move(MoveDirect * Speed * Time.deltaTime);
        }
    }     

    private void Idle()
    {
        State = StateUnit.Idle;
        Speed = 0f;
        //body.Move(InputActive.Instance.MoveDirect * Time.deltaTime * Speed);
    }

    private void Walk()
    {
        State = StateUnit.Idle;
        Speed = 5f / 2f;
    }

    private void Run()
    {
        State = StateUnit.Run;
        Speed = 5f;

        var rotate = Quaternion.LookRotation(MoveDirect);
        transform.rotation = Quaternion.Slerp(rotate, transform.rotation, Time.deltaTime);
        
    }

    private void Dash()
    {
        StartCoroutine(DashCouroutine());
    }

    private IEnumerator DashCouroutine()
    {
        State = StateUnit.Dash;
        Speed = 5f * 3f;
        yield return new WaitForSeconds(0.25f);
        IsDash = false;
    }

    private void Jump()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<CharacterController>();
        InputActive.Instance.Movement_Handler = Action;
        InputActive.Instance.Dash_Handler = () => IsDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
