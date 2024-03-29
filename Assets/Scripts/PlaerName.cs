﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaerName : MonoBehaviour
{
    private InputField input;
    // Start is called before the first frame update
    void Start()
    {
        // 1   
        input = GetComponent<InputField>();
        input.onValueChanged.AddListener(SavePlayerName);
        // 2  PlayerPrefs to look for and retrieve the value for a key named PlayerName
        var savedName = PlayerPrefs.GetString("PlayerName");
        if (!string.IsNullOrEmpty(savedName))
        {     input.text = savedName;
            GameManager.instance.playerName = savedName;
        }
    }
    private void SavePlayerName(string playerName)
    {
        // 3   
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        GameManager.instance.playerName = playerName;
    }

            // Update is called once per frame
    void Update()
    {
        
    }
}
