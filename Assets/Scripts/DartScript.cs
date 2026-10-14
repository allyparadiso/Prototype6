using UnityEngine;
using UnityEngine.InputSystem;

public class DartScript : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform spawnPoint;
    public float dartSpeed = 15f;

    public Camera mainCamera;
    private float maxRange = 20f;

    private Rigidbody rb;
    private bool hasHit = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowDart();
        }
    }


    void ThrowDart()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxRange);
        }

        Vector3 direction = (targetPoint - spawnPoint.position).normalized;

        Instantiate(dartPrefab, spawnPoint.position, Quaternion.LookRotation(direction));
        
        if (rb != null)
        {
            rb.linearVelocity = direction * dartSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.gameObject.CompareTag("Balloon"))
        {
            Destroy(gameObject);
        }
    }
}
//detect collision w/ balloons
//throwing
//instantiate