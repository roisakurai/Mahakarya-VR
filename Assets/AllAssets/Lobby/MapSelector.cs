using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    // Array untuk menyimpan nama-nama scene
    public string[] sceneNames;

    // Metode untuk mendapatkan nama scene berdasarkan indeks
    public string GetSceneName(int index)
    {
        // Periksa apakah indeks dalam rentang yang valid
        if(index >= 0 && index < sceneNames.Length)
        {
            return sceneNames[index];
        }
        else
        {
            return "Invalid index";
        }
    }

    // Metode untuk mencetak nama scene berdasarkan indeks
    public void PrintSceneName(int index)
    {
        string sceneName = GetSceneName(index);
        Debug.Log("Scene Name: " + sceneName);
    }

    // Metode untuk memuat scene berdasarkan indeks
    public void LoadScene(int index)
    {
        string sceneName = GetSceneName(index);
        if (sceneName != "Invalid index")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Invalid index provided, scene not loaded.");
        }
    }
}