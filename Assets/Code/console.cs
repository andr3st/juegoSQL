using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Console : MonoBehaviour
{
    public TMP_InputField textConsola;
    private const string TEXTO_OBJETIVO = "SELECT * FROM productos WHERE precio < 20;";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Si el usuario presiona Enter
        {
           
            textConsola.text = ""; // Limpiar el campo de texto
        }
    }
}
