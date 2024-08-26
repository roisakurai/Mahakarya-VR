using System.Collections;
using UnityEngine;

public class GameVRManager : MonoBehaviour
{
    public GameObject bossMushroom; // Objek bos jamur
    public GameObject[] kendis; // Array kendi
    public float destroyTime = 2f; // Waktu untuk menghancurkan kendi
    public float bossDestroyTime = 5f; // Waktu untuk menghancurkan bos jamur setelah kendi dihancurkan

    private int destroyedKendis = 0; // Jumlah kendi yang sudah dihancurkan

    void Start()
    {
        StartCoroutine(StartBossFight());
    }

    IEnumerator StartBossFight()
    {
        yield return new WaitForSeconds(3f); // Waktu tunda sebelum bos muncul

        bossMushroom.SetActive(true);

        // Menginisialisasi kendi
        foreach (GameObject kendi in kendis)
        {
            kendi.SetActive(true);
            kendi.GetComponent<KendiController>().OnKendiDestroyed += HandleKendiDestroyed;
        }
    }

    void HandleKendiDestroyed()
    {
        destroyedKendis++;

        if (destroyedKendis >= kendis.Length)
        {
            StartCoroutine(DestroyBoss());
        }
    }

    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(bossDestroyTime);

        bossMushroom.SetActive(false);
        // Tambahkan logika lainnya sesuai kebutuhan, misalnya menang permainan atau menampilkan layar kemenangan
    }
}

public class KendiController : MonoBehaviour
{
    public delegate void KendiDestroyed();
    public event KendiDestroyed OnKendiDestroyed;
    public float destroyTime = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, destroyTime);
            OnKendiDestroyed?.Invoke();
        }
    }
}
