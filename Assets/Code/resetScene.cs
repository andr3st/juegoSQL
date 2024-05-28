using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Método para cambiar a una escena específica por índice
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(1);
    }
}
