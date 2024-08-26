using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class swipe_menu : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    public GameObject[] maps; // Array untuk menyimpan game object map
    public GameObject MapSelector; // Referensi ke game object MapSelector
    int selectedMapIndex = 0; // Indeks map yang dipilih
    float[]pos;
    

    //Variable tombol
    int posisi = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Function Button Slide
    public void next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scroll_pos = pos[posisi];
            selectedMapIndex = posisi; // Mengupdate indeks map yang dipilih
        }
    }

    public void prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scroll_pos = pos[posisi];
            selectedMapIndex = posisi; // Mengupdate indeks map yang dipilih
        }
    }

    public void SelectMap()
    {
        // Menjalankan aksi yang diinginkan saat map dipilih dengan tombol space
        Debug.Log("Map selected: " + maps[selectedMapIndex].name);
        // Tambahkan kode di sini untuk menjalankan aksi yang diinginkan saat map dipilih

        // Mengaktifkan game object map yang dipilih dan menonaktifkan yang lainnya
        for (int i = 0; i < maps.Length; i++)
        {
            if (i == selectedMapIndex)
            {
                maps[i].SetActive(true);
            }
            else
            {
                maps[i].SetActive(false);
            }
        }

        // Menonaktifkan game object MapSelector
        MapSelector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for(int i = 0; i < pos.Length; i++) {
            pos [i] = distance * i;
        } 
        if (Input.GetMouseButton(0)) {
            scroll_pos = scrollbar.GetComponent<Scrollbar> ().value;
        } else {
            for (int i = 0; i < pos.Length; i++) {
                if (scroll_pos < pos [i] + (distance / 2) && scroll_pos > pos [i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar> ().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar> ().value, pos [i], 0.05f);
                    posisi = i;
                }
            }
        }

        for (int i = 0; i < pos.Length; i++) {
            if (scroll_pos < pos [i] + (distance / 2) && scroll_pos > pos [i] - (distance / 2)) {
                transform.GetChild (i).localScale = Vector2.Lerp (transform.GetChild(i).localScale, new Vector2(1f,1f), 0.05f);
                for (int a = 0; a < pos.Length; a++){
                    if (a != i) {
                        transform.GetChild (a).localScale = Vector2.Lerp (transform.GetChild(a).localScale, new Vector2(0.8f,0.8f), 0.05f);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            prev();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            next();
        }

        // Mendeteksi tombol space
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectMap();
        }
        
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        // Kode untuk kembali ke scene MainMenu
        SceneManager.LoadScene("MainMenu"); // Pastikan bahwa Anda sudah mengimpor namespace UnityEngine.SceneManagement
    }
}