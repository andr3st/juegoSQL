using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateClickDetector : MonoBehaviour
{
    void OnMouseDown()
    {
        // Llama a la función en ChocolateCounter cuando se hace clic en el chocolate
        ChocolateCounter chocolateCounter = FindObjectOfType<ChocolateCounter>();
        if (chocolateCounter != null)
        {
            chocolateCounter.UseChocolate();
        }
    }
}
