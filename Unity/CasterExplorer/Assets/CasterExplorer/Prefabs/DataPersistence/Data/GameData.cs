using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int currentHealth;
    public int inventoryCount;
    public PlayerHealth playerHealthScript;
    public float playerHealthFillAmount;
    public string MagisteriyaCount;
    public Vector3 playerPosition;
    public MagisteriyaFruitPickUp MagisterutaFruitPickUp;
    public DateTime currentDateTime;
    public int maxHealth;

    public SerializableDictionary<string, bool> magisteriyaCollected;

    public GameData()
    {
        this.maxHealth = 100;
        playerPosition = Vector3.zero;
        this.inventoryCount = 0;
        this.MagisteriyaCount = inventoryCount.ToString();
        this.playerHealthFillAmount = 0f;
        this.currentHealth = 0;
        magisteriyaCollected = new SerializableDictionary<string, bool>();
        currentDateTime = DateTime.Now;
    }

    public int GetLevelInfo() // информация которая будет находиться в слоте сохранения
    {

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SceneNumber", sceneNumber);
        return sceneNumber;
    }
    public int GetTimeInfo() // информация, которая будет находиться в слоте сохранения
    {
        DateTime currentDateTime = DateTime.Now;
        int unixTimestamp = (int)currentDateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        return unixTimestamp;
    }

}