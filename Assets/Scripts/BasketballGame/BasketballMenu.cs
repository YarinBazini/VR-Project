using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketballMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameController, menuPopup, howToPopup, bestScoresPopup, gameInfoPopup, cameraObj;
    private GameControllerBasketball game;
    private RaycastHit hitHover;
    private bool isHover = false;
    private float gamePosCamera = -1f, menuPosCamera = 1f, fontSizeHover = 0.2f, fontSize = 0.17f;

    // Start is called before the first frame update
    void Start()
    {
        menuPopup.SetActive(true);
        game = gameController.GetComponent<GameControllerBasketball>();
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
            game.ResetGame();
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
        cameraObj.transform.position = new Vector3(gamePosCamera, cameraObj.transform.position.y, cameraObj.transform.position.z);
        game.getHelpArrow().SetActive(true);
        menuPopup.SetActive(false);
        gameInfoPopup.SetActive(true);
        game.ResetGame();
    }

    /* the function setup the back screen */
    void backSetUp()
    {
        game.setOriginBasket();
        game.getHelpArrow().SetActive(false);
        gameInfoPopup.SetActive(false);
        howToPopup.SetActive(false);
        bestScoresPopup.SetActive(false);
        menuPopup.SetActive(true);
        game.startGame = false;
        cameraObj.transform.position = new Vector3(menuPosCamera, cameraObj.transform.position.y, cameraObj.transform.position.z);
    }
}
