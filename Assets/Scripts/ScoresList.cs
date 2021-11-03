using UnityEngine;

public class ScoresList : MonoBehaviour
{
    [SerializeField] private GameObject listRight, listLeft;

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<GameControllerWhackMoles>())
            GetScores("Mole");
        else if(FindObjectOfType<GameControllerBasketball>())
            GetScores("Basket");
    }

    /* the function print the best scores on the game */
    void GetScores(string gameName)
    {
        listLeft.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        listRight.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        for (int i = 1; i <= 5; i++)
        {
            listLeft.GetComponent<TMPro.TextMeshProUGUI>().text += i.ToString() + " - ";
            if(PlayerPrefs.GetInt(gameName+i.ToString()) == 0)
                listLeft.GetComponent<TMPro.TextMeshProUGUI>().text += "---";
            else
                listLeft.GetComponent<TMPro.TextMeshProUGUI>().text += PlayerPrefs.GetInt(gameName + i.ToString()).ToString();
            listLeft.GetComponent<TMPro.TextMeshProUGUI>().text += "\n";
        }
        for (int i = 6; i <= 10; i++)
        {
            listRight.GetComponent<TMPro.TextMeshProUGUI>().text += i.ToString() + " - ";
            if (PlayerPrefs.GetInt(gameName + i.ToString()) == 0)
                listRight.GetComponent<TMPro.TextMeshProUGUI>().text += "---";
            else
                listRight.GetComponent<TMPro.TextMeshProUGUI>().text += PlayerPrefs.GetInt(gameName + i.ToString()).ToString();
            listRight.GetComponent<TMPro.TextMeshProUGUI>().text += "\n";
        }
    }
}
