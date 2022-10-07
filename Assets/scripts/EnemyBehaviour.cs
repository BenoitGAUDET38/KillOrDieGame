using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float moveSpeed = 5;
    public int life = 1;

    private Transform target;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Joueur").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //Quaternion.LookRotation(player.position - transform.position), 100 * Time.deltaTime);
        LookAt2D();

        Vector3 pos = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed*Time.deltaTime);
        //move towards the player
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(pos);
        //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        //transform.LookAt(target);
    }

    public void LookAt2D()
    {
        Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
        Vector2 current = transform.position;
        Vector2 direction = targetPosition - current;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // logic when enemy is hit by a projectile
    void getHitByProjectile()
	{
        life--;
        if (life <= 0)
            Destroy(gameObject);
	}


    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Projectile")
        {
            getHitByProjectile();
        }
    }
}
