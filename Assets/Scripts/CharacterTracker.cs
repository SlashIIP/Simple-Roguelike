using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker instance;

    public int currentHealth, maxHealth, currentLevel;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
