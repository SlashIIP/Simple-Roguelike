using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();

        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.currentLevel++;

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;
        }
    }
}
