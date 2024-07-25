using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Instance singleton
    private static GameManager instance;

    // Method untuk mendapatkan instance singleton
    public static GameManager Instance
    {
        get
        {
            // Jika instance belum ada, cari GameManager di scene
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // Jika tidak ditemukan, buat instance baru
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Jika instance belum diset, set instance ini sebagai instance singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Jika instance sudah ada dan bukan instance ini, hancurkan objek ini
        else if (instance != this)
        {
            Destroy(gameObject);
        }
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
        if (!win)
        {
            win = true;
            SceneManager.LoadScene("WIN");
            Invoke(nameof(LoadMainMenu), 5f);
        }

    }

    private void Restart()
    {
        SceneManager.LoadScene("GameOver");
        Invoke(nameof(LoadMainMenu), 5f); // Panggil metode LoadMainMenu setelah 5 detik
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }

    private void Update()
    {
        // Logika Update Anda di sini
    }
}
