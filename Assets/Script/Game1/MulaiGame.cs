using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnStartButtonClicked()
   {
        if (LifeManager.Instance != null)
            LifeManager.Instance.ResetLives();

        // mulai urutan scene, dll
        SceneOrderManager.Instance?.StartGameSequence();
    }
    
}
