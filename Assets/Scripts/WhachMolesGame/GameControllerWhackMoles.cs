using UnityEngine;

public class GameControllerWhackMoles : MonoBehaviour
{
    [SerializeField] private GameObject moleContainer, scoreText, timeText, restartBtn;
    [SerializeField] private MolePlayer player;
    [SerializeField] private float gameTimerInput = 60f;
    private Mole[] moles;
    private float gameTimer, initialMoleTimerInput = 1f, initialMoleDurationInput = 3f, minInitialMoleDuration = 1f, 
        initialMoleDuration, initialMoleDecremrnt = 0.2f, initialMoleTimer;
    private bool isGameEnd, isFound = false;
    public bool startGame = false;

    // Start is called before the first frame update
    void Start()
    {
        moles = moleContainer.GetComponentsInChildren<Mole>();
        player.game = this;
        SetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            restartBtn.SetActive(false);
            StartGame();
        }
    }

    /* the function responsible for the moles moves */
    void RunMoles()
    {
        initialMoleTimer -= Time.deltaTime;
        if (initialMoleTimer <= 0f)
        {
            moles[Random.Range(0, moles.Length)].Visible();
            initialMoleDuration -= initialMoleDecremrnt;
            if (initialMoleDuration < minInitialMoleDuration)
                initialMoleDuration = minInitialMoleDuration;
            initialMoleTimer = initialMoleDuration;
        }
    }

    /* the function end the game and update the score */
    void GameEnd()
    {
        isGameEnd = true;
        timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Game End";
        SetScoreTable();
        restartBtn.SetActive(true);
    }

    /* the function start the game */
    public void StartGame()
    {
        if (!isGameEnd)
            gameTimer -= Time.deltaTime;
        if (gameTimer > 0f)
        {
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "SCORE: " + player.score;
            timeText.GetComponent<TMPro.TextMeshProUGUI>().text = "Time left: " + Mathf.Floor(gameTimer);
            RunMoles();
        }
        else
            GameEnd();
    }

    /* the function update user score */
    void SetScoreTable()
    { 
        for(int i = 1; i <= 10 && !isFound; i++)
        {
            if (PlayerPrefs.GetInt("Mole"+i.ToString()) < player.score)
            {
                int tmpScore, newScore = player.score;
                for (int j = i; j <= 10; j++)
                {
                    tmpScore = PlayerPrefs.GetInt(("Mole" + j).ToString());
                    PlayerPrefs.SetInt("Mole"+j.ToString(), newScore);
                    newScore = tmpScore;
                }   
                isFound = true;
            }
        }
    }

    /* the function restart the game */
    public void ResetGame()
    {
        startGame = true;
        isFound = false;
        player.score = 0;
        SetGame();
    }

    /* the function set game properties */
    void SetGame()
    {
        gameTimer = gameTimerInput;
        initialMoleTimer = initialMoleTimerInput;
        initialMoleDuration = initialMoleDurationInput;
        isGameEnd = false;
        for (int i = 0; i < moles.Length; i++)
        {
            moles[i].ResetMole();
            moles[i].isClicked = true;
        }
    }
}
