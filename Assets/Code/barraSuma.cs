using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barra : MonoBehaviour
{
    Slider Barra;
    public float max;
    public float sum;

    void Awake()
    {
        Barra = GetComponent<Slider>();
        Barra.value = 0; // Asegurarse de que la barra inicie en 0
        InvokeRepeating("ActualizarValorBarra", 0f, 1f); // Llamar a ActualizarValorBarra cada segundo
    }

    void ActualizarValorBarra()
    {
        if (Barra.value < max) // Asegurarse de que no exceda el valor máximo
        {
            Barra.value += sum; // Aumentar el valor de la barra
        }
    }
}
