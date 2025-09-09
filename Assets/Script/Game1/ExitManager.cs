using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupHandler : MonoBehaviour
{
    public GameObject popupPanel; // Drag panel pop-up dari Hierarchy ke sini

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ganti sesuai nama scene kamu
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false); // Menyembunyikan panel popup
    }
}
