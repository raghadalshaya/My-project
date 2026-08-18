using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // عشان نقدر نناديه من أي سكربت ثاني

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private int enemiesRemaining;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // يعد كل الوحوش الموجودين بالسين اللي عليهم تاق "Enemy"
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyDied()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            ShowWinPanel();
        }
    }

    public void PlayerDied()
    {
        ShowLosePanel();
    }

    void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void ShowLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}