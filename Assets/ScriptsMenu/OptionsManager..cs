using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource musicAudioSource;

    void Start()
    {
        // 1. Carica il valore salvato (se non esiste, parte da 1f)
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // 2. Imposta lo slider e l'audio
        volumeSlider.value = savedVolume;

        if (musicAudioSource != null)
        {
            musicAudioSource.volume = savedVolume;
        }

        // 3. Collega l'evento dello slider
        volumeSlider.onValueChanged.AddListener(SaveVolume);
    }

    public void SaveVolume(float value)
    {
        // Salva il valore
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        // Applica il volume in tempo reale
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = value;
        }
    }

    // Aggiungi questa funzione per il bottone Reset
    public void ResetSettings()
    {
        Debug.Log("Reset eseguito!");

        // Cancella i dati salvati
        PlayerPrefs.DeleteAll();

        // Riporta lo slider al massimo (1f)
        volumeSlider.value = 1f;

        // Forza l'audio al massimo
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = 1f;
        }
    }
}