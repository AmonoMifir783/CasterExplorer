using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public List<int> SlotsSave;
    public int scrollCount;
    public string ScrollCount;


    public SerializableDictionary<string, bool> magisteriyaCollected;
    public SerializableDictionary<string, bool> deadSalamandras;

    public GameData()
    {
        this.maxHealth = 100;
        playerPosition = Vector3.one;
        this.inventoryCount = 0;
        this.MagisteriyaCount = inventoryCount.ToString();
        this.scrollCount = 5;
        this.ScrollCount = scrollCount.ToString();
        this.playerHealthFillAmount = 0f;
        this.currentHealth = 100;
        magisteriyaCollected = new SerializableDictionary<string, bool>();
        deadSalamandras = new SerializableDictionary<string, bool>();
        SlotsSave = new List<int>();

    }

    public int GetLevelInfo() // информация которая будет находиться в слоте сохранения
    {

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SceneNumber", sceneNumber);
        return sceneNumber;
    }
    public DateTime GetTimeInfo() // информация, которая будет находиться в слоте сохранения
    {
        DateTime currentDateTime = DateTime.Now;
        return currentDateTime;
    }

}