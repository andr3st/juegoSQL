using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    public TMP_InputField textConsola;
    public TextMeshProUGUI textConsulta;
    public barraResta barraRestaScript; // Referencia al script barraResta
    public barraSuma barraSumaScript;   // Referencia al script barraSuma

    private int consultaActual = 0;
    private int contadorCorrectas = 0; // Contador de respuestas correctas consecutivas

    private struct Consulta
    {
        public string consulta;
        public string respuestaCorrecta;

        public Consulta(string consulta, string respuestaCorrecta)
        {
            this.consulta = consulta;
            this.respuestaCorrecta = respuestaCorrecta;
        }
    }

    private Consulta[] consultas = new Consulta[]
    {
        new Consulta("Tengo una tabla de 'productos' con nombres y precios. ¿Puedes mostrarme todos los productos que cuesten menos de $20?", "SELECT * FROM productos WHERE precio < 20;"),
        new Consulta("Tengo una tabla de 'clientes' con nombres y edades. ¿Puedes mostrarme todos los clientes mayores de 30 años?", "SELECT * FROM clientes WHERE edad > 30;")
        // Añade más consultas aquí
    };

    void Start()
    {
        MostrarConsulta();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Si el usuario presiona Enter
        {
            VerificarRespuesta();
        }
    }

    void MostrarConsulta()
    {
        if (consultaActual < consultas.Length)
        {
            textConsulta.text = consultas[consultaActual].consulta;
            textConsola.text = "";
        }
        else
        {
            textConsulta.text = "Has completado todas las consultas.";
            textConsola.gameObject.SetActive(false);
        }
    }

    public void VerificarRespuesta()
    {
        string textoIngresado = textConsola.text.Trim(); // Obtener y limpiar el texto ingresado
        Debug.Log($"Texto ingresado: '{textoIngresado}'");

        // Log de cada carácter ingresado y su código ASCII
        foreach (char c in textoIngresado)
        {
            Debug.Log($"Carácter: '{c}' ASCII: {(int)c}");
        }

        if (consultaActual < consultas.Length)
        {
            if (textoIngresado == consultas[consultaActual].respuestaCorrecta)
            {
                Debug.Log("El texto ingresado es correcto.");
                barraRestaScript.AumentarBarra(10); // Aumenta la barra de resta en 10 si es correcto
                contadorCorrectas++; // Incrementa el contador de respuestas correctas
                Debug.Log("Racha: "+contadorCorrectas);
            }
            else
            {
                Debug.Log("El texto ingresado es incorrecto.");
                barraRestaScript.ReducirBarra(10); // Reduce la barra de resta en 10 si es incorrecto
                barraSumaScript.AumentarBarra(10); // Aumenta la barra de suma en 10 si es incorrecto
                contadorCorrectas = 0; // Reinicia el contador si la respuesta es incorrecta
                Debug.Log("Racha: "+contadorCorrectas);
            }

            consultaActual++;
            MostrarConsulta();
        }
    }
}
