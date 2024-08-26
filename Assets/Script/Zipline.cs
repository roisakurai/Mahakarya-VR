using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    public float Go = 100f;
    public float range = 3f;
    public GameObject ZiplineFloor;
    public bool isMoving = false;
    public Camera fpsCam;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                StartCoroutine(ZiplineGo());
            }
        }
    }

    IEnumerator ZiplineGo()
    {
        isMoving = true;
        ZiplineFloor.GetComponent<Animator>().Play("zipline");
        yield return new WaitForSeconds(0.05f);

        // Set player position to zipline floor position
        player.transform.position = ZiplineFloor.transform.position;

        // Disable player gravity
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.useGravity = false;
        }

        yield return new WaitForSeconds(10f);
        ZiplineFloor.GetComponent<Animator>().Play("New State");
        isMoving = false;

        // Re-enable player gravity after zipline
        if (playerRigidbody != null)
        {
            playerRigidbody.useGravity = true;
        }
    }

    // When player collides with zipline floor
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set player position to zipline floor position
            player.transform.position = ZiplineFloor.transform.position;

            // Disable player gravity
            Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.useGravity = false;
            }
        }
    }
}