using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraResta : MonoBehaviour
{
    Slider Barra;
    public float max;
    public float res;

    void Awake()
    {
        Barra = GetComponent<Slider>();
        Barra.value = max;
        InvokeRepeating("ActualizarValorBarra", 0f, 1f);
    }

    void ActualizarValorBarra()
    {
        if (Barra.value > 0)
        {
            Barra.value -= res;
        }
    }
}
