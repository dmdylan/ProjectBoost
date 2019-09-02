using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private Text levelCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        levelCounter.text = String.Format("Level {0}", SceneManager.GetActiveScene().buildIndex + 1);
    }

}
