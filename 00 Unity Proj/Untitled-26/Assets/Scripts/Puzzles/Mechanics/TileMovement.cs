// using UnityEngine;
//
// // TODO: Should probably switch the actual movement process into here.
// //       Right now, SelectableTile.cs is handling both raycasting and
// //       movement, so it would be good if we could break that down.
//
// public class MoveCube : MonoBehaviour
// {
//     public float moveAmount = 0.1f;
//
//     private static MoveCube selectedTile;
//     private Renderer rend;
//     private Color originalColor;
//
//     void Start()
//     {
//         rend = GetComponent<Renderer>();
//         originalColor = rend.material.color;
//     }
//
//     void OnMouseDown()
//     {
//         if (selectedTile != null)
//         {
//             selectedTile.ResetColor();
//         }
//
//         selectedTile = this;
//         rend.material.color = Color.yellow;
//     }
//
//     void ResetColor()
//     {
//         rend.material.color = originalColor;
//     }
// }
