using UnityEngine;
using System.Collections.Generic;

public class SceneOrderManager : MonoBehaviour
{
    public static SceneOrderManager Instance;

    private List<string> sceneOrder = new List<string>();
    private int currentIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSceneOrder();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeSceneOrder()
    {
        sceneOrder = new List<string> {
            "Game1", "Game2", "Game3", "Game4", "Game5",
            "Game6", "Game7", "Game8", "Game9", "Game10"
        };

        Shuffle(sceneOrder);
        currentIndex = 0;
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public string GetNextSceneName()
    {
        if (currentIndex < sceneOrder.Count)
            return sceneOrder[currentIndex++];
        else
            return null;
    }

    public void ResetAndShuffle()
    {
        InitializeSceneOrder();
    }

    // Tambahkan method ini untuk dipanggil dari MainMenu
    public void StartGameSequence()
    {
        ResetAndShuffle();
        string firstScene = GetNextSceneName();
        if (!string.IsNullOrEmpty(firstScene))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(firstScene);
        }
    }
}
