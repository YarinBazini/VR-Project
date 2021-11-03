using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private RaycastHit hitHover;
    private bool isHover = false;
    private float fontSizeHover = 0.2f, fontSize = 0.15f;

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
        if (hit.transform.name == "WhackMoleBtn")
            WhackMoleGame();
        else if (hit.transform.name == "BasketballBtn")
            BasketBallGame();
        else if (hit.transform.name == "ExitBtn")
            ExitGame();
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

    /* the function exit the application */
    void ExitGame()
    {
        Application.Quit();
    }

    /* the function call WhackMoles menu */
    void WhackMoleGame()
    {
        SceneManager.LoadScene("GameWhackMoles");
    }

    /* the function call BasketBall menu */
    void BasketBallGame()
    {
        SceneManager.LoadScene("GameBasketball");
    }
}
