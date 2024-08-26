using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 10f; // Gaya loncat

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mendapatkan input keyboard horizontal (A, D) untuk pergerakan lateral (kiri dan kanan)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Mendapatkan input keyboard vertical (W, S) untuk pergerakan maju-mundur
        float verticalInput = Input.GetAxis("Vertical");

        // Mendapatkan arah pandang global ke depan dan memproyeksikan ke bidang horizontal
        Vector3 forwardDirection = Vector3.forward;
        forwardDirection.y = 0f; // Mengabaikan komponen y
        forwardDirection = forwardDirection.normalized;

        // Mendapatkan arah pandang lateral (kiri dan kanan) dengan menggunakan cross product
        Vector3 lateralDirection = Vector3.Cross(Vector3.up, forwardDirection).normalized;

        // Menghitung vektor gerakan berdasarkan input
        Vector3 moveDirection = (lateralDirection * horizontalInput + forwardDirection * verticalInput).normalized;

        // Menghitung total pergerakan
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // Menggerakkan pemain
        transform.Translate(moveAmount);

        // Rotasi objek pemain jika tombol kanan mouse ditekan
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, mouseX);
        }

        // Memeriksa input untuk loncat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Menerapkan gaya loncat ke rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
