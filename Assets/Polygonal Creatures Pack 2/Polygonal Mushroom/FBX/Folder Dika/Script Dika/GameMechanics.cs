using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    public GameObject bossMushroom;
    public GameObject[] kendis;
    public int kendisToDestroy = 4;

    private void Start()
    {
        // Inisialisasi kendi
        kendis = new GameObject[kendisToDestroy];
        for (int i = 0; i < kendisToDestroy; i++)
        {
            kendis[i] = Instantiate(Resources.Load("KendiPrefab")) as GameObject;
            // Tempatkan kendi di posisi yang diinginkan
            kendis[i].transform.position = new Vector3(i * 2, 0, 0);
        }

        // Inisialisasi bos jamur
        bossMushroom = Instantiate(Resources.Load("BossMushroomPrefab")) as GameObject;
        // Tempatkan bos jamur di posisi yang diinginkan
        bossMushroom.transform.position = new Vector3(0, 5, 0);
    }

    private void Update()
    {
        // Cek apakah semua kendi telah dihancurkan
        if (kendisToDestroy <= 0)
        {
            // Pemain berhasil mengalahkan bos jamur
            Debug.Log("Anda Menang!");
        }
    }

    // Dipanggil ketika pemain menghancurkan kendi
    public void HancurkanKendi()
    {
        kendisToDestroy--;
    }
}
