using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 0.5f; // island speed

    public void MoveRight()
    {
        transform.position += Vector3.right * speed; //direction
    }
}
