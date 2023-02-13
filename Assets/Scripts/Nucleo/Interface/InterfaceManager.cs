using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    public float fadeTime = 1f;
    private Button button;
    private CanvasGroup canvasGroup;

    void Start()
    {
        button = GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
        button.onClick.AddListener(FadeOut);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
         LoadGame();
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

