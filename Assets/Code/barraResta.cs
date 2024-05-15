using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraResta : MonoBehaviour
{
    public Slider Barra;
    public float max = 200;
    public float res = 1;

    public Image imageChanger; // Referencia al componente Image en el GameObject
    public List<Sprite> images; // Lista de sprites para las diferentes imágenes

    private float intervalo;

    void Awake()
    {
        Barra = GetComponent<Slider>();
        Barra.value = max; // Asegurarse de que la barra inicie en su valor máximo
        intervalo = max / 3f; // Calcular el intervalo para cada imagen (200 / 3)
        InvokeRepeating("ActualizarValorBarra", 0f, 1f); // Llamar a ActualizarValorBarra cada segundo
    }

    void ActualizarValorBarra()
    {
        if (Barra.value > 0) // Asegurarse de que la barra no sea menor que 0
        {
            Barra.value -= res; // Reducir el valor de la barra
            ActualizarImagen();
        }
    }

    void ActualizarImagen()
    {
        // Determina el índice de la imagen en función del valor del slider
        int index = Mathf.FloorToInt(Barra.value / intervalo);

        // Ajusta el índice para asegurarse de que la última imagen se muestre en el valor mínimo
        if (Barra.value <= intervalo)
        {
            index = 2; // Tercera imagen
        }
        else if (Barra.value <= intervalo * 2)
        {
            index = 1; // Segunda imagen
        }
        else
        {
            index = 0; // Primera imagen
        }

        // Asegúrate de que el índice esté dentro de los límites de la lista
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

    public void ReducirBarra(float cantidad)
    {
        Barra.value = Mathf.Max(Barra.value - cantidad, 0);
        ActualizarImagen();
    }
}
