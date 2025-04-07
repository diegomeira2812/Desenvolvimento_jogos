using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int PlayerScore1 = 0;
    public GUISkin layout;
    public static int lifes = 3;
    public static GameObject thePlayer;

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

    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
    }

    public void LoseLife()
    {
        lifes--;
        PlayerScore1 = 0; 
        if (lifes > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (lifes == 0)
        {
            GameOver();
        }
    }

    void Update()
    {
        if (PlayerScore1 >= 4)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene("YouWin");
                PlayerScore1 = 0;
            }
            else
            {
                PlayerScore1 = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2, 2, 200, 200), "" + PlayerScore1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        lifes = 3;
        PlayerScore1 = 0;
    }

    public void AddScore()
    {
        PlayerScore1 += 1;
    }
}
