using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hazard : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerHealth playerHealth;
    Level01Controller uiManager;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth
        = other.gameObject.GetComponent<PlayerHealth>();

        PlayerHealth DamagePlayer = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            
        }
    }

}
