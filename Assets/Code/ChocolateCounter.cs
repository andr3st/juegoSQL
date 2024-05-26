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
            barraSumaScript.ReducirBarra(20); // Resta 20 puntos de la barra
            SoundManager.Instance.PlaySoundClickChocolate(); // Reproduce el sonido al usar un chocolate
        }
    }

    private void UpdateChocolateText()
    {
        chocolateText.text = "x" + chocolateCount.ToString();
    }

    public void OnClickChocolate()
    {
        SoundManager.Instance.PlaySoundClickChocolate(); // Reproduce el sonido de clic en el chocolate
    }
}
