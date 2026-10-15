using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dart"))
        {
            gameManager.AddScore();
            Destroy(gameObject);
        }
    }
}
