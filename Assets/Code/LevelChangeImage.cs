using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelChangeImage : MonoBehaviour
{
    public Image imageComponent; // Referencia al componente Image
    public Sprite originalImage; // Imagen original
    public Sprite levelChangeImage; // Imagen que se mostrará temporalmente
    public float displayTime = 2f; // Tiempo que se mostrará la imagen temporal

    private void Start()
    {
        // Verificar que las referencias estén asignadas
        if (imageComponent == null)
        {
            Debug.LogError("Image component is not assigned.");
            return;
        }

        if (originalImage == null)
        {
            Debug.LogError("Original image is not assigned.");
            return;
        }

        // Asegurarse de que la imagen inicial sea la original
        imageComponent.sprite = originalImage;
    }

    public void ChangeImageTemporarily()
    {
        // Verificar que la imagen de cambio de nivel esté asignada
        if (levelChangeImage == null)
        {
            Debug.LogError("Level change image is not assigned.");
            return;
        }

        StartCoroutine(ChangeImageCoroutine());
    }

    private IEnumerator ChangeImageCoroutine()
    {
        // Cambiar a la imagen temporal
        imageComponent.sprite = levelChangeImage;

        // Esperar el tiempo especificado
        yield return new WaitForSeconds(displayTime);

        // Volver a la imagen original
        imageComponent.sprite = originalImage;
    }
}
