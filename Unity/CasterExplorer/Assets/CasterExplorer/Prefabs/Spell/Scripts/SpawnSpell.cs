using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpell : MonoBehaviour
{
    public GameObject Spell;
    public Transform MagicWand;
    public Camera mainCamera;
    public float Force = 1000;
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] shootSounds;
    private PlayerStamina playerStamina;
    public GameObject Player;
    public bool isShooting;

    void Start()
    {
        playerStamina = Player.GetComponent<PlayerStamina>();
    }
    public void Shoot(int Temperature, int Force, int Amperage, int Gravity, int Light)
    {
        if (playerStamina.currentStamina >= 5)
        {
            isShooting = true;
            playerStamina.TakeFatigue(5);
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 dirWithoutSpread = targetPoint - MagicWand.position;



            var emission = Spell.transform.GetChild(0).GetComponent<ParticleSystem>().emission; // влияние на "хвост"
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(Temperature + Force + Amperage + Gravity + Light);

            if((Temperature + Force + Amperage + Gravity + Light) == 0)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(40);
            }


            Color newColor1 = new Color(Temperature / 255f, Force / 255f, Amperage / 255f);
            Color newColor2 = new Color(Temperature + 100 / 255f, Force + 100 / 255f, Amperage + 100 / 255f);

            var colorModule = Spell.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(newColor1, 0.0f), new GradientColorKey(newColor2, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }


            );
            colorModule.color = gradient;


            Spell.GetComponent<Spell>().Temperature = Temperature;
            Spell.GetComponent<Spell>().Force = Force;
            Spell.GetComponent<Spell>().Amperage = Amperage;
            Spell.GetComponent<Spell>().Gravity = Gravity;
            Spell.GetComponent<Spell>().Light = Light;


            GameObject currentSpell = Instantiate(Spell, MagicWand.position, Quaternion.identity);
            currentSpell.transform.forward = dirWithoutSpread.normalized;
            currentSpell.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized * Force, ForceMode.Impulse);
            if (shootSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, shootSounds.Length);
                audioSource.PlayOneShot(shootSounds[randomIndex]);
            }
        }
    }
}
