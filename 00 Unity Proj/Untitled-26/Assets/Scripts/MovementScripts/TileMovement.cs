using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 0.5f; // speed of tile moving


//direction of the tile
    public void MoveRight()
    {
        transform.position += Vector3.right * speed;
    }

    public void MoveLeft()
    {
        transform.position += Vector3.left * speed;
    }

    public void MoveForward()
    {
        transform.position += Vector3.forward * speed;
    }

    public void MoveBack()
    {
        transform.position += Vector3.back * speed;
    }
}
