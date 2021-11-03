using UnityEngine;

public class MoleHammer : MonoBehaviour
{
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, initialPos, Time.deltaTime);
    }

    /* the function change hammer position according to hit mole */
    public void Hit(Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}
