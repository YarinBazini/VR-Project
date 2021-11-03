using UnityEngine;

public class BasketballBasket : MonoBehaviour
{
    public bool startMove = false;
    public enum Side {LEFT,RIGHT, MIDDLE};
    private float minBasketYAxis = 47.3f, maxBasketYAxis = 96f, originBasketYAxis = 72.4f,
        originBasketXAxis = 0.1f, originBasketZAxis = -2.6f, moveSpeed = 1f;
    public Side basketSide = Side.MIDDLE;

    // Update is called once per frame
    void Update()
    {
        Vector3 initialPos;
        if (startMove)
        {
            if (basketSide == Side.MIDDLE)
            {
                initialPos = new Vector3(originBasketXAxis, minBasketYAxis, originBasketZAxis);
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, Time.deltaTime * moveSpeed);
            }
            else if (basketSide == Side.LEFT)
            {
                initialPos = new Vector3(originBasketXAxis, maxBasketYAxis, originBasketZAxis);
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, Time.deltaTime * moveSpeed);

            }
            else if (basketSide == Side.RIGHT)
            {
                initialPos = new Vector3(originBasketXAxis, minBasketYAxis, originBasketZAxis);
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, Time.deltaTime * moveSpeed);
            }
        }
    }

    /* the function return the basket to his origin position */
    public void setOriginPosition()
    {
        transform.localPosition = new Vector3(originBasketXAxis, originBasketYAxis, originBasketZAxis);
    }

    /* the funtion check if the basket get to one of the sides and update basketSide property */
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.name == "TriggerLeft")
            basketSide = Side.LEFT;
        else if (collider.transform.name == "TriggerRight")
            basketSide = Side.RIGHT;
    }
}
