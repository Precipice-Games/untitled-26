using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float moveAmount = 1f;

    private static MoveCube selectedTile;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (selectedTile != null)
        {
            selectedTile.ResetColor();
        }

        selectedTile = this;
        rend.material.color = Color.yellow;
    }

    void ResetColor()
    {
        rend.material.color = originalColor;
    }

    public static void MoveRight()
    {
        if (selectedTile != null)
        {
            selectedTile.transform.position += Vector3.right * selectedTile.moveAmount;
            Debug.Log("Moved to the right.");
        }
    }

    public static void MoveLeft()
    {
        if (selectedTile != null)
        {
            selectedTile.transform.position += Vector3.left * selectedTile.moveAmount;
            Debug.Log("Moved platform left.");
        }
    }

    public static void MoveForward()
    {
        if (selectedTile != null)
        {
            selectedTile.transform.position += Vector3.forward * selectedTile.moveAmount;
            Debug.Log("Moved platform forwards.");
        }
    }

    public static void MoveBack()
    {
        if (selectedTile != null)
        {
            selectedTile.transform.position += Vector3.back * selectedTile.moveAmount;
            Debug.Log("Moved platform backwards.");
        }
    }
    
}
