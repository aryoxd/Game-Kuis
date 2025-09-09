using UnityEngine;
using UnityEngine.SceneManagement;

public class DropZoneManager : MonoBehaviour
{
    public static DropZoneManager Instance;

    [Header("List semua dropzone di scene")]
    public DropZone[] dropZones;

    [Header("Sound dan Delay")]
    public AudioClip successSound;
    public float delayBeforeNextScene = 1.5f;

    [Header("Skor")]
    public int scorePerLevel = 10;

    private AudioSource audioSource;
    private bool levelCompleted = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void CheckAllZonesFilled()
    {
        if (levelCompleted) return;

        foreach (DropZone dz in dropZones)
        {
            if (!dz.IsOccupied())
                return;
        }

        levelCompleted = true;

        // ✅ Mainkan suara sukses
        if (successSound != null && audioSource != null)
            audioSource.PlayOneShot(successSound);

        Invoke(nameof(LoadNextScene), delayBeforeNextScene);
    }

    void LoadNextScene()
    {
        string nextScene = SceneOrderManager.Instance.GetNextSceneName();

        if (string.IsNullOrEmpty(nextScene))
        {
            // Semua soal selesai → End Scene
            SceneManager.LoadScene("EndScene"); // ganti jika nama scene akhir Anda berbeda
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
