using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;




public class player : NetworkBehaviour
{
    public float movingspeed;
    public GameObject playerCamera;
    public Transform bulletSpawnPoints;
    public GameObject bullet;
    public float Health;
    private bool triggeringAnotherPlayer;
    private GameObject otherPlayer;


    void Start()
    {
        if (isLocalPlayer == true)
        {
            playerCamera.GetComponent<Camera>().enabled = true;
            this.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        }
        else
        {
            playerCamera.GetComponent<Camera>().enabled = false;
            this.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        }
    }

    void Update() {

        if (isLocalPlayer == true) {

            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * movingspeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * movingspeed);
            }

            if (triggeringAnotherPlayer && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(otherPlayer);
            }
            if (Input.GetMouseButtonDown(0)) ;
            {
                CmdShoot();
            }
        }
    }

    [Command]

    void CmdShoot()
    {
      GameObject _bullet = (GameObject)Instantiate(bullet.gameObject, bulletSpawnPoints.transform.position, Quaternion.identity);
        _bullet.transform.rotation = bulletSpawnPoints.transform.rotation;
      NetworkServer.SpawnWithClientAuthority(_bullet, connectionToClient);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
       {
            triggeringAnotherPlayer = true;
            otherPlayer = other.gameObject;
       }
    }

    void OnTriggerExit(Collider Other)
    {
        if(Other.tag == "Player")
        {
            triggeringAnotherPlayer = true;
            otherPlayer = null;

        }

    }


  

}
