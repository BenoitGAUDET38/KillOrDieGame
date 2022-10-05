using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public Camera camera;


    private GameObject textScore;
    private GameObject textBestScore;
    private GameObject textFinalScore;
    private GameObject textRestart;

    private float timerEnemy = 0f;
    private GameObject player;
    private int score = 0;
    private int bestScore = 0;
    private bool endGame = false;

    void Awake()
    {
        // find Text objects
        textScore = GameObject.Find("TextScore");
        textBestScore = GameObject.Find("TextBestScore");
        textFinalScore = GameObject.Find("TextFinalScore");
        textRestart = GameObject.Find("TextRestart");

        // Hide after game text
        textFinalScore.SetActive(false);
        textRestart.SetActive(false);

        // create player
        Vector3 playerSpawnPoint = new Vector3(0, 0, 0);
        player = Instantiate(playerPrefab, playerSpawnPoint, transform.rotation) as GameObject;
        camera.GetComponent<CameraLogic>().setPlayer(player.transform);

        updateBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (endGame)
		{
            if (!Input.GetKey(KeyCode.Space))
                return;

            setupNewGame();
		}

        if (!player.activeSelf)
            endingGame();

        spawnEnemy();
        updateScore();
    }

	private void FixedUpdate()
	{
		score++;
	}

    void endingGame()
	{
        endGame = true;

        // manage best score
        if (score > bestScore)
            bestScore = score;

        // Show after game UI
        textFinalScore.SetActive(true);
        textRestart.SetActive(true);
        textFinalScore.GetComponent<Text>().text = "Final score : " + score;
    }

    void setupNewGame()
	{
        // destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
		}

        // reset score
        score = 0;
        updateBestScore();

        // Hide after game UI
        textFinalScore.SetActive(false);
        textRestart.SetActive(false);

        // reset player for the new game
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = transform.rotation;
        player.SetActive(true);

        endGame = false;
    }

    void updateBestScore()
	{
        textBestScore.GetComponent<Text>().text = "Best score : " + bestScore;
    }

    void updateScore()
	{
        textScore.GetComponent<Text>().text = "Score : " + score;
    }


    void spawnEnemy()
	{
        timerEnemy += Time.deltaTime;

        if (timerEnemy > 2)
        {
            timerEnemy = 0;
            Vector3 enemyPosition = new Vector3(transform.position.x + Random.Range(-9, 9), transform.position.y + Random.Range(-9, 9), 0);
            Instantiate(enemyPrefab, enemyPosition, transform.rotation);
        }
	}
}
