using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float force = 300f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Debug.Log("Found player");
                Vector3 bounceDirection = -collision.contacts[0].normal;
                playerRigidbody.AddForce(bounceDirection * force);
            }
        }
    }
}