using UnityEngine;

public class CheckAndToggleScriptsToraja : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;

    public MonoBehaviour scriptToDisable, scriptToDisable2, scriptToDisable3;
    public MonoBehaviour scriptToEnable, scriptToEnable2, scriptToEnable3;

    void Update()
    {
        // Periksa jika kedua game objek aktif
        if (object1 != null && object2 != null && object1.activeSelf && object2.activeSelf)
        {
            // Matikan skripToDisable jika tidak null
            if (scriptToDisable != null)
            {
                scriptToDisable.enabled = false;
                scriptToDisable2.enabled = false;
                scriptToDisable3.enabled = false;
            }

            // Nyalakan skripToEnable jika tidak null
            if (scriptToEnable != null)
            {
                scriptToEnable.enabled = true;
                scriptToEnable2.enabled = true;
                scriptToEnable3.enabled = true;
            }

            // Matikan skrip ini jika kondisi terpenuhi
            enabled = false;
        }
    }
}
