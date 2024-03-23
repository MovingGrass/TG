using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private bool ended = false;

    private bool win = false;

    public void EndGame()
    {
        if (!ended)
        {
            ended = true;
            Debug.Log("Game Over");
            Restart();
        }
    }

    public void Win()
    {
        if(!win)
        {
            win = true;
            SceneManager.LoadScene("WIN");
            Invoke(nameof(LoadMainMenu), 5f);
        }

    }

    private void Restart()
    {
        SceneManager.LoadScene("GameOver");
        Invoke(nameof(LoadMainMenu), 5f); // Invoke the LoadMainMenu method after 5 seconds
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }

    private void Update()
    {
        // Your Update logic here
    }
}

