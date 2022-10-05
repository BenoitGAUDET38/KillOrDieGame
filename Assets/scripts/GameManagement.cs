using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    float timer = 0f;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public Text textScore;
    public Camera camera;

    int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 playerSpawnPoint = new Vector3(0, 0, 0);
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint, transform.rotation) as GameObject;

        camera.GetComponent<CameraLogic>().setPlayer(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2)
		{
            timer = 0;
            spawnEnemy();
		}

        textScore.text = "Score : " + score;
    }

	private void FixedUpdate()
	{
		score++;
	}

	void spawnEnemy()
	{
        Vector3 enemyPosition = new Vector3(transform.position.x + Random.Range(-9, 9), transform.position.y + Random.Range(-9, 9), 0);
        Instantiate(enemyPrefab, enemyPosition, transform.rotation);
	}
}
