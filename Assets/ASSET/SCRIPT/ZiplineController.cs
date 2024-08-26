using UnityEngine;

public class ZiplineController : MonoBehaviour
{
    public Transform EndPosition;
    public GameObject ObjectToMove;
    public float MoveSpeed = 1f;

    private bool isMoving = false;
    private Vector3 startPosition;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        // Set start position awal sesuai dengan posisi awal ObjectToMove
        startPosition = ObjectToMove.transform.position;

        // Hitung panjang perjalanan (jarak antara start dan end)
        journeyLength = Vector3.Distance(startPosition, EndPosition.position);
    }

    void Update()
    {
        if (isMoving)
        {
            // Hitung perpindahan posisi berdasarkan waktu
            float distCovered = (Time.time - startTime) * MoveSpeed;
            float fracJourney = distCovered / journeyLength;

            // Pindahkan objek ke posisi antara start dan end
            ObjectToMove.transform.position = Vector3.Lerp(startPosition, EndPosition.position, fracJourney);

            // Jika objek telah mencapai posisi end, hentikan pergerakan
            if (ObjectToMove.transform.position == EndPosition.position)
            {
                isMoving = false;
            }
        }
    }

    public void Zipline()
    {
        // Reset waktu pergerakan dan mulai pergerakan
        startTime = Time.time;
        isMoving = true;

        // Set start position sesuai dengan posisi objek terakhir
        startPosition = ObjectToMove.transform.position;

        // Hitung ulang panjang perjalanan (jarak antara start dan end)
        journeyLength = Vector3.Distance(startPosition, EndPosition.position);
    }
}
