using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    private Animator myAnimator;
    private string currentState;
    private Transform target;

    private static string UP = "Enemy_Up";
    private static string DOWN = "Enemy_Down";
    private static string LEFT = "Enemy_Left";
    private static string RIGHT = "Enemy_Right";

    private static string WALK_UP = "Enemy_WalkUp";
    private static string WALK_DOWN = "Enemy_WalkDown";
    private static string WALK_LEFT = "Enemy_WalkLeft";
    private static string WALK_RIGHT = "Enemy_WalkRight";

    // Start is called before the first frame update
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Joueur").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animate();
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
        float vectorX = target.position.x - transform.position.x;
        float vectorY = target.position.y - transform.position.y;

        if (Mathf.Abs(vectorX) >= Mathf.Abs(vectorY))
		{
            if (vectorX >= 0)
                changeAnimationState(WALK_RIGHT);
            else
                changeAnimationState(WALK_LEFT);
        }
        else
		{
            if (vectorY >= 0)
                changeAnimationState(WALK_UP);
            else
                changeAnimationState(WALK_DOWN);
        }
    }
}
