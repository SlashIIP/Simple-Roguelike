using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject recordsPanel;
    public Text playerStats;

    void Start()
    {
        recordsPanel.SetActive(false);
        UpdatePlayerStats();
    }

    void Update()
    {

    }

    public void StartGame()
    {
        CharacterTracker.instance.currentLevel = 1;
        SceneManager.LoadScene(levelToLoad);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowRecords()
    {
        recordsPanel.SetActive(true);
    }

    public void RecordsBackButton()
    {
        recordsPanel.SetActive(false);
    }

    public void UpdatePlayerStats()
    {
        playerStats.text = "Максимальный уровень: " + CharacterTracker.instance.currentLevel;}
    }
