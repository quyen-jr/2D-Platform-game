using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float playerLives = 3;
    [SerializeField] int scores = 0;
    [SerializeField] TextMeshProUGUI scoresLive;
    [SerializeField] TextMeshProUGUI LivesLive;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        scoresLive.text ="0";
        LivesLive.text = playerLives.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        

    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LivesLive.text = playerLives.ToString();
        SceneManager.LoadScene(currentSceneIndex);
    }
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().ResetGamePersist();
        Destroy(gameObject);
    }
    public void AddScore(int point)
    {
        scores += point;
        scoresLive.text = scores.ToString();
    }
    public void ResetPoint()
    {
        scores = 0;
        playerLives = 3;
        LivesLive.text = playerLives.ToString();
        scoresLive.text = "0";
    }


}
