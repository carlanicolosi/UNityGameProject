using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections; // Necessario per le Coroutines

public class MenuManager : MonoBehaviour
{
    // --- SINGLETON PATTERN (Punti extra!) ---
    public static MenuManager Instance { get; private set; }

    [Header("Pagine del Menu")]
    public GameObject creditsPage;
    public GameObject startPage;
    public GameObject optionsPage;
    public GameObject scorePage;
    public GameObject quitPage;

    private void Awake()
    {
        // Inizializzazione del Singleton
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        // Esempio uso PlayerPrefs: Carica il volume salvato all'avvio
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        Debug.Log("Volume caricato: " + savedVolume);

        CloseAllPages();
        // Mostra solo la pagina iniziale se vuoi
    }

    public void OnButtonClick(Animator anim)
    {
        if (anim != null)
        {
            anim.SetTrigger("Pressed");
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void CloseAllPages()
    {
        if (creditsPage) creditsPage.SetActive(false);
        if (startPage) startPage.SetActive(false);
        if (optionsPage) optionsPage.SetActive(false);
        if (scorePage) scorePage.SetActive(false);
        if (quitPage) quitPage.SetActive(false);
    }

    // --- USO DELLE COROUTINES (Punti extra!) ---
    // Invece di aprirle subito, facciamo una piccola attesa per far finire l'animazione del tasto
    public void OpenCredits() { StartCoroutine(WaitAndOpen(creditsPage)); }
    public void OpenStart() { StartCoroutine(WaitAndOpen(startPage)); }
    public void OpenOptions() { StartCoroutine(WaitAndOpen(optionsPage)); }
    public void OpenScore() { StartCoroutine(WaitAndOpen(scorePage)); }

    private IEnumerator WaitAndOpen(GameObject page)
    {
        yield return new WaitForSeconds(0.2f); // Aspetta 0.2 secondi
        CloseAllPages();
        if (page) page.SetActive(true);
    }

    // Metodo per la pagina Opzioni: Salva il volume
    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save(); // Salva fisicamente i dati
    }

    public void QuitGame()
    {
        Debug.Log("Chiusura gioco...");
        Application.Quit();
    }
}