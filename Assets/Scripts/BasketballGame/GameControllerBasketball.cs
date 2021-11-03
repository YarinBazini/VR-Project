using UnityEngine;

public class GameControllerBasketball : MonoBehaviour
{
    [SerializeField] private GameObject scoreText, timeText, restartBtn, ballPrab, helpArrow, basketObj;
    [SerializeField] private BasketballPlayer player;
    [SerializeField] private float gameTimerInput = 60f;
    private BasketballBasket basket;
    private float gameTimer, netMoveTimer = 30f;
    private bool isGameEnd, isFound = false;
    public bool startGame = false;

    // Start is called before the first frame update
    void Start()
    {
        basket = basketObj.GetComponent<BasketballBasket>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            if (!player.isFirstBall)
                helpArrow.SetActive(false);
            restartBtn.SetActive(false);
            player.isStartScore = true;
            StartGame();
        }
    }

    /* the function set game properties */
    void SetGame()
    {
        gameTimer = gameTimerInput;
        isGameEnd = false;
        if(FindObjectOfType<Ball>() == null && !isGameEnd)
            createBall();
    }

    /* the function create new ball */
    void createBall()
    {
        if (FindObjectOfType<Ball>() == null || FindObjectOfType<Ball>().isSent)
        {
            Ball ball = Instantiate(ballPrab).GetComponent<Ball>();
            ball.setPlayer(player);
        }
    }

    /* the function end the game and update the score */
    void GameEnd()
    {
        setOriginBasket();
        isGameEnd = true;
        timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Game End";
        player.isStartScore = false;
        SetScoreTable();
        restartBtn.SetActive(true);
    }

    /* the function start the game */
    public void StartGame()
    {
        if (!isGameEnd)
            gameTimer -= Time.deltaTime;
        if(gameTimer <= netMoveTimer)
            basket.startMove = true;
        if (gameTimer > 0f)
        {
            if (player.sendBall)
            {
                createBall();
                player.sendBall = false;
            }
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE: " + player.score;
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Time left: " + Mathf.Floor(gameTimer);
        }
        else
            GameEnd();
    }

    /* the function update user score */
    void SetScoreTable()
    {
        for (int i = 1; i <= 10 && !isFound; i++)
        {
            if (PlayerPrefs.GetInt("Basket"+i.ToString()) < player.score)
            {
                int tmpScore, newScore = player.score;
                for (int j = i; j <= 10; j++)
                {
                    tmpScore = PlayerPrefs.GetInt(("Basket" + j).ToString());
                    PlayerPrefs.SetInt("Basket"+j.ToString(), newScore);
                    newScore = tmpScore;
                }
                isFound = true;
            }
        }
    }

    /* the function restart the game */
    public void ResetGame()
    {
        setOriginBasket();
        startGame = true;
        isFound = false;
        player.score = 0;
        player.isFirstBall = true;
        SetGame();
    }

    /* the function return the player */
    public BasketballPlayer getPlayer()
    {
        return player;
    }

    /* the function return the help arrow */
    public GameObject getHelpArrow()
    {
        return helpArrow;
    }

    /* the function return the basket to his origin position */
    public void setOriginBasket()
    {
        basket.startMove = false;
        basket.setOriginPosition();
    }
}
