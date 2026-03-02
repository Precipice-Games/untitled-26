using UnityEngine;

public class MountainElevator : MonoBehaviour
{
    public float maxHeight = 15.0f;
    public float minHeight = 0f;
    public float moveSpeed = 2.0f;
    public bool goUp = true;

    public bool startTimer = false;
    public float maxTime = 1f;
    public float currentTime = 0f;


    private void Update()
    {
        
        if (startTimer)
        {

            if (currentTime < maxTime)
            {

                currentTime += Time.deltaTime;

            }
            else
            {
                goUp = !goUp;
                currentTime = 0f;
                startTimer = false;
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (goUp)
        {

            transform.position = new Vector3 (transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);

        }
        else
        {

            transform.position = new Vector3(transform.position.x, transform.position.y - (moveSpeed * Time.deltaTime), transform.position.z);

        }

        if (transform.position.y > maxHeight)
        {

            //startTimer = true;
            goUp = false;

        }
        else if (transform.position.y < minHeight)
        {
            //startTimer = true;
            goUp = true;
        }

    }
}
