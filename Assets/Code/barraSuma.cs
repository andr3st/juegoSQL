using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraSuma : MonoBehaviour
{
    Slider Barra;
    public float max = 200;
    public float sum = 2;

    public Image imageChanger; // Referencia al componente Image en el GameObject
    public List<Sprite> images; // Lista de sprites para las diferentes imágenes

    public Image emojiChanger; // Referencia al componente Image en el GameObject
    public List<Sprite> emojis; // Lista de sprites para las diferentes imágenes

    public AudioSource alertSound; // Referencia al componente AudioSource para la alerta

    private float intervalo;
    private bool alertaActiva = false; // Variable para controlar el estado de la alerta

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
            ActualizarEmoji();
            VerificarAlerta();
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

    void ActualizarEmoji()
    {
        // Determina el índice de la imagen en función del valor del slider
        int index = Mathf.FloorToInt(Barra.value / intervalo);

        // Ajusta el índice para asegurarse de que la última imagen se muestre en el valor mínimo
        if (Barra.value <= max * 0.5f)
        {
            index = 0; // Primera imagen
        }
        else if (Barra.value <= max * 0.75f)
        {
            index = 1; // Segunda imagen
        }
        else
        {
            index = 2; // Tercera imagen
        }

        // Asegúrate de que el índice esté dentro de los límites de la lista
        if (index >= 0 && index < emojis.Count)
        {
            emojiChanger.sprite = emojis[index];
        }
    }

    void VerificarAlerta()
    {
        if (Barra.value >= max * 2 / 3)
        {
            if (!alertaActiva)
            {
                alertSound.Play(); // Reproduce el sonido de alerta
                alertaActiva = true; // Activa la alerta
            }
        }
        else
        {
            if (alertaActiva)
            {
                alertSound.Stop(); // Detiene el sonido de alerta
                alertaActiva = false; // Desactiva la alerta
            }
        }
    }

    public void AumentarBarra(float cantidad)
    {
        Barra.value = Mathf.Min(Barra.value + cantidad, max);
        ActualizarImagen();
        ActualizarEmoji();
        VerificarAlerta();
    }

    public void ReducirBarra(float cantidad)
    {
        Barra.value = Mathf.Max(Barra.value - cantidad, 0);
        ActualizarImagen();
        ActualizarEmoji();
        VerificarAlerta();
    }
}
