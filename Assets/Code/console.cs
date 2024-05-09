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
            string textoIngresado = textConsola.text.Trim(); // Obtener y limpiar el texto ingresado
            Debug.Log($"Texto ingresado: '{textoIngresado}'");

            // Log de cada carácter ingresado y su código ASCII
            foreach (char c in textoIngresado)
            {
                Debug.Log($"Carácter: '{c}' ASCII: {(int)c}");
            }

            if (textoIngresado == TEXTO_OBJETIVO)
            {
                Debug.Log("El texto ingresado es correcto.");
            }
            else
            {
                Debug.Log("El texto ingresado es incorrecto.");
            }
            textConsola.text = ""; // Limpiar el campo de texto
        }
    }
}
