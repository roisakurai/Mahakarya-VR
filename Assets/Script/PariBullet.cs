using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PariBullet : MonoBehaviour
{
    public float speed = 5f; // Kecepatan jatuh peluru
    public float lifetime = 10f; // Waktu hidup peluru sebelum dihancurkan otomatis
    public GameObject explosionPrefab; // Prefab ledakan
    public float explosionDuration = 1f; // Durasi ledakan sebelum dihancurkan
    public int damage = 10; // Besar damage yang diberikan ke player

    private bool hasExploded = false; // Menyimpan status apakah sudah meledak

    private void Start()
    {
        // Hancurkan peluru setelah waktu tertentu jika tidak terkena apapun
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Gerakkan peluru ke bawah setiap frame
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Water")) // Jika peluru mengenai tanah atau air dan belum meledak
            {
                Explode();
            }
            else if (collision.gameObject.CompareTag("Player")) // Jika peluru mengenai player
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); // Berikan damage ke player
                }
                Explode(); // Meledak setelah memberikan damage
            }
        }
    }

    private void Explode()
    {
        hasExploded = true; // Tandai bahwa peluru sudah meledak

        // Nonaktifkan collider peluru untuk mencegah tabrakan lebih lanjut
        GetComponent<Collider>().enabled = false;

        // Buat instance ledakan pada posisi peluru
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

        // Hancurkan peluru
        Destroy(gameObject);

        // Hancurkan ledakan setelah durasi tertentu
        Destroy(explosion, explosionDuration);
    }
}
