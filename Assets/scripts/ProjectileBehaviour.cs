using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 40f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = transform.up * speed;
        transform.position += transform.up * speed * Time.deltaTime;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag != "Joueur" && collision.gameObject.tag != "Projectile")
        {
            Destroy(gameObject);
        }
	}
}
