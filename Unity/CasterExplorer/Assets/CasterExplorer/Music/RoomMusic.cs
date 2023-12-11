//using UnityEngine;

//public class MusicTrigger : MonoBehaviour
//{
//    public AudioSource audioSource;

//    private void OnTriggerEnter(Collider other)
//    {
//        // Проверяем, что объект, входящий в коллайдер, является игроком
//        if (other.CompareTag("Player"))
//        {
//            // Воспроизводим музыку
//            audioSource.Play();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        // Проверяем, что объект, выходящий из коллайдера, является игроком
//        if (other.CompareTag("Player"))
//        {
//            // Останавливаем воспроизведение музыки
//            audioSource.Stop();
//        }
//    }
//}
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 1f; // Длительность плавного перехода
    private bool isPlaying = false; // Флаг, указывающий, играет ли музыка
    private float targetVolume = 0f; // Целевая громкость музыки
    private GameObject ambianceMusic;
    private AmbianceMusic ambianceMusicScript;

    private void Start()
    {
        ambianceMusic = GameObject.FindGameObjectWithTag("AmbianceMusic");
        ambianceMusicScript = ambianceMusic.GetComponent<AmbianceMusic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект, входящий в коллайдер, является игроком
        if (other.CompareTag("Player"))
        {
            // Останавливаем эмбиентную музыку
            ambianceMusicScript.StopMusic();

            // Устанавливаем целевую громкость на максимальное значение
            targetVolume = 0.35f;
            // Запускаем плавное появление музыки
            if (!isPlaying)
            {
                audioSource.volume = 0f;
                audioSource.Play();
                isPlaying = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что объект, выходящий из коллайдера, является игроком
        if (other.CompareTag("Player"))
        {
            // Восстанавливаем эмбиентную музыку
            ambianceMusicScript.RestartMusic();

            // Устанавливаем целевую громкость на минимальное значение
            targetVolume = 0f;
            // Запускаем плавное исчезновение музыки
            if (isPlaying)
            {
                isPlaying = false;
            }
        }
    }

    private void Update()
    {
        // Плавно изменяем громкость музыки в соответствии с целевым значением
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, fadeDuration * Time.deltaTime);
        // Если громкость достигла нуля и музыка перестала играть, останавливаем воспроизведение
        if (audioSource.volume == 0f && !isPlaying)
        {
            audioSource.Stop();
        }
    }
}