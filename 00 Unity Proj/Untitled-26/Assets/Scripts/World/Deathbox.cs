using UnityEngine;

public class Deathbox : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Player")
        {

            collision.gameObject.transform.position = new Vector3(2.79f,1.199f,-9.31f);

        }

    }

}
