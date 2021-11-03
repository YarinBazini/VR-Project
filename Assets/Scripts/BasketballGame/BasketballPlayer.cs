using UnityEngine;

public class BasketballPlayer : MonoBehaviour
{
    [SerializeField] private GameObject audioObj;
    private Ball ball = null;
    public int score;
    public bool sendBall, isStartScore, isFirstBall = true;
    private float shootingForwardForce = 1000f, shootingUpForce = 600f;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.transform.tag == "Ball")
                {
                    isFirstBall = false;
                    ball = hit.transform.GetComponent<Ball>();
                    ball.getBallToPlayer();
                }
                else if (ball != null)
                {
                    sendBall = true;
                    ball.isSent = true;
                    ball.transform.parent = null;
                    ball.GetComponent<Rigidbody>().AddForce(transform.up * shootingUpForce);
                    ball.GetComponent<Rigidbody>().AddForce(transform.forward * shootingForwardForce);
                    ball.GetComponent<Rigidbody>().useGravity = true;
                    ball = null;
                }
            }
        }
    }

    /* the function return the audio object */
    public GameObject getAudioObj()
    {
        return audioObj;
    }
}
