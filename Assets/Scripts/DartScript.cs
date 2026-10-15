using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DartScript : MonoBehaviour
{
    public static DartScript Instance;
    public GameManager gameManager;

    public GameObject dartPrefab;
    public Transform spawnPoint;
    public float dartForce = 10000f;

    public float cooldownTime = .5f;
    private bool onCooldown = false;

    public Camera mainCamera;
    private float maxRange = 500f;

    public Rigidbody rb;
    private bool hasHit = false;
    void Start()
    {
        rb = dartPrefab.GetComponent<Rigidbody>();
        if (Instance == null) Instance = this;
        rb.linearVelocity = transform.forward * dartForce;
        gameManager = GameManager.Instance;
    }

    public void ThrowDart()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, maxRange))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxRange);
        }

        Vector3 direction = (targetPoint - spawnPoint.position).normalized;

        Instantiate(dartPrefab, spawnPoint.position, Quaternion.LookRotation(direction));
        StartCoroutine(SpawnCooldown());
        rb.AddForce(rb.linearVelocity, ForceMode.Impulse);
        
    }


    IEnumerator SpawnCooldown()
    {
        onCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        onCooldown = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Balloon"))
        {

            gameManager.AddScore();
            Destroy(gameObject);
        }
    }
}
//detect collision w/ balloons
//throwing
//instantiate