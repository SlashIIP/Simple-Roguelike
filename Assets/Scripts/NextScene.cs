using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextScene;

    void Start()
    {
        SceneManager.LoadScene(nextScene);

    }

    void Update()
    {
        
    }
}
