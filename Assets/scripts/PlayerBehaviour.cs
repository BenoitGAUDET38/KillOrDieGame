using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10;
    public GameObject projectilePrefab;
    public float attackRate = 0.2f;

    private Rigidbody2D rb;
    private float timerAttackRate = 0f;

	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
    {
        move();
        fire();
    }

    void move()
	{
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(inputX, inputY).normalized * moveSpeed;

        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void fire()
	{
        if (timerAttackRate < attackRate)
		{
            timerAttackRate += Time.deltaTime;
            return;
		}

        if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Space))
		{
            Vector3 thePosition = transform.TransformPoint(Vector3.up);
            Instantiate(projectilePrefab, thePosition, transform.rotation);
            timerAttackRate = 0f;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
