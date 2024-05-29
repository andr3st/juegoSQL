using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager
using UnityEngine.UI;

public class barraResta : MonoBehaviour
{
    public Slider Barra;
    public float max = 200;
    public float res = 2;

    public Image imageChanger; // Referencia al componente Image en el GameObject
    public List<Sprite> images; // Lista de sprites para las diferentes imágenes

    public AudioSource alarmSound; // Referencia al componente AudioSource para la alarma

    private float intervalo;
    private bool alarmaActiva = false; // Variable para controlar el estado de la alarma
    private bool maxValueReached = false; // Variable para controlar si se ha alcanzado el valor máximo

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
            VerificarAlarma();
            VerificarCambioEscena(); // Verificar si es necesario cambiar de escena
        }
        else
        {
            VerificarCambioEscena(); // Verificar si es necesario cambiar de escena incluso si el valor es 0
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

    void VerificarAlarma()
    {
        if (Barra.value <= max / 3f)
        {
            if (!alarmaActiva)
            {
                alarmSound.Play(); // Reproduce el sonido de alarma
                alarmaActiva = true; // Activa la alarma
            }
        }
        else
        {
            if (alarmaActiva)
            {
                alarmSound.Stop(); // Detiene el sonido de alarma
                alarmaActiva = false; // Desactiva la alarma
            }
        }
    }

    void VerificarCambioEscena()
    {
        if (Barra.value <= 0)
        {
            SceneManager.LoadScene(3); // Cargar escena con índice 3 cuando el valor es mínimo
        }
        else if (Barra.value >= max && !maxValueReached)
        {
            SceneManager.LoadScene(2); // Cargar escena con índice 2 cuando el valor es máximo
            maxValueReached = true; // Indicar que el valor máximo ha sido alcanzado
        }
    }

    public void AumentarBarra(float cantidad)
    {
        if (Barra.value < max)
        {
            Barra.value = Mathf.Min(Barra.value + cantidad, max);
            ActualizarImagen();
            VerificarAlarma();
            VerificarCambioEscena(); // Verificar si es necesario cambiar de escena
        }
    }

    public void ReducirBarra(float cantidad)
    {
        if (Barra.value > 0)
        {
            Barra.value = Mathf.Max(Barra.value - cantidad, 0);
            ActualizarImagen();
            VerificarAlarma();
            VerificarCambioEscena(); // Verificar si es necesario cambiar de escena
        }
    }
}
