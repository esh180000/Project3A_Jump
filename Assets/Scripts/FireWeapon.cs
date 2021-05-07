using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 20f;
    //[SerializeField] GameObject visualFeedbackObject;
    [SerializeField] int weaponDamage = 20;
    [SerializeField] LayerMask hitLayers;

    public GameObject bullet;
    public float shootForce;
    public float spread;
    public Camera fpsCam;
    public Transform attackPoint;


    RaycastHit objectHit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    //Fire weapon using RayCast
    void Shoot()
    {
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.blue, 1f);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
        {
            Debug.Log("HIT" + objectHit.transform.name);

            if (objectHit.transform.tag == "Enemy")
            {
                EnemyShooter enemyShooter = objectHit.transform.gameObject.GetComponent<EnemyShooter>();
                if (enemyShooter != null)
                {
                    enemyShooter.takeDamage(weaponDamage);
                }
            }

        }
        else
        {
            Debug.Log("MISS");
        }
    }
}
