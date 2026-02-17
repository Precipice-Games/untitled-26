using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSelector : MonoBehaviour
{
    private SelectableCube selectedCube;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SelectableCube cube = hit.collider.GetComponent<SelectableCube>();
                if (cube != null)
                {
                    if (selectedCube != null)
                        selectedCube.Deselect();

                    selectedCube = cube;
                    selectedCube.Select();
                }
            }
        }
    }

    public void MoveSelectedRight(float speed)
    {
        if (selectedCube != null)
            selectedCube.transform.position += Vector3.right * speed;
    }

    public void MoveSelectedLeft(float speed)
    {
        if (selectedCube != null)
            selectedCube.transform.position += Vector3.left * speed;
    }

    public void MoveSelectedForward(float speed)
    {
        if (selectedCube != null)
            selectedCube.transform.position += Vector3.forward * speed;
    }

    public void MoveSelectedBack(float speed)
    {
        if (selectedCube != null)
            selectedCube.transform.position += Vector3.back * speed;
    }
}
