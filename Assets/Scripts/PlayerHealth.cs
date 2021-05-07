using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    Level01Controller uiManager;

    public void Awake()
    {
        uiManager = FindObjectOfType<Level01Controller>();
    }

    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer(8);
        }
        */
    }

    private void OnTriggerStay(Collider col)
    {

        string name = col.gameObject.name;
        if (name == "Enemy_bullet(Clone)")
        {
            Debug.Log("Collision detected");
            DamagePlayer(5);
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        health -= damageAmount;
        uiManager.UpdateHealthSlider();
    }

}
