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

    public void MoveSelectedRight()
    {
        if (selectedCube != null)
            selectedCube.TryMove(1, 0);
    }

    public void MoveSelectedLeft()
    {
        if (selectedCube != null)
            selectedCube.TryMove(-1, 0);
    }

    public void MoveSelectedForward()
    {
        if (selectedCube != null)
            selectedCube.TryMove(0, 1);
    }

    public void MoveSelectedBack()
    {
        if (selectedCube != null)
            selectedCube.TryMove(0, -1);
    }
}