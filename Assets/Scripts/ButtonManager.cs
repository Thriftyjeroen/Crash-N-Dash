using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EnableInfiniteMode()
    {
        if (RaceManager.Instance != null)
        {
            RaceManager.Instance.SetInfiniteMode(true);
        }
        else
        {
            Debug.LogWarning("RaceManager instance not found.");
        }
    }
}
