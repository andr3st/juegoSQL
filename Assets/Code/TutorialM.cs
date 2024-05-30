using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject Siguiente; // Botón siguiente
    public GameObject Anterior; // Botón anterior
    public GameObject Texto; // GameObject que contiene el texto
    private int ContadorTexto = 0; // Contador del texto actual
    private string[] Sentencias = new string[]{
        "Bienvenido a SQL. A continuación te enseñaremos cómo se juega",
        "La barra de estrés aumentará si la respuesta es incorrecta",
        "La barra de suma aumentará si la respuesta es correcta",
        "Sigue respondiendo correctamente para ganar chocolates",
        "¡Buena suerte y diviértete!"
    };

    public void CambiarTexto(string direccion)
    {
        TextMeshProUGUI Info = Texto.GetComponent<TextMeshProUGUI>();
        Button bAnterior = Anterior.GetComponent<Button>();
        Button bSiguiente = Siguiente.GetComponent<Button>();

        if (direccion == "Anterior")
        {
            ContadorTexto--;
        }
        else if (direccion == "Siguiente")
        {
            ContadorTexto++;
        }

        ContadorTexto = Mathf.Clamp(ContadorTexto, 0, Sentencias.Length - 1);

        if (ContadorTexto == 0)
        {
            bAnterior.interactable = false;
        }
        else
        {
            bAnterior.interactable = true;
        }

        if (ContadorTexto == Sentencias.Length - 1)
        {
            bSiguiente.interactable = false;
        }
        else
        {
            bSiguiente.interactable = true;
        }

        Info.text = Sentencias[ContadorTexto];
    }

    void Start()
    {
        Button bAnterior = Anterior.GetComponent<Button>();
        bAnterior.interactable = false;

        TextMeshProUGUI Info = Texto.GetComponent<TextMeshProUGUI>();
        Info.text = Sentencias[ContadorTexto];

        Button bSiguiente = Siguiente.GetComponent<Button>();
        if (Sentencias.Length <= 1)
        {
            bSiguiente.interactable = false;
        }
    }

    // Update is called once per frame (si se necesita)
    void Update()
    {
        // Aquí podrías manejar cualquier lógica adicional que necesites para el tutorial
    }
}
