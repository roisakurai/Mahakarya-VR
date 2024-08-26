using UnityEngine;

public class WalkSoundManager : MonoBehaviour
{
    public AudioClip suaraJalan; // Audio clip untuk suara jalan
    private AudioSource audioSource; // Komponen AudioSource

    private void Start()
    {
        // Mendapatkan komponen AudioSource pada objek ini
        audioSource = GetComponent<AudioSource>();

        // Menetapkan AudioClip ke AudioSource
        audioSource.clip = suaraJalan;
    }

    private void Update()
    {
        // Memeriksa apakah pemain bergerak (misalnya, menggunakan input horizontal)
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        // Jika ada input horizontal atau vertical, artinya pemain sedang bergerak
        if (Mathf.Abs(inputHorizontal) > 0.1f || Mathf.Abs(inputVertical) > 0.1f)
        {
            // Memeriksa apakah audio sedang tidak bermain
            if (!audioSource.isPlaying)
            {
                // Memulai pemutaran audio jalan
                audioSource.Play();
            }
        }
        else
        {
            // Jika tidak ada input, berhenti pemutaran audio
            audioSource.Stop();
        }
    }
}
