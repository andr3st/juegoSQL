using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraSuma : MonoBehaviour
{
    Slider Barra;
    public float max = 200;
    public float sum = 1;

    public Image imageChanger; // Referencia al componente Image en el GameObject
    public List<Sprite> images; // Lista de sprites para las diferentes imágenes

    private float intervalo;

    void Awake()
    {
        Barra = GetComponent<Slider>();
        Barra.value = 0; // Asegurarse de que la barra inicie en 0
        intervalo = max / (float)(images.Count - 1);
        InvokeRepeating("ActualizarValorBarra", 0f, 1f); // Llamar a ActualizarValorBarra cada segundo
    }

    void ActualizarValorBarra()
    {
        if (Barra.value < max) // Asegurarse de que no exceda el valor máximo
        {
            Barra.value += sum; // Aumentar el valor de la barra
            ActualizarImagen();
        }
    }

    void ActualizarImagen()
    {
        int index = Mathf.FloorToInt(Barra.value / intervalo);

        // Asegúrate de que la última imagen solo se muestre cuando se alcance el valor máximo
        if (Barra.value >= max)
        {
            index = images.Count - 1;
        }

        if (index >= 0 && index < images.Count)
        {
            imageChanger.sprite = images[index];
        }
    }

    public void AumentarBarra(float cantidad)
    {
        Barra.value = Mathf.Min(Barra.value + cantidad, max);
        ActualizarImagen();
    }
}
