using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }

    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(1f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1;
        FindObjectOfType<ScenePersist>().ResetGamePersist();
        // Debug.Log(SceneManager.sceneCountInBuildSettings);
        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            FindObjectOfType<GameSession>().ResetPoint();
            SceneManager.LoadScene(0);       
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
