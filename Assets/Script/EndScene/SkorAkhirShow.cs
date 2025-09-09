using UnityEngine;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;      // Text untuk skor akhir
    [SerializeField] private TMP_Text correctText;    // Text untuk jumlah benar/total soal

    void Start()
    {
        if (scoreText == null || correctText == null)
        {
            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
            if (texts.Length > 0) scoreText = texts[0];
            if (texts.Length > 1) correctText = texts[1];
        }

        if (scoreText != null)
        {
            int finalScore = ScoreManager.Instance.GetSkorAkhir();
            scoreText.text = $"Nilai anda adalah {finalScore} dari total 100 poin";
        }
        else
        {
            Debug.LogWarning("TMP_Text untuk skor tidak ditemukan!");
        }

        if (correctText != null)
        {
            int benar = ScoreManager.Instance.totalCorrect;
            int total = ScoreManager.Instance.totalQuestions;
            correctText.text = $"Anda menjawab benar sebanyak {benar} dari {total} soal";
        }
        else
        {
            Debug.LogWarning("TMP_Text untuk benar/total tidak ditemukan!");
        }
    }

    public void BackToMenu()
    {
        ScoreManager.Instance.ResetSkor();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
