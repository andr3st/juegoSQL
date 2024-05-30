using UnityEngine;
using UnityEngine.SceneManagement; // Importa SceneManager
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ConsoleManager : MonoBehaviour
{
    public TMP_InputField textConsola;
    public TextMeshProUGUI textConsulta;
    public barraResta barraRestaScript; // Referencia al script barraResta
    public barraSuma barraSumaScript;   // Referencia al script barraSuma
    public ChocolateCounter chocolateCounter; // Referencia al script ChocolateCounter
    public LevelChangeImage levelChangeImage; // Referencia al script LevelChangeImage
    public GameObject specialImage; // Referencia al GameObject que contiene la imagen especial
    public AudioSource soundClick; // Referencia al sonido de clic

    private int consultaActual = 0;
    private int contadorCorrectas = 0; // Contador de respuestas correctas consecutivas
    private int contadorIncorrectas = 0; // Contador de respuestas incorrectas consecutivas
    private bool mostrarMensajeChocolate = false; // Bandera para el mensaje de chocolate
    private int nivelAnterior = 0; // Variable para almacenar el nivel anterior

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
    // Nivel 1
    new Consulta(1, "Para la creación de una base de datos utilizamos el comando:", "CREATE DATABASE"), //si
    new Consulta(1, "Para la creación de tablas utilizamos el comando:", "CREATE TABLE"), //si
    new Consulta(1, "Para la inserción de datos a una tabla utilizamos el comando:", "INSERT INTO"), //si
    new Consulta(1, "Para visualizar todos los datos de una tabla utilizamos el comando:", "SELECT * FROM"), //si
    new Consulta(1, "Palabra que se utiliza para visualizar datos que cumplan una condición:", "WHERE"), //si
    new Consulta(1, "Palabra que se usa para unir 2 tablas:", "JOIN"), //si
    new Consulta(1, "Comando que se utiliza para agrupar filas en una tabla en función del valor de una columna determinada:", "GROUP BY"), //si
    new Consulta(1, "Función para devolver el nombre del mes:", "MONTHNAME()"), //si
    new Consulta(1, "Palabra y función que se utiliza para realizar el conteo de los datos:", "COUNT(*)"), //si
    new Consulta(1, "Palabra para asignar un nombre a una columna de la tabla de visualización:", "AS"), //si

    // Nivel 2
    new Consulta(2, "Para modificar los datos existentes de una tabla se utiliza la palabra:", "UPDATE"), //si
    new Consulta(2, "Modificar el nombre de usuario (Alix) en la columna c_nombre de la tabla t_usuarios donde su correo es ale@gmail.com (usa UPDATE):", "UPDATE t_usuarios SET c_nombre = 'Alix' WHERE c_correo = 'ale@gmail.com';"), // si
    new Consulta(2, "Selecciona los primeros 3 usuarios de la tabla t_usuarios (usa LIMIT):", "SELECT * FROM t_usuarios LIMIT 3;"), // si
    new Consulta(2, "Recupera la información de la tabla t_post y regresa la cantidad de publicaciones con la fecha del 23 de enero de 2023, la columna se llama dFecha (usa COUNT):", "SELECT COUNT(*) FROM t_post WHERE dFecha='2023-01-23';"), // si
    new Consulta(2, "Actualizar el saldo a 500.00 en la tabla t_Cuentas donde el CuentaID es 10:", "UPDATE t_Cuentas SET Saldo = 500.00 WHERE CuentaID = 10;"), // si
    new Consulta(2, "Selecciona los usuarios (c_nombre) cuyo nombre comienza con 'A' de la tabla (t_usuarios) (usa LIKE):", "SELECT * FROM t_usuarios WHERE c_nombre LIKE 'A%';"), // si
    new Consulta(2, "Recupera el correo y el nombre (c_correo), (c_nombre) de todos los usuarios (t_usuarios) ordenados por nombre de forma ascendente:", "SELECT c_correo, c_nombre FROM t_usuarios ORDER BY c_nombre ASC;"), //si
    new Consulta(2, "Selecciona todos los registros de la tabla t_ventas donde el monto de la venta (cmonto_venta) sea mayor a 1000 (usa WHERE):", "SELECT * FROM t_ventas WHERE cmonto_venta > 1000;"),//si
    new Consulta(2, "Ingresa las palabras faltantes del siguiente query: CREATE TABLE t_Cuentas ( [CuentaID] INT NOT NULL, [Saldo] DECIMAL (10,2) NOT NULL);", "CREATE TABLE t_Cuentas ( [CuentaID] INT NOT NULL, [Saldo] DECIMAL (10,2) NOT NULL);"), // si
    new Consulta(2, "Escribe la sentencia para visualizar todos los datos de la tabla t_categorias:", "SELECT * FROM t_categorias;"), //si 

    // Nivel 3
    new Consulta(3, "Escribe la sentencia para borrar los datos de la tabla t_post que en su c_postID sean mayores a 10 (usa DELETE):", "DELETE FROM t_post WHERE c_postID>10;"), //si
    new Consulta(3, "Sentencia para contar el número de post que ha hecho un usuario, teniendo en cuenta que la columna de post es c_post, y el nombre de usuario es c_usuario de la tabla t_posts (usa COUNT y GROUP BY):", "SELECT COUNT(c_post), c_usuario FROM t_posts GROUP BY c_usuario;"), // si
    new Consulta(3, "Mostrar id de Post que sean 11 o 18 de la tabla t_post, donde la columna del id post es c_id_post (usa WHERE):", "SELECT c_id_post FROM t_post WHERE c_id_post=11 or c_id_post=18;"), //si
    new Consulta(3, "Unir las tablas t_post y t_usuarios por medio de la columna c_usuarioID (usa JOIN):", "SELECT * FROM t_post JOIN t_usuarios ON t_post.c_usuarioID= t_usuarios.c_usuarioID;"), //si
    new Consulta(3, "Eliminar los usuarios de la tabla t_usuarios cuyo c_usuarioID sea menor a 5 (usa DELETE):", "DELETE FROM t_usuarios WHERE c_usuarioID < 5;"),// si
    new Consulta(3, "Palabra que se utiliza para ejecutar un procedimiento almacenado:", "EXEC"), //si
    new Consulta(3, "Selecciona el total de usuarios (Total_usuarios) agrupados por su apellido paterno (c_apaterno) y ordenados de forma descendente de la tabla t_usuarios:", "SELECT c_apaterno, COUNT(*) AS Total_usuarios FROM t_usuarios GROUP BY c_apaterno ORDER BY Total_usuarios DESC;"), //si
    new Consulta(3, "Actualizar el nombre de usuario (c_nombre) en la tabla t_usuarios a (Juan) donde el ID del usuario (c_usuarioID) es 5:", "UPDATE t_usuarios SET c_nombre = 'Juan' WHERE c_usuarioID = 5;"), //si
    new Consulta(3, "Ejecución del procedimiento almacenado ActualizarSaldo con CuentaID=5 y NuevoSaldo=300.00:", "EXEC ActualizarSaldo 5, 300.00;"), // si
    new Consulta(3, "Crear una variable promedio y asignarle el valor 0 (usa SET):", "SET @promedio = 0") //si
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
            SceneManager.LoadScene(2); // Cambia a la escena con índice 2 cuando no haya más consultas
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

        // Reproduce el sonido de subir de nivel solo si el nivel ha cambiado
        if (nivel != nivelAnterior)
        {
            SoundManager.Instance.PlaySoundLevelUp();
            nivelAnterior = nivel; // Actualiza el nivel anterior
        }
    }

    List<Consulta> SeleccionarPreguntas()
    {
        List<Consulta> seleccionadas = new List<Consulta>();
        System.Random rnd = new System.Random();

        var nivel1 = consultas.Where(c => c.nivel == 1).OrderBy(x => rnd.Next()).Take(4).ToList();
        var nivel2 = consultas.Where(c => c.nivel == 2).OrderBy(x => rnd.Next()).Take (3).ToList();
        var nivel3 = consultas.Where(c => c.nivel == 3).OrderBy(x => rnd.Next()).Take (3).ToList();

        seleccionadas.AddRange(nivel1);
        seleccionadas.AddRange(nivel2);
        seleccionadas.AddRange(nivel3);

        return seleccionadas; // Devuelve las preguntas en orden de nivel
    }

    public void VerificarRespuesta()
    {
        soundClick.Play(); // Reproduce el sonido de clic

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
                contadorIncorrectas = 0; // Reinicia el contador de respuestas incorrectas
                Debug.Log("Racha: " + contadorCorrectas);
                SoundManager.Instance.PlaySoundCorrectAnswer(); // Reproduce el sonido de respuesta correcta

                if (contadorCorrectas % 3 == 0)
                {
                    textConsulta.text = "Felicidades ganaste un chocolate";
                    textConsola.text = ""; // Vacía el campo de entrada
                    chocolateCounter.AddChocolate(); // Añade un chocolate al contador
                    mostrarMensajeChocolate = true; // Activa la bandera para el mensaje del chocolate
                    SoundManager.Instance.PlaySoundChocolate(); // Reproduce el sonido de chocolate
                    return; // No pasar a la siguiente consulta inmediatamente
                }
            }
            else
            {
                Debug.Log("El texto ingresado es incorrecto.");
                barraRestaScript.ReducirBarra(10); // Reduce la barra de resta en 10 si es incorrecto
                barraSumaScript.AumentarBarra(20); // Aumenta la barra de suma en 10 si es incorrecto
                contadorCorrectas = 0; // Reinicia el contador si la respuesta es incorrecta
                contadorIncorrectas++; // Incrementa el contador de respuestas incorrectas
                Debug.Log("Racha negativa: " + contadorIncorrectas);
                SoundManager.Instance.PlaySoundIncorrectAnswer(); // Reproduce el sonido de respuesta incorrecta

                if (contadorIncorrectas == 3)
                {
                    textConsulta.text = "Tu compañero te mandó un chocolate dice que le eches ganas y no te rindas";
                    textConsola.text = ""; // Vacía el campo de entrada
                    chocolateCounter.AddChocolate(); // Añade un chocolate al contador
                    mostrarMensajeChocolate = true; // Activa la bandera para el mensaje del chocolate
                    specialImage.SetActive(true); // Muestra la imagen especial
                    SoundManager.Instance.PlaySoundFriend(); // Reproduce el sonido de amigo
                    StartCoroutine(EsconderImagen());
                    contadorIncorrectas = 0; // Reinicia el contador de racha negativa después de mostrar el mensaje
                    return; // No pasar a la siguiente consulta inmediatamente
                }
            }

            consultaActual++;
            MostrarConsulta();
        }
    }

    private IEnumerator EsconderImagen()
    {
        yield return new WaitForSeconds(5); // Espera 5 segundos
        specialImage.SetActive(false); // Esconde la imagen especial
    }
}
