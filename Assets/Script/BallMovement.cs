using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    public float power = 10f; // Topa uygulanacak güç
    private Rigidbody rb;
    private LineRenderer lr;

    public int click;

    public bool groundCheck;

    public AudioSource ballHitSound;
    public AudioSource obstacleHitSound;
    public AudioSource deadzoneSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Topun RigidBody bileþeni alýnýr
        lr = GetComponent<LineRenderer>();
        click = 0;
    }

    private void Update()
    {
        if (groundCheck)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lr.enabled = true;
            }
            if (Input.GetMouseButton(0))
            {
                lr.SetPosition(0, transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    Vector3 lrPos2 = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
                    lr.SetPosition(1, lrPos2);
                }
            }

            if (Input.GetMouseButtonUp(0)) // Fare sol tuþuna basýldýðýnda
            {
                Vector3 force = CalculateForce(); // Uygulanacak güç hesaplanýr
                Vector3 addForce = new Vector3(-force.x, 0, -force.z);
                rb.AddForce(addForce, ForceMode.Impulse); // Güç topa uygulanýr
                lr.enabled = false;
                click++;
                ballHitSound.Play();
            }
        }

        
    }

    private Vector3 CalculateForce()
    {
        // Topa uygulanacak gücün yönünü ve büyüklüðünü hesaplar

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3 ballPosition = transform.position;
            Vector3 forceDirection = hitPoint - ballPosition;
            forceDirection.Normalize();

            float distance = Vector3.Distance(hitPoint, ballPosition);
            float forceMagnitude = Mathf.Clamp(distance, 0f, 10f) * power;

            return forceDirection * forceMagnitude;
        }

        return Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "deadzone")
        {
            deadzoneSound.Play();
            rb.constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine(loadCurrentScene());
        }
        if (collision.transform.tag == "Obstacle")
        {
            obstacleHitSound.Play();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            groundCheck = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            groundCheck = false;
        }
    }

    IEnumerator loadCurrentScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
