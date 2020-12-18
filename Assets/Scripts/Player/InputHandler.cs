using System.Collections;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance = null;
    public Vector2 axisInput;
    public Pointer<bool> boost;
    public bool jump;
    public bool interact;
    public bool drop;

    private void Awake()
    {
        axisInput = new Vector2(0, 0);
        boost = new Pointer<bool>(false);
        jump = false;
        interact = false;
        drop = false;

        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

    }

    private void Update()
    { 
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        axisInput = new Vector2(hMove, vMove);
        axisInput.Normalize();

        boost.Value = Input.GetButtonDown("Boost") && !boost.CoolingDown;
        if (boost.Value) StartCoroutine(CoolDown.CoolDownEnum(2f, boost, null));

        jump = Input.GetButton("Jump");

        interact = Input.GetButtonDown("Interact");
        drop = Input.GetButtonDown("StopInteract");
    }

}