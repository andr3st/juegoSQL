using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{

    // Método para cambiar a una escena específica por índice
    public void CambiarEscena(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}