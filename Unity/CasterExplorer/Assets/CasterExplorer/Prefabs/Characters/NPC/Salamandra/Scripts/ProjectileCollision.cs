using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damageAmount = 20;
    private SalamandraSR salamandraSr;
    private SpellReaction spellReaction;


    private void Start()
    {
        salamandraSr = GetComponent<SalamandraSR>();
        spellReaction = GetComponent<SpellReaction>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if (playerHealthScript)
            {
                playerHealthScript.TakeDamage(damageAmount);
                Debug.Log("� ������� 20 �����");
            }
            if (playerHealthScript != null && salamandraSr != null && salamandraSr.isBurning)
            {
                playerHealthScript.TakeDamage(salamandraSr.FireDamage);
                Debug.Log("� ������� 20 ����������");
            }

            Destroy(gameObject); // ���������� ������ ��� ������������ � �������
        }
    }
}