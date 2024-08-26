using UnityEngine;

public class BossMushroom : MonoBehaviour
{
    public int kendisToDestroy = 4; // Jumlah kendi yang harus dihancurkan
    public GameObject[] kendis; // Array kendi yang harus dihancurkan
    public GameObject bossJamur; // Objek bos jamur

    private bool bossActive = false; // Status bos jamur (aktif/tidak aktif)

    private void Start()
    {
        // Pastikan bos jamur dimatikan pada awal permainan
        bossJamur.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jika objek masuk ke collider bos jamur
        if (other.CompareTag("Player"))
        {
            bossActive = true;
            bossJamur.SetActive(true);
        }
    }

    // Fungsi untuk menghancurkan kendi
    public void DestroyKendi(GameObject kendi)
    {
        // Hancurkan kendi yang diberikan
        Destroy(kendi);

        // Kurangi jumlah kendi yang harus dihancurkan
        kendisToDestroy--;

        // Cek apakah semua kendi telah dihancurkan
        if (kendisToDestroy <= 0)
        {
            BossDefeated();
        }
    }

    // Aksi yang terjadi ketika boss telah dikalahkan
    private void BossDefeated()
    {
        // Lakukan tindakan setelah boss dikalahkan, misalnya menampilkan pesan kemenangan
        Debug.Log("Boss Jamur telah dikalahkan!");
    }
}
