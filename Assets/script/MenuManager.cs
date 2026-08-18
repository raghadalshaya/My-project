using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // اسم السين الأساسي للعبة
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); // يظهر بس بالـ Editor
    }
}