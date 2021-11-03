using UnityEngine;
using UnityEngine.SceneManagement;

public class WhackMolesMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameController, menuPopup, howToPopup, bestScoresPopup, gameInfoPopup, cameraObj,
        hammerMechine, hammerGame;
    private GameControllerWhackMoles game;
    private RaycastHit hitHover;
    private bool isHover = false;
    private float gamePosCamera = 3f, menuPosCamera = 2.5f, fontSizeHover = 0.2f, fontSize = 0.17f;

    // Start is called before the first frame update
    void Start()
    {
        menuPopup.SetActive(true);
        game = gameController.GetComponent<GameControllerWhackMoles>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "Button")
            {
                HoverButton(hit);
                if (Input.anyKeyDown)
                    ClickButton(hit);
            }
        }
        else if (isHover)
            RemoveHoverButton();
    }

    /* the function call to other function when button selected */
    void ClickButton(RaycastHit hit)
    {
        if (hit.transform.name == "NewGameBtn")
            newGameSetUp();
        else if (hit.transform.name == "HowToBtn")
        {
            menuPopup.SetActive(false);
            howToPopup.SetActive(true);
        }
        else if (hit.transform.name == "BestScoresBtn")
        {
            menuPopup.SetActive(false);
            bestScoresPopup.SetActive(true);
        }
        else if (hit.transform.name == "ExitBtn")
            SceneManager.LoadScene("MainMenu");
        else if (hit.transform.name == "RestartBtn")
        {
            game.startGame = true;
            game.ResetGame();
        }
        else if (hit.transform.name == "BackBtn")
            backSetUp();
    }

    /* the function change button design on hover */
    void HoverButton(RaycastHit hit)
    {
        hit.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = fontSizeHover;
        isHover = true;
        hitHover = hit;
    }

    /* the function change button design on back from hover */
    void RemoveHoverButton()
    {
        hitHover.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = fontSize;
        isHover = false;
    }

    /* the function setup the game */
    void newGameSetUp()
    {
        hammerMechine.SetActive(false);
        hammerGame.SetActive(true);
        cameraObj.transform.position = new Vector3(gamePosCamera, cameraObj.transform.position.y, cameraObj.transform.position.z);
        menuPopup.SetActive(false);
        gameInfoPopup.SetActive(true);
        game.startGame = true;
        game.ResetGame();
    }

    /* the function setup the back screen */
    void backSetUp()
    {
        gameInfoPopup.SetActive(false);
        howToPopup.SetActive(false);
        bestScoresPopup.SetActive(false);
        menuPopup.SetActive(true);
        game.startGame = false;
        cameraObj.transform.position = new Vector3(menuPosCamera, cameraObj.transform.position.y, cameraObj.transform.position.z);
        hammerMechine.SetActive(true);
        hammerGame.SetActive(false);
    }
}
