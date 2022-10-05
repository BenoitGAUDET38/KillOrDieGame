using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    private Transform playerBody;

	private void Start()
	{
		//playerBody = GameObject.FindGameObjectWithTag("Joueur").transform;
	}

	// Update is called once per frame
	void Update()
    {
		if (playerBody == null)
			return;

        transform.position = new Vector3(playerBody.position.x, playerBody.position.y, transform.position.z);
    }

	public void setPlayer(Transform playerTransform)
	{
		playerBody = playerTransform;
	}
}
