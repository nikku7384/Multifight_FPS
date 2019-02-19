     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float expireRate;
    public float movespeed;
    public float damageRate;

    private float currentTimer;
    private GameObject otherplayer;

  
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
        currentTimer += 1 * Time.deltaTime;

        if (currentTimer >= expireRate)
        {
            Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherplayer = other.gameObject;
            DealDamage(otherplayer);
        }

    }
    public void DealDamage(GameObject otherplayer)
    {
        otherplayer.GetComponent<player>().Health -= damageRate;
        Destroy(this.gameObject);
    }

}
