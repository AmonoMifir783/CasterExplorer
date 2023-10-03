using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentHealth;

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> magisteriyaCollected;

    public GameData()
    { 
        this.currentHealth = 100;
        playerPosition = Vector3.zero;
        magisteriyaCollected = new SerializableDictionary<string, bool>();

    }
}