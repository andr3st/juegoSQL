using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ConsoleManager : MonoBehaviour
{
    public TMP_InputField textConsola;
    public TextMeshProUGUI textConsulta;
    public barraResta barraRestaScript; // Referencia al script barraResta
    public barraSuma barraSumaScript;   // Referencia al script barraSuma
    public ChocolateCounter chocolateCounter; // Referencia al script ChocolateCounter

    private int consultaActual = 0;
    private int contadorCorrectas = 0; // Contador de respuestas correctas consecutivas
    private bool mostrarMensajeChocolate = false; // Bandera para el mensaje de chocolate

    private struct Consulta
    {
        public int nivel;
        public string consulta;
        public string respuestaCorrecta;

        public Consulta(int nivel, string consulta, string respuestaCorrecta)
        {
            this.nivel = nivel;
            this.consulta = consulta;
            this.respuestaCorrecta = respuestaCorrecta;
        }
    }

    private List<Consulta> preguntasDelJuego;

    private Consulta[] consultas = new Consulta[]
    {
        new Consulta(1, "Para la creación de una base de dato utilizamos el comando:", "CREATE DATABASE"),
        new Consulta(1, "Para la creación de tablas utilizamos el comando:", "CREATE TABLE"),
        new Consulta(1, "Para la inserción de datos a una tabla utilizamos el comando:", "INSERT INTO"),
        new Consulta(1, "Para visualizar todos los datos de una tabla utilizamos el comando:", "SELECT * FROM"),
        new Consulta(1, "Palabra que se utiliza para visualizar datos que cumplan una condición:", "WHERE"),
        new Consulta(1, "Palabra que se usa para unir 2 tablas:", "JOIN"),
        new Consulta(1, "Comando que se utiliza para agrupar filas en una tabla en función del valor de una columna determinada:", "GROUP BY"),
        new Consulta(1, "Función para devolver el nombre del mes:", "MONTHNAME()"),
        new Consulta(1, "Palabra y función que se utiliza para realizar el conteo de los datos:", "COUNT(*)"),
        new Consulta(1, "Palabra para asignar un nombre a una columna de la tabla de visualización:", "AS"),

        new Consulta(2, "Para modificar los datos existentes de una tabla se utiliza la palabra:", "UPDATE"),
        new Consulta(2, "Modificar el nombre de usuario en la columna c_nombre de la tabla t_usuarios donde su correo es ale@gmail.com (usa UPDATE):", "UPDATE t_usuario SET c_nombre = “Alix” WHERE c_correo = “ale@gmail.com”;"),
        new Consulta(2, "Selecciona los primeros 3 usuarios de la tabla t_usuarios (usa SELECT TOP):", "SELECT TOP 3 FROM t_usuarios;"),
        new Consulta(2, "Recupera la información de la tabla t_post y regresa la cantidad de publicaciones con la fecha del 23 de enero de 2023, la columna se llama dFecha (usa COUNT):", "SELECT count(dFecha) FROM t_post WHERE dFecha=”2023-01-23”;"),
        new Consulta(2, "Ingresa las palabras faltantes del siguiente Query: SELECT count(*) AS _____ FROM t_usuarios:", "SELECT count(*) AS Total_usuarios FROM t_usuarios;"),
        new Consulta(2, "Ingresa las palabras faltantes del siguiente query: SELECT cNickname FROM t_usuarios WHERE cEmail LIKE %gmail%;", "SELECT cNickname FROM t_usuarios WHERE cEmail LIKE %gmail%;"),
        new Consulta(2, "Ingresa las palabras faltantes del siguiente Query: SELECT c_usuario FROM t_usuarios WHERE EXISTS (SELECT c_post FROM t_posts FROM t_usuarios.c_usuarioID=t_posts.c_usuarioID AND c_etiqueta= ”imagen”);", "SELECT c_usuario FROM t_usuarios WHERE EXISTS (SELECT c_post FROM t_posts FROM t_usuarios.c_usuarioID=t_posts.c_usuarioID AND c_etiqueta= ”imagen”);"),
        new Consulta(2, "Ingresa las palabras faltantes del siguiente query: SELECT nUsuarioID, nCategoriasID, COUNT(*) AS Total_posts FROM t_posts GROUP BY nCategoriasID, nUsuarioID HAVING Total_posts>6 ORDER BY Total_posts;", "SELECT nUsuarioID, nCategoriasID, COUNT(*) AS Total_posts FROM t_posts GROUP BY nCategoriasID, nUsuarioID HAVING Total_posts>6 ORDER BY Total_posts;"),
        new Consulta(2, "Ingresa las palabras faltantes del siguiente query: CREATE TABLE t_Cuentas ( [CuentaID] INT NOT NULL, [Saldo] DECIMAL (10,2) NOT NULL;", "CREATE TABLE t_Cuentas ( [CuentaID] INT NOT NULL, [Saldo] DECIMAL (10,2) NOT NULL;"),
        new Consulta(2, "Escribe la sentencia para visualizar todos los datos de la tabla t_categorias:", "SELECT * FROM t_categorias;"),

        new Consulta(3, "Escribe la sentencia para borrar los datos de la tabla t_post que en su c_postID sean mayores a 10 (usa DELETE):", "DELETE FROM t_post WHERE c_postID>10;"),
        new Consulta(3, "Sentencia para contar el número de post que ha hecho un usuario, teniendo en cuenta que la columna de post es c_post, y el nombre de usuario es c_usuario de la tabla t_posts (usa COUNT y GROUP BY):", "SELECT COUNT(c_post),c_usuario FROM t_post GROUP BY c_usuario;"),
        new Consulta(3, "Mostrar id de Post que sean 11 o 18 de la tabla t_post, donde la columna del id post es c_id_post (usa WHERE):", "SELECT c_id_post FROM t_post WHERE c_id_post=11 or c_id_post=18;"),
        new Consulta(3, "Unir las tablas t_post y t_usuarios por medio de la columna c_usuarioID (usa JOIN):", "SELECT * FROM t_post JOIN t_usuarios ON t_post.c_usuarioID= t_usuarios.c_usuarioID;"),
        new Consulta(3, "Apellido Paterno más común de la tabla t_usuarios: SELECT TOP 1 c_apaterno AS Total, COUNT(*) FROM t_usuarios GROUP BY apaterno ORDER BY Total DESC;", "SELECT TOP 1 c_apaterno AS Total, COUNT(*) FROM t_usuarios GROUP BY apaterno ORDER BY Total DESC;"),
        new Consulta(3, "Palabra que se utiliza para ejecutar un procedimiento almacenado:", "EXEC"),
        new Consulta(3, "Crear un procedimiento almacenado que muestre toda la información de la tabla t_usuarios (usa CREATE PROCEDURE):", "CREATE PROCEDURE MostrarInfo AS SELECT * FROM t_usuarios GO;"),
        new Consulta(3, "Crear un procedimiento almacenado que inserte información de un usuario a la tabla t_usuarios (usa CREATE PROCEDURE y INSERT INTO):", "CREATE PROCEDURE InsertarUsuario @nombre nvarchar(20), @apaterno nvarchar(20), @usuario nvarchar(20) AS INSERT INTO t_usuarios (c_nombre, c_apaterno, c_usuario) VALUES (@nombre, @apaterno,@usuario) GO;"),
        new Consulta(3, "Ejecución del procedimiento almacenado InsertarUsuario con valores de nombre=”ANA”, apaterno=”ORTIZ”, usuario=”AORTIZ”:", "EXEC InsertarUsuario (“ANA”,”ORTIZ”,”AORTIZ”);"),
        new Consulta(3, "Crear una variable promedio y asignarle el valor 0 (usa SET):", "SET @promedio = 0")
    };

    void Start()
    {
        preguntasDelJuego = SeleccionarPreguntas();
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
        if (consultaActual < preguntasDelJuego.Count)
        {
            textConsulta.text = preguntasDelJuego[consultaActual].consulta;
            textConsola.text = "";
            CambiarColorInputField(preguntasDelJuego[consultaActual].nivel);
        }
        else
        {
            textConsulta.text = "Has completado todas las consultas.";
            textConsola.gameObject.SetActive(false);
        }
    }

    void CambiarColorInputField(int nivel)
    {
        Color color;
        switch (nivel)
        {
            case 1:
                color = Color.green;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.red;
                break;
            default:
                color = Color.white;
                break;
        }
        textConsola.GetComponent<Image>().color = color;
    }

    List<Consulta> SeleccionarPreguntas()
    {
        List<Consulta> seleccionadas = new List<Consulta>();
        System.Random rnd = new System.Random();

        var nivel1 = consultas.Where(c => c.nivel == 1).OrderBy(x => rnd.Next()).Take(4).ToList();
        var nivel2 = consultas.Where(c => c.nivel == 2).OrderBy(x => rnd.Next()).Take(3).ToList();
        var nivel3 = consultas.Where(c => c.nivel == 3).OrderBy(x => rnd.Next()).Take(3).ToList();

        seleccionadas.AddRange(nivel1);
        seleccionadas.AddRange(nivel2);
        seleccionadas.AddRange(nivel3);

        return seleccionadas; // Devuelve las preguntas en orden de nivel
    }

    public void VerificarRespuesta()
    {
        if (mostrarMensajeChocolate)
        {
            mostrarMensajeChocolate = false;
            consultaActual++;
            MostrarConsulta();
            return;
        }

        string textoIngresado = textConsola.text.Trim(); // Obtener y limpiar el texto ingresado
        Debug.Log($"Texto ingresado: '{textoIngresado}'");

        // Log de cada carácter ingresado y su código ASCII
        foreach (char c in textoIngresado)
        {
            Debug.Log($"Carácter: '{c}' ASCII: {(int)c}");
        }

        if (consultaActual < preguntasDelJuego.Count)
        {
            if (textoIngresado == preguntasDelJuego[consultaActual].respuestaCorrecta)
            {
                Debug.Log("El texto ingresado es correcto.");
                barraRestaScript.AumentarBarra(10); // Aumenta la barra de resta en 10 si es correcto
                contadorCorrectas++; // Incrementa el contador de respuestas correctas
                Debug.Log("Racha: " + contadorCorrectas);

                if (contadorCorrectas % 3 == 0)
                {
                    textConsulta.text = "Felicidades ganaste un chocolate";
                    textConsola.text = ""; // Vacía el campo de entrada
                    chocolateCounter.AddChocolate(); // Añade un chocolate al contador
                    mostrarMensajeChocolate = true; // Activa la bandera para el mensaje del chocolate
                    return; // No pasar a la siguiente consulta inmediatamente
                }
            }
            else
            {
                Debug.Log("El texto ingresado es incorrecto.");
                barraRestaScript.ReducirBarra(10); // Reduce la barra de resta en 10 si es incorrecto
                barraSumaScript.AumentarBarra(10); // Aumenta la barra de suma en 10 si es incorrecto
                contadorCorrectas = 0; // Reinicia el contador si la respuesta es incorrecta
                Debug.Log("Racha: " + contadorCorrectas);
            }

            consultaActual++;
            MostrarConsulta();
        }
    }
}
