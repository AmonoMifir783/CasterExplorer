using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentHealth;
    public int inventoryCount;
    public PlayerHealth playerHealthScript;
    public float playerHealthFillAmount;
    public string MagisteriyaCount;
    public Vector3 playerPosition;
    public MagisteriyaFruitPickUp MagisterutaFruitPickUp;

    public SerializableDictionary<string, bool> magisteriyaCollected;

    public GameData()
    {
        this.inventoryCount = 0;
        this.MagisteriyaCount = inventoryCount.ToString();
        this.playerHealthFillAmount = 1f;
        this.currentHealth = 100;
        playerPosition = Vector3.zero;
        magisteriyaCollected = new SerializableDictionary<string, bool>();
    }
}