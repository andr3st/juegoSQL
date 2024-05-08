using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public InputField textConsola; // Referencia al InputField donde el usuario ingresa el texto
    private const string TEXTO_OBJETIVO = "SELECT * FROM productos WHERE precio < 20;"; // Texto objetivo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Si el usuario presiona Enter
        {
            string textoIngresado = textConsola.text; // Obtener el texto ingresado
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