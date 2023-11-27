//using UnityEngine;

//public class MusicTrigger : MonoBehaviour
//{
//    public AudioSource audioSource;

//    private void OnTriggerEnter(Collider other)
//    {
//        // ���������, ��� ������, �������� � ���������, �������� �������
//        if (other.CompareTag("Player"))
//        {
//            // ������������� ������
//            audioSource.Play();
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        // ���������, ��� ������, ��������� �� ����������, �������� �������
//        if (other.CompareTag("Player"))
//        {
//            // ������������� ��������������� ������
//            audioSource.Stop();
//        }
//    }
//}
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 1f; // ������������ �������� ��������

    private bool isPlaying = false; // ����, �����������, ������ �� ������
    private float targetVolume = 0f; // ������� ��������� ������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������, �������� � ���������, �������� �������
        if (other.CompareTag("Player"))
        {
            // ������������� ������� ��������� �� ������������ ��������
            targetVolume = 1f;

            // ��������� ������� ��������� ������
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
        // ���������, ��� ������, ��������� �� ����������, �������� �������
        if (other.CompareTag("Player"))
        {
            // ������������� ������� ��������� �� ����������� ��������
            targetVolume = 0f;

            // ��������� ������� ������������ ������
            if (isPlaying)
            {
                isPlaying = false;
            }
        }
    }

    private void Update()
    {
        // ������ �������� ��������� ������ � ������������ � ������� ���������
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, fadeDuration * Time.deltaTime);

        // ���� ��������� �������� ���� � ������ ��������� ������, ������������� ���������������
        if (audioSource.volume == 0f && !isPlaying)
        {
            audioSource.Stop();
        }
    }
}