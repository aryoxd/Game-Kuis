using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Data Skor")]
    public int totalCorrect = 0;       // berapa jawaban benar
    public int totalQuestions = 25;    // jumlah total soal

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TambahBenar()
    {
        totalCorrect++;
        Debug.Log($"Jawaban benar bertambah: {totalCorrect}/{totalQuestions}");
    }

    public void ResetSkor()
    {
        totalCorrect = 0;
    }


    public int GetSkorAkhir()
    {
        return Mathf.RoundToInt((totalCorrect / (float)totalQuestions) * 100);
    }

    public void KurangiBenar()
    {
        totalCorrect = Mathf.Max(0, totalCorrect - 1);
        Debug.Log($"Jawaban benar dikurangi: {totalCorrect}/{totalQuestions}");
    }
}
