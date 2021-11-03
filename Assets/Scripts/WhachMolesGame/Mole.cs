using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] private float moleVisibleHeight = 0.6f, moleHiddenHeight = 0f, hiddenDuration = 1.5f;
    private Vector3 targetPosition;
    private float actionSpeed = 3f, hiddenTimer = 0f;
    public bool isClicked;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        targetPosition = new Vector3(transform.localPosition.x, moleHiddenHeight, transform.localPosition.z);
        transform.localPosition = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        hiddenTimer -= Time.deltaTime;
        if (hiddenTimer <= 0f)
            Hide();
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * actionSpeed);
    }

    /* the function make the mole visible */
    public void Visible()
    {
        targetPosition = new Vector3(transform.localPosition.x, moleVisibleHeight, transform.localPosition.z);
        hiddenTimer = hiddenDuration;
        isClicked = false;
    }

    /* the function make the mole hide */
    public void Hide()
    {
        targetPosition = new Vector3(transform.localPosition.x, moleHiddenHeight, transform.localPosition.z);
    }

    /* the function return the mole to hide position */
    public void ResetMole()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, moleHiddenHeight, transform.localPosition.z);
    }
}
