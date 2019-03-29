using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform playerSpawn;
    public GameObject player;

    public Material player1Mat;
    public Material player2Mat;

    private int player1Score;
    private int player2Score;

    private List<int> playerList;

    public Text score1;
    public Text score2;

    public Text countText;
    private float startCount = 11f;

    private static GameManager instance;

    public static GameManager getGameManger()
    {
        return instance;
    }

    private void HardReset()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Awake()
    {
        instance = this;
        player1Score = PlayerPrefs.GetInt("score1");
        player2Score = PlayerPrefs.GetInt("score2");

        score1.text = player1Score.ToString();
        score2.text = player2Score.ToString();
        countText.text = "11";
    }

    // Use this for initialization
    void Start () {
        playerList = new List<int>();
        playerList.Add(1);
        playerList.Add(2);

        GameObject player1 =  Instantiate(player, playerSpawn.GetChild(0).position, Quaternion.identity);
        GameObject player2 =  Instantiate(player, playerSpawn.GetChild(1).position, Quaternion.identity);

        player1.transform.Rotate(new Vector3(0, 0, 90));
        player2.transform.Rotate(new Vector3(0, 0, 90));

        player1.GetComponentInChildren<SpriteRenderer>().material = player1Mat;
        player2.GetComponentInChildren<SpriteRenderer>().material = player2Mat;

        player1.GetComponent<PlayerScript>().setControll( 1,"Forward", "Horizontal", "Attack");
        player2.GetComponent<PlayerScript>().setControll( 2, "Forward2", "Horizontal2", "Attack2");
    }
	
	// Update is called once per frame
	void Update () {
        startCount -= Time.deltaTime;
        countText.text = ((int)startCount).ToString();
        if(startCount <= 0)
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            HardReset();
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
            
    }

    public void endGame(int playerDestroyed)
    {
        playerList.Remove(playerDestroyed);

        if (playerList.Count == 1)
        {
            var playerIdWin = playerList[0];
            var score = PlayerPrefs.GetInt("score" + playerIdWin) + 1;
            updateVisualScore(playerIdWin, score.ToString());
            PlayerPrefs.SetInt("score" + playerIdWin, score);

            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }

    private void updateVisualScore(int playerID, string playerScore)
    {
        switch (playerID)
        {
            case 1: score1.text = playerScore;
                break;
            case 2: score2.text = playerScore;
                break;
            default:
                break;
        }
    }
}
