using UnityEngine;
using UnityEngine.InputSystem; 

public class SelectableCube : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public bool isSelected = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Select()
    {
        isSelected = true;
        rend.material.color = highlightColor;
    }

    public void Deselect()
    {
        isSelected = false;
        rend.material.color = originalColor;
    }
}
