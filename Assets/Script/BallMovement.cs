using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float power = 10f; // Topa uygulanacak g��
    private Rigidbody rb;

    private LineRenderer lr;
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Topun RigidBody bile�eni al�n�r
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Fare sol tu�una bas�ld���nda
        {
            Vector3 force = CalculateForce(); // Uygulanacak g�� hesaplan�r
            rb.AddForce(-force, ForceMode.Impulse); // G�� topa uygulan�r
        }

        lr.SetPosition(0, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            lr.SetPosition(1, raycastHit.point);
        }
    }

    private Vector3 CalculateForce()
    {
        // Topa uygulanacak g�c�n y�n�n� ve b�y�kl���n� hesaplar

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
}
