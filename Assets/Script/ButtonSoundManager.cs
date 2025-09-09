using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;

    private AudioSource audioSource;

    public void OnButtonClicked()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("UIAudioSource");
            audioSource = audioObj.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioObj);
        }

        if (clickSound != null)
            audioSource.PlayOneShot(clickSound, sfxVolume);
    }
}
