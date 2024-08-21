using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public AudioClip destroySound; // 소리 파일을 할당할 변수
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            PlayDestroySound();
            Destroy(gameObject, destroySound.length); // 소리 재생 후 오브젝트 제거
        }
    }

    private void PlayDestroySound()
    {
        if (audioSource != null && destroySound != null)
        {
            audioSource.PlayOneShot(destroySound);
        }
    }
}
