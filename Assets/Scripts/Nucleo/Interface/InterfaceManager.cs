using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    public CanvasGroup canvasGroup;
     public CanvasGroup canvasGroupBackground;

    public void FadeOut()
    {
        StartCoroutine(FadeOutBackground());
        StartCoroutine(FadeOutMenu());
        //Invoke("StartCoroutine(FadeOutBackground())", 3f);
        Invoke("LoadGame", 5f);
    }

     IEnumerator FadeOutBackground()
    {
        //CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroupBackground.alpha>0){
            canvasGroupBackground.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        canvasGroupBackground.interactable = false;
        yield return null;
    }

    IEnumerator FadeOutMenu()
    {
        //CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        while (canvasGroup.alpha>0){
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
    }

    public void NovoJogo(){
        // SceneManager.LoadScene("Global");
        // SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 10", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 11", LoadSceneMode.Additive);

        LoadGame();
        
    }

    public void LoadGame()
    {
        float posYPlayer = 0;
        float posXPlayer = 0;

        if (PlayerPrefs.HasKey("posYPlayer"))
        {
            posYPlayer = PlayerPrefs.GetFloat("posYPlayer");
        }

        if (PlayerPrefs.HasKey("posXPlayer"))
        {
            posXPlayer = PlayerPrefs.GetFloat("posXPlayer");
        }

        SceneManager.LoadScene("Intro");
        // SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 10", LoadSceneMode.Additive);
        // SceneManager.LoadSceneAsync("Sala 11", LoadSceneMode.Additive);
    }

    public void Salvar(){

    }

    public void SairJogo(){
        Debug.Log("Sair do Jogo");

        //Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado
        //Application.Quit();
    }
}

