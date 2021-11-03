using UnityEngine;

public class MolePlayer : MonoBehaviour
{
    [SerializeField] private MoleHammer hammer;
    [SerializeField] private GameObject audioObj;
    public GameControllerWhackMoles game;
    private AudioSource audioGame;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        audioGame = audioObj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && game.startGame)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.tag == "Mole")
                {
                    Mole mole = hit.transform.GetComponent<Mole>();
                    if (!mole.isClicked)
                    {
                        hammer.Hit(mole.transform.position);
                        score++;
                        playAudio();
                    }
                    mole.isClicked = true;
                    mole.Hide();
                }
                else
                    hammer.Hit(hit.transform.position);
            }
        }
    }

    /* the function play the hit audio */
    void playAudio()
    {
        audioGame.Play();
    }
}
