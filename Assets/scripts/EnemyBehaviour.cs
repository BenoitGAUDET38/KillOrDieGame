using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int life = 1;
    public ParticleSystem particlesDeathPrefab;

    private Transform target;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Joueur").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        float vectorX = target.position.x - transform.position.x;
        float vectorY = target.position.y - transform.position.y;
        Vector2 direction = new Vector2(vectorX, vectorY);
        direction.Normalize();

        rb.velocity = direction * moveSpeed;
    }

    // logic when enemy is hit by a projectile
    void getHitByProjectile()
	{
        life--;
        if (life > 0)
            return;


        Instantiate(particlesDeathPrefab, transform.position, new Quaternion());

        Destroy(gameObject);
	}

    void setHitColor()
	{
        spriteRenderer.color = Color.red;
        Invoke("setDefaultColor", 0.1f);
	}

    void setDefaultColor()
    {
        spriteRenderer.color = Color.white;
    }


    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Projectile")
        {
            getHitByProjectile();
            setHitColor();
        }
    }
}
