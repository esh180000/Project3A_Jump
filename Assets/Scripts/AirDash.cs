using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : MonoBehaviour
{

    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    private Rigidbody rb;

    Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Pressed");
           StartCoroutine(AirDashing());
        }
    }

   public IEnumerator AirDashing()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;

        
    }
}
