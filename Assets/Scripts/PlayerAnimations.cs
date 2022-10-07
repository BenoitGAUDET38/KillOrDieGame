using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator myAnimator;
    private string currentState;

    private float xAxis;
    private float yAxis;
    private float xShoot;
    private float yShoot;

    private static string UP = "Player_Up";
    private static string DOWN = "Player_Down";
    private static string LEFT = "Player_Left";
    private static string RIGHT = "Player_Right";

    private static string WALK_UP = "Player_WalkUp";
    private static string WALK_DOWN = "Player_WalkDown";
    private static string WALK_LEFT = "Player_WalkLeft";
    private static string WALK_RIGHT = "Player_WalkRight";

    // Start is called before the first frame update
    void Awake()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        animate();
    }

    void getInputs()
	{
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        xShoot = Input.GetAxis("xShoot");
        yShoot = Input.GetAxis("yShoot");
    }

    void changeAnimationState(string state)
	{
        if (currentState == state)
            return;

        myAnimator.Play(state, 0, 0f);
        currentState = state;
	}

    void animate()
	{
        // animation with shooting
        if (xShoot > 0)
		{
            if (xAxis != 0 || yAxis != 0)
                changeAnimationState(WALK_RIGHT);
            else
                changeAnimationState(RIGHT);
        } 
        else if (xShoot < 0)
		{
            if (xAxis != 0 || yAxis != 0)
                changeAnimationState(WALK_LEFT);
            else
                changeAnimationState(LEFT);
        }
        else if (yShoot > 0)
        {
            if (xAxis != 0 || yAxis != 0)
                changeAnimationState(WALK_UP);
            else
                changeAnimationState(UP);
        }
        else if (yShoot < 0)
        {
            if (xAxis != 0 || yAxis != 0)
                changeAnimationState(WALK_DOWN);
            else
                changeAnimationState(DOWN);
        }
        // animations without shooting
        else if (xAxis > 0)
		{
            changeAnimationState(WALK_RIGHT);
        }
        else if (xAxis < 0)
        {
            changeAnimationState(WALK_LEFT);
        }
        else if (yAxis > 0)
        {
            changeAnimationState(WALK_UP);
        }
        else if (yAxis < 0)
        {
            changeAnimationState(WALK_DOWN);
        }
        else 
            changeAnimationState(DOWN);
    }
}
