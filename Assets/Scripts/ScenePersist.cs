using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numScenepersist = FindObjectsOfType<ScenePersist>().Length;
        if (numScenepersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetGamePersist()
    {
        Destroy(gameObject);
    }
}
