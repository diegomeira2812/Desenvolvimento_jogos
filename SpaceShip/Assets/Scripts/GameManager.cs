using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore = 0; // Pontuação do player
    public static int lifes = 3; // Vidas do jogador
    public static GameObject thePlayer; // Referência ao objeto jogador
    private float shipInterval;
    public static float shipSpeed = -2.0f;
    public GameObject ship1Prefab;
    public GameObject ship2Prefab;
    public GameObject ship3Prefab;
    public GameObject background1;
    public GameObject background2;
    public GameObject stars1;
    public GameObject stars2;
    public int SlowMotion = 50;
    private float slowMotionTime = 1.0f;
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); // Busca a referência do jogador
        RestartGame();
        shipInterval = Random.Range(1f, 2f);
        UpdateScoreText();
    }

    public void LoseLife()
    {
        lifes--;
        Debug.Log("Lives: " + lifes);
        thePlayer.SendMessage("RestartPosition", null, SendMessageOptions.RequireReceiver);

        if (lifes == 0)
        {
            Derrota();
            thePlayer.SendMessage("Die", null, SendMessageOptions.RequireReceiver);
        }

        GameObject[] Invader = GameObject.FindGameObjectsWithTag("Invader");
        foreach (GameObject invader in Invader)
        {
            Destroy(invader);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza a pontuação no console
        Debug.Log("Score: " + PlayerScore);
        UpdateScoreText();

        //slowMotion
        if (PlayerScore >= SlowMotion)
        {
            Debug.Log("SLOWMOTION");
            slowMotionTime -= Time.deltaTime;

            background1.GetComponent<Parallax>().setSpeed(0.1f);
            background2.GetComponent<Parallax>().setSpeed(0.1f);
            stars1.GetComponent<Parallax>().setSpeed(0.05f);
            stars2.GetComponent<Parallax>().setSpeed(0.05f);

            GameObject[] invaders = GameObject.FindGameObjectsWithTag("Invader");
            GameObject[] invaderBullets = GameObject.FindGameObjectsWithTag("InvaderBullet");

            foreach (GameObject invader in invaders)
            {
                invader.GetComponent<Invaders>().setSpeedLevel(0);
            }

            foreach (GameObject invaderBullet in invaderBullets)
            {
                invaderBullet.GetComponent<InvaderBullet>().setSpeed(-2f);
            }

            if (slowMotionTime <= 0)
            {
                slowMotionTime = 10f;

                background1.GetComponent<Parallax>().setSpeed(0.5f);
                background2.GetComponent<Parallax>().setSpeed(0.5f);
                stars1.GetComponent<Parallax>().setSpeed(0.3f);
                stars2.GetComponent<Parallax>().setSpeed(0.3f);

                invaders = GameObject.FindGameObjectsWithTag("Invader");
                invaderBullets = GameObject.FindGameObjectsWithTag("InvaderBullet");

                foreach (GameObject invader in invaders)
                {
                    invader.GetComponent<Invaders>().setSpeedLevel(1);
                }
            }
        }

        if (PlayerScore >= 100)
        {
            SceneManager.LoadScene("vitoria");
        }

        shipInterval -= Time.deltaTime;
        if (shipInterval <= 0)
        {
            shipInterval = Random.Range(1.0f, 2.0f);
            SpawnShip();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + PlayerScore.ToString();
    }
    public void Derrota()
    {
        SceneManager.LoadScene("derrota");
    }

    public void RestartGame()
    {
        lifes = 3;
        PlayerScore = 0;
    }

    void SpawnShip()
    {
        int random = Random.Range(0, 3);
        GameObject ship;

        if (random == 0)
        {
            ship = Instantiate(ship1Prefab) as GameObject;
        }
        else if (random == 1)
        {
            ship = Instantiate(ship2Prefab) as GameObject;
        }
        else
        {
            ship = Instantiate(ship3Prefab) as GameObject;
        }

        Rigidbody2D rb2d = ship.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = ship.AddComponent<Rigidbody2D>();
            rb2d.gravityScale = 0;
        }

        float randomY = Random.Range(-2.5f, 2.5f);
        ship.transform.position = new Vector2(8f, randomY);
        rb2d.velocity = new Vector2(shipSpeed, 0);
    }
}
