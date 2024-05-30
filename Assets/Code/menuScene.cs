using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChanger : MonoBehaviour
{

    // Método para cambiar a una escena específica por índice
    public void MenuScene(int sceneIndex)
    {
        SceneManager.LoadScene(0);
    }
}