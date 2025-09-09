using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    [Header("List ikon hati")]
    public List<GameObject> heartIcons;

    private int livesRemaining;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Set jumlah nyawa awal sesuai jumlah hati
        livesRemaining = heartIcons.Count;

        // Update tampilan UI
        UpdateHeartUI();

        // Dengarkan perubahan scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cek apakah scene saat ini adalah Game1–Game10
        if (scene.name.StartsWith("Game"))
        {
            // Aktifkan Canvas hati
            if (heartIcons.Count > 0)
                heartIcons[0].transform.parent.gameObject.SetActive(true);
        }
        else
        {
            // Sembunyikan Canvas hati di MainMenu, EndScene, dll
            if (heartIcons.Count > 0)
                heartIcons[0].transform.parent.gameObject.SetActive(false);
        }

        // Saat masuk EndScene, reset nyawa untuk game berikutnya
        if (scene.name == "EndScene" || scene.name == "MainMenu")
        {
            ResetLives();
        }

        UpdateHeartUI();
    }

    public void UseLife()
    {
        if (livesRemaining <= 0) return;

        livesRemaining--;
        UpdateHeartUI();

        if (livesRemaining == 0)
        {
            Debug.Log("Game Over! Nyawa habis.");
            // Kamu bisa panggil handler game over di sini kalau perlu
        }
    }

    public bool CanUseLife()
    {
        return livesRemaining > 0;
    }

    public void ResetLives()
    {
        livesRemaining = heartIcons.Count;
        UpdateHeartUI();
    }

    public int GetRemainingLives()
    {
        return livesRemaining;
    }

    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartIcons.Count; i++)
        {
            heartIcons[i].SetActive(i < livesRemaining);
        }
    }
}
