using UnityEngine;
using UnityEngine.EventSystems; // Importante per gestire il focus

public class MenuManager : MonoBehaviour
{
    [Header("Pagine del Menu")]
    public GameObject creditsPage;
    public GameObject startPage;
    public GameObject optionsPage;
    public GameObject scorePage;
    public GameObject quitPage;

    // Metodo per gestire l'animazione e il reset del focus
    public void OnButtonClick(Animator anim)
    {
        // 1. Fa partire l'animazione del bottone
        if (anim != null)
        {
            anim.SetTrigger("Pressed");
        }

        // 2. Forza la deselezione: il tasto non resterà "schiacciato" graficamente
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

    public void OpenCredits() { CloseAllPages(); if (creditsPage) creditsPage.SetActive(true); }
    public void OpenStart() { CloseAllPages(); if (startPage) startPage.SetActive(true); }
    public void OpenOptions() { CloseAllPages(); if (optionsPage) optionsPage.SetActive(true); }
    public void OpenScore() { CloseAllPages(); if (scorePage) scorePage.SetActive(true); }
    public void OpenQuit() { CloseAllPages(); if (quitPage) quitPage.SetActive(true); }

    public void QuitGame()
    {
        Debug.Log("Chiusura gioco...");
        Application.Quit();
    }
}