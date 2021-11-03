using UnityEngine;
public class Ball : MonoBehaviour
{
    private BasketballPlayer player;
    private AudioSource audioGame;
    private float ballXPosStart = -0.3f, ballYPosStart = 0.2f, ballZPosStart = 2.25f, ballYPosPlayer = -30f, ballTimer = 3f;
    public bool isSent = false;
    private bool isMakeScore;

    // Start is called before the first frame update
    void Start()
    {
        isMakeScore = false;
        this.transform.position = new Vector3(ballXPosStart, ballYPosStart, ballZPosStart);
        this.transform.tag = "Ball";
        audioGame = player.getAudioObj().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSent)
        {
            ballTimer -= Time.deltaTime;
            if (ballTimer < 0)
                Destroy(gameObject);
        }
    }

    /* the function grub the ball to the player */
    public void getBallToPlayer()
    {
        BasketballPlayer player = FindObjectOfType<BasketballPlayer>();
        this.transform.parent = player.transform;
        this.transform.localPosition = new Vector3(0f, ballYPosPlayer, 0f);
    }

    /* the function check if the ball hit the trigger (ball enter to basket) and increase player score */
    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Trigger" && player.isStartScore && !isMakeScore)
        {
            isMakeScore = true;
            player.score++;
            playAudio();
        }
    }

    /* the function set the player property */
    public void setPlayer(BasketballPlayer playerRef)
    {
        player = playerRef;
    }

    
    /* the function play the score audio */
    void playAudio()
    {
        audioGame.Play();
    }
}
