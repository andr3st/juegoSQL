using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource soundCorrectAnswer;
    public AudioSource soundIncorrectAnswer;
    public AudioSource soundChocolate;
    public AudioSource soundLevelUp;
    public AudioSource soundClickChocolate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundCorrectAnswer()
    {
        soundCorrectAnswer.Play();
    }

    public void PlaySoundIncorrectAnswer()
    {
        soundIncorrectAnswer.Play();
    }

    public void PlaySoundChocolate()
    {
        soundChocolate.Play();
    }

    public void PlaySoundLevelUp()
    {
        soundLevelUp.Play();
    }

    public void PlaySoundClickChocolate()
    {
        soundClickChocolate.Play();
    }
}
