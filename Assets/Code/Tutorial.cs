using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject Pantalla;
    public GameObject Siguiente;
    public GameObject Anterior;
    public GameObject Inicio;
    public GameObject tSiguiente;
    public GameObject tAnterior;
    public GameObject tInicio;
    public Sprite[] Imagenes;

    private int ContadorImagen = 0;
    private Image pantallaImagen;
    private Button botonSiguiente;
    private Button botonAnterior;
    private Button botonInicio;
    private Image ImagenSiguiente;
    private Image ImagenAnterior;
    private Image ImagenInicio;
    private TextMeshProUGUI InfoSiguiente;
    private TextMeshProUGUI InfoAnterior;
    private TextMeshProUGUI InfoInicio;

    void Start()
    {
        // Inicializa los componentes
        pantallaImagen = Pantalla.GetComponent<Image>();
        botonSiguiente = Siguiente.GetComponent<Button>();
        botonAnterior = Anterior.GetComponent<Button>();
        botonInicio = Inicio.GetComponent<Button>();
        ImagenSiguiente = Siguiente.GetComponent<Image>();
        ImagenAnterior = Anterior.GetComponent<Image>();
        ImagenInicio = Inicio.GetComponent<Image>();
        InfoSiguiente = tSiguiente.GetComponent<TextMeshProUGUI>();
        InfoAnterior = tAnterior.GetComponent<TextMeshProUGUI>();
        InfoInicio = tInicio.GetComponent<TextMeshProUGUI>();
                
        // Configura la imagen inicial y los botones
        if (Imagenes.Length > 0)
        {
            pantallaImagen.sprite = Imagenes[ContadorImagen];
        }
        ActualizarBotones();
    }

    public void cambiarImagenSiguiente()
    {
        if (ContadorImagen < Imagenes.Length - 1)
        {
            ContadorImagen++;
            ActualizarBotones();
            pantallaImagen.sprite = Imagenes[ContadorImagen];
        }
    }

    public void cambiarImagenAnterior()
    {
        if (ContadorImagen > 0)
        {
            ContadorImagen--;
            ActualizarBotones();
            pantallaImagen.sprite = Imagenes[ContadorImagen];
        }
    }

    private void ActualizarBotones()
    {
        botonSiguiente.interactable = ContadorImagen < Imagenes.Length - 1;
        botonAnterior.interactable = ContadorImagen > 0;
        botonInicio.interactable = ContadorImagen == Imagenes.Length -1;
        Color colorS = ImagenSiguiente.color;
        Color colorA = ImagenAnterior.color;
        Color colorI =ImagenInicio.color;
        
        if(!botonSiguiente.interactable){
            colorS.a = 0;
            ImagenSiguiente.color = colorS;
            InfoSiguiente.color = colorS;
        }
        else{
            colorS.a = 1;
            ImagenSiguiente.color = colorS;
            InfoSiguiente.color = colorS;
        }
        if(!botonAnterior.interactable){
            colorA.a=0;
            ImagenAnterior.color = colorA;
            InfoAnterior.color = colorA;
        }
        else{
            colorA.a=1;
            ImagenAnterior.color = colorA;
            InfoAnterior.color = colorA;
        }
        if(!botonInicio.interactable){
            colorI.a=0;
            ImagenInicio.color = colorI;
            InfoInicio.color = colorI;
        }
        else{
            colorI.a=1;
            ImagenInicio.color = colorI;
            InfoInicio.color = colorI;
        }
        

        
    }
}
