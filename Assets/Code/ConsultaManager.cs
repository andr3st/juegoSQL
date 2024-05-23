using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class ConsoleManager : MonoBehaviour
{
    public TMP_InputField textConsola;
    public TextMeshProUGUI textConsulta;
    public TextMeshProUGUI textConsola2;
    public barraResta barraRestaScript; // Referencia al script barraResta
    public barraSuma barraSumaScript;   // Referencia al script barraSuma
    public SpriteRenderer consola1;
    public Image consola2;

    private int[] Numero;
    private  int NivelActual=1;
    private int Racha = 0;
    private int Chocolate=0;
    private int consultaActual = 0;
    private int contadorCorrectas = 0; 
    private int contadorIncorrectas = 0;

    private struct Consulta
    {
        public string consulta;
        public string IngresarTexto;

        public string RespuestaCorrecta;

        public Consulta(string consulta, string IngresarTexto, string RespuestaCorrecta)
        {
            this.consulta = consulta;
            this.IngresarTexto=IngresarTexto;
            this.RespuestaCorrecta = RespuestaCorrecta;
        }
    }

        private Consulta[] Nivel1= new Consulta[]
        {
            new Consulta("Para la creación de una base de dato utilizamos el comando:","","CREATE DATABASE"),
            new Consulta("Para la creación de tablas utilizamos el comando:","","CREATE TABLE"),
            new Consulta("Para la inserción de datos a una tabla utilizamos el comando:","","INSERT INTO"),
            new Consulta("Para visualizar todos los datos de una tabla utilizamos el comando:","","SELECT * FROM"),
            new Consulta("Palabra que se utiliza para visualizar datos que cumplan una condición:","","WHERE"),
            new Consulta("Palabra que se usa para unir 2 tablas:","","JOIN"),
            new Consulta("Comando que se utiliza para agrupar filas en una tabla en función del valor de una columna determinada:","","GROUP BY"),
            new Consulta("Función para devolver el nombre del mes:","","MONTHNAME()"),
            new Consulta("Palabra y función que se utiliza para realizar el conteo de los datos:","","COUNT(*)"),
            new Consulta("Palabra para asignar un nombre a una columna de la tabla de visualización:", "","AS")
        };

        private Consulta[] Nivel2=new Consulta[]
        {
            new Consulta("Para modificar los datos existentes de una tabla se utiliza la palabra:","","UPDATE"),
            new Consulta("Modificar el nombre de usuario  en la columna c_nombre de la tabla t_usuarios donde su correo es ale@gmail.com:","___ t_usuario __ c_nombre = “Alix” ___ c_correo = “ale@gmail.com”;","UPDATE t_usuario SET c_nombre = “Alix” WHERE c_correo = “ale@gmail.com”;"),
            new Consulta("Selecciona los primeros 3 usuarios de la tabla t_usuarios:","___  __ 3 FROM t_usuarios;","SELECT  TOP 3 FROM t_usuarios;"),
            new Consulta("Recupera la información de la tabla t_post y regresa la cantidad de publicaciones con la fecha del 23 de enero de 2023, la columna se llama dFecha:","____ count(dFecha) ___ t_post WHERE dFecha=_______;","SELECT count(dFecha) FROM t_post WHERE dFecha=”2023-01-23”;"),
            new Consulta("Ingresa las palabras faltantes del siguiente Query:","___ count(*) ___ Total_usuarios ___ t_usuarios;","SELECT count(*) AS Total_usuarios FROM t_usuarios;"),
            new Consulta("Ingresa las palabras faltantes del siguiente query:","SELECT cNickname FROM t_usuarios ___ cEmail ___  %gmail%;","SELECT cNickname FROM t_usuarios WHERE cEmail LIKE  %gmail%;"),
            new Consulta("Ingresa las palabras faltantes del siguiente Query:","SELECT c_usuario FROM t_usuarios ___ ____ (SELECT c_post FROM t_posts FROM t_usuarios.c_usuarioID=t_posts.c_usuarioID AND c_etiqueta= ”imagen”);","SELECT c_usuario FROM t_usuarios WHERE EXISTS (SELECT c_post FROM t_posts FROM t_usuarios.c_usuarioID=t_posts.c_usuarioID AND c_etiqueta= ”imagen”);"),
            new Consulta("Ingresa las palabras faltantes del siguiente query:","____ nUsuarioID, nCategoriasID, COUNT(*) ___ Total_posts ___ t_posts GROUP BY nCategoriasID, nUsuarioID ____ Total_posts>6 ____ __ Total_posts;","SELECT nUsuarioID, nCategoriasID, COUNT(*) AS Total_posts FROM t_posts GROUP BY nCategoriasID, nUsuarioID HAVING Total_posts>6 ORDER BY Total_posts;"),
            new Consulta("Ingresa las palabras faltantes del siguiente query:"," ___ _____ t_Cuentas ( [CuentaID] __ NOT NULL, [Saldo] _______ (10,2) NOT NULL;","CREATE TABLE t_Cuentas ( [CuentaID] INT NOT NULL, [Saldo] DECIMAL (10,2) NOT NULL;"),
            new Consulta("Escribe la sentencia para visualizar todos los datos de la tabla t_categorias:","", "SELECT * FROM t_categorias;")
        };

        private Consulta[] Nivel3 = new Consulta[]
        {
            new Consulta("Escribe la sentencia para borrar los datos de la tabla t_post que en su c_postID sean mayores a 10:","","DELETE FROM t_post WHERE c_postID>10;"),
            new Consulta("Sentencia para contar el número de post que ha hecho un usuario, teniendo en cuenta que la columna de post es c_post, y el nombre de usuario es c_usuario de la tabla t_posts:","","SELECT COUNT(c_post),c_usuario FROM t_post GROUP BY c_usuario;"),
            new Consulta("Mostrar id de Post que sean 11 o 18 de la tabla t_post, donde la columna del id post es c_id_post:","","SELECT c_id_post FROM t_post WHERE c_id_post=11 or c_id_post=18;"),
            new Consulta("Unir las tablas t_post y t_usuarios por medio de la columna c_usuarioID:","","SELECT * FROM t_post JOIN t_usuarios ON t_post.c_usuarioID= t_usuarios.c_usuarioID;"),
            new Consulta("Apellido Paterno más común de la tabla t_usuarios:","SELECT ___ _ c_apaterno AS Total, _____ FROM t_usuarios ____ __ ______ ____ __  Total ___;","SELECT TOP 1 c_apaterno AS Total, COUNT(*) FROM t_usuarios GROUP BY apaterno ORDER BY  Total DESC;"),
            new Consulta("Palabra que se utiliza para ejecutar un procedimiento almacenado:","","EXEC"),
            new Consulta("Crear un procedimiento almacenado que muestre toda la información de la tabla t_usuarios:","______ _______ MostrarInfo __ ____ _ ____ t_usuarios GO;","CREATE PROCEDURE MostrarInfo AS SELECT * FROM t_usuarios GO;"),
            new Consulta("Crear un procedimiento almacenado que inserte información de un usuario a la tabla t_usuarios:","___ ________ InsertarUsuario @nombre nvarchar(20), @apaterno nvarchar(20), @usuario nvarchar(20) __ ____ ___ t_usuarios (c_nombre, c_apaterno, c_usuario) ____ (@___, @____,@____) GO;","CREATE PROCEDURE InsertarUsuario @nombre nvarchar(20), @apaterno nvarchar(20), @usuario nvarchar(20) AS INSERT INTO t_usuarios (c_nombre, c_apaterno, c_usuario) VALUES (@nombre, @apaterno,@usuario) GO;"),
            new Consulta("Ejecución del procedimiento almacenado InsertarUsuario con valores de nombre=”ANA”, apaterno=”ORTIZ”, usuario=”AORTIZ”:","","EXEC InsertarUsuario (“ANA”,”ORTIZ”,”AORTIZ”);"),
            new Consulta("Crear una variable promedio y asignarle el valor 0:","","SET @promedio = 0")
        };
        void Start(){
            Numero = NumerosAleatorios(Nivel1.Length);
            MostrarConsulta(NivelActual,Numero[consultaActual]);
            
        }

        void Update(){
            if (Input.GetKeyDown(KeyCode.Return)) // Si el usuario presiona Enter
        {
            VerificarRespuesta(NivelActual,Numero[consultaActual]);
        }

        }

        void VerificarRespuesta(int NivelActual,int num){
            if(NivelActual==1){
                string textoIngresado = textConsola.text.Trim(); // Obtener y limpiar el texto ingresado
                if(textoIngresado==Nivel1[num].RespuestaCorrecta){
                    contadorCorrectas++;
                    barraRestaScript.AumentarBarra(10); // Aumenta la barra de resta en 10 si es correcto

                    if(Racha<3){
                        Racha++;
                        Debug.Log("RACHA= "+ Racha);
                    }
                    else if(Racha==3){
                        Racha=0;
                        Chocolate++;
                        Debug.Log("OBTUVISTE UN CHOCOLATE, CHOCOLATES= "+ Chocolate);
                        Debug.Log("RACHA= "+ Racha);
                    }

                    if(consultaActual<3){
                        consultaActual++;
                    }
                    else{
                        if(NivelActual<3){
                            NivelActual++;
                            consultaActual=0;
                            switch(NivelActual) {
                                case 2:
                                    Numero=NumerosAleatorios(Nivel2.Length);
                                    break;
                                case 3:
                                    Numero=NumerosAleatorios(Nivel3.Length);
                                    break;
                                default:
                                    Debug.Log("No existen más Niveles");
                                    break;
                                }
                        }
                        else{
                            //Se acabó el juego
                        }
                    }

                    MostrarConsulta(NivelActual,Numero[consultaActual]);
                }
                else{
                    //RESPUESTA INCORRECTA
                    //IMPLEMENTACIÓN DE SONIDO
                    barraRestaScript.ReducirBarra(10); // Reduce la barra de resta en 10 si es incorrecto
                    barraSumaScript.AumentarBarra(10); // Aumenta la barra de suma en 10 si es incorrecto
                    Racha=0;
                    contadorIncorrectas++;

                }
            }
        }

        void MostrarConsulta(int NivelActual,int num){
            if(NivelActual==1){
                Color color1 = new Color(255f,255f,255f,1f);
                consola1.color = color1;
                consola2.color = color1;
                textConsulta.text= Nivel1[num].consulta;
                textConsola.text= Nivel1[num].IngresarTexto;
                Debug.Log("Nivel 1 consulta: " + Nivel1[num].consulta);
                Debug.Log("Nivel 1 texto inicial: " + Nivel1[num].IngresarTexto);
                Debug.Log("Nivel 1 RESPUESTA: " + Nivel1[num].RespuestaCorrecta);
            }
            else if(NivelActual==2){
                Color color2 = new Color(0f,27f,178f,1f);
                consola1.color = color2;
                consola2.color = color2;
                textConsulta.text= Nivel2[num].consulta;
                textConsola.text= Nivel2[num].IngresarTexto;
                Debug.Log("Nivel 2 consulta: " + Nivel2[num].consulta);
                Debug.Log("Nivel 2 texto inicial: " + Nivel2[num].IngresarTexto);
                Debug.Log("Nivel 2 RESPUESTA: " + Nivel2[num].RespuestaCorrecta);
            }
            else{
                Color color2 = new Color(0f,0f,0f,1f);
                consola1.color = color2;
                consola2.color = color2;
                textConsola2.color = Color.green;
                textConsulta.text= Nivel3[num].consulta;
                textConsola.text= Nivel3[num].IngresarTexto;
            }
        }

        public static int[] NumerosAleatorios(int Length)
    {
        int[] Numeros = new int[Length];
        List<int> pool = new List<int>();
        for (int i = 0; i < Length; i++)
        {
            pool.Add(i);
        }

       System.Random rand = new System.Random();
        for (int i = 0; i < Length; i++)
        {
            int index = rand.Next(pool.Count);
            Numeros[i] = pool[index];
            pool.RemoveAt(index);
        }

        return Numeros;
    }

    
}
