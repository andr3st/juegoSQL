using UnityEngine;
using TMPro;

public class ChocolateCounter : MonoBehaviour
{
    public TextMeshProUGUI chocolateText; // Referencia al texto del contador
    public barraSuma barraSumaScript; // Referencia al script de la barraSuma

    private int chocolateCount = 0; // Contador de chocolates

    void Start()
    {
        UpdateChocolateText();
    }

    public void AddChocolate()
    {
        chocolateCount++;
        UpdateChocolateText();
    }

    public void UseChocolate()
    {
        if (chocolateCount > 0)
        {
            chocolateCount--;
            UpdateChocolateText();
            barraSumaScript.ReducirBarra(15); // Resta 15 puntos de la barra
        }
    }

    private void UpdateChocolateText()
    {
        chocolateText.text = "x" + chocolateCount.ToString();
    }
}
