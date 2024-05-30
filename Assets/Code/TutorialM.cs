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
    public GameObject Texto;
    public Sprite[] Imagenes;
    private int ContadorImagen=0;
    private string[] Sentencias = new string[]{
        "Bienvenido a SQL. A continuación te enseñaremos cómo se juega", "Esta es la segunda Imagen",
    };
    public void cambiarImagen(GameObject b){
        TextMeshProUGUI Info= Texto.GetComponent<TextMeshProUGUI>();
        Image Imagen=Pantalla.GetComponent<Image>();
        Button bAnterior= Anterior.GetComponent<Button>();
        Button bSiguiente= Siguiente.GetComponent<Button>();
        if(ContadorImagen==0){
            if(b.tag=="Anterior"){
                bAnterior.interactable=false;
            }
            else{
                ContadorImagen++;
            }
        }
        else if(ContadorImagen>0 && ContadorImagen<Sentencias.Length){
            if(b.tag=="Anterior"){
                ContadorImagen--;
                bAnterior.interactable=true;
            }
            else{
                ContadorImagen++;
                bSiguiente.interactable=true;
            }
        }
        else{
            if(b.tag=="Anterior"){
                ContadorImagen--;
            }
            else{
                bSiguiente.interactable=false;
            }
        }
        Imagen.sprite=Imagenes[ContadorImagen];
        Info.text=Sentencias[ContadorImagen];
    }
    //Hacer un arreglo con texto
    // Start is called before the first frame update
    void Start()
    {
        Button bAnterior= Anterior.GetComponent<Button>();
        bAnterior.interactable= false;

        Image Imagen=Pantalla.GetComponent<Image>();
        Imagen.sprite=Imagenes[ContadorImagen];
        
        TextMeshProUGUI Info= Texto.GetComponent<TextMeshProUGUI>();
        Info.text=Sentencias[ContadorImagen];

    }

    // Update is called once per frame
}


