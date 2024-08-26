using UnityEngine;

public class Kendi : MonoBehaviour
{
    public GameMechanics gameMechanics;

    private void Start()
    {
        gameMechanics = GameObject.FindObjectOfType<GameMechanics>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Hancurkan kendi
            Destroy(gameObject);
            // Kurangi jumlah kendi yang harus dihancurkan
            gameMechanics.HancurkanKendi();
        }
    }
}
