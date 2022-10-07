using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 10;
    public GameObject projectilePrefab;
    public float attackRate = 0.2f;

    private Rigidbody2D rb;
    private float timerAttackRate = 0f;
    private float xAxis, yAxis;
    private float xShoot, yShoot;

    private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
    {
        getInputs();
    }

	private void FixedUpdate()
	{
        move();
        attack();
    }

    void getInputs()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        xShoot = Input.GetAxis("xShoot");
        yShoot = Input.GetAxis("yShoot");
    }

    void move()
	{
        Vector2 moveDirection = new Vector2(xAxis, yAxis).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    void attack()
	{
        // check if the player attack isn't on cooldown
        if (timerAttackRate < attackRate)
		{
            timerAttackRate += Time.deltaTime;
            return;
		}

        // check if an attack button is registered
        if (xShoot == 0 && yShoot == 0)
            return;

        int projectileRotation;
        Vector3 projectilePosition;

        if (xShoot > 0)
        {
            projectileRotation = -90;
            projectilePosition = Vector3.right;
        }
        else if (xShoot < 0)
        {
            projectileRotation = 90;
            projectilePosition = Vector3.left;
        }
        else if (yShoot > 0)
        {
            projectileRotation = 0;
            projectilePosition = Vector3.up;
        }
        else
        {
            projectileRotation = 180;
            projectilePosition = Vector3.down;
        }

        Vector3 finalProjectilePosition = transform.TransformPoint(projectilePosition * 0.75f);
        Quaternion finalProjectileRotation = Quaternion.Euler(new Vector3(0, 0, projectileRotation));
        Instantiate(projectilePrefab, finalProjectilePosition, finalProjectileRotation);

        // reinitialize attack cooldown
        timerAttackRate = 0f;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);

            // stop all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}
