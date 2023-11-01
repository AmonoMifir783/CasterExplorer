using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 20f;
    public float attackInterval = 3f;
    public int baseDamage = 100;

    private float nextAttackTime = 0f;

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within attack range
        if (distanceToPlayer <= attackRange)
        {
            // Check if enough time has passed since the last attack
            if (Time.time >= nextAttackTime)
            {
                // Attack the player
                float damage = (attackRange - distanceToPlayer) / attackRange;
                int finalDamage = Mathf.RoundToInt(baseDamage * damage);
                //int damage = distanceToPlayer < (attackRange / 2) ? increasedDamage : baseDamage;
                AttackPlayer(finalDamage);

                // Set the next attack time
                nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    private void AttackPlayer(int finalDamage)
    {
        PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealthScript.TakeDamage(finalDamage);
        // TODO: Implement your attack logic here
        // You can apply damage to the player, trigger animations, or any other desired actions
        Debug.Log("Enemy attacks player with damage: " + finalDamage);
    }
}
