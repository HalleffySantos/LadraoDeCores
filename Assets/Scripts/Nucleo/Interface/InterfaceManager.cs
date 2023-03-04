using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Mapeamento;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class InterfaceManager : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public CanvasGroup canvasGroupBackground;
    public Animator animator;

    void Start()
    {
        if (!PlayerPrefs.HasKey(GameManagerMap.GameSaved.Value))
        {
            Destroy(GameObject.FindGameObjectWithTag(GameObjectsTags.ButtonTag.Value).gameObject);
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutMenu());
        Invoke("FadeOutBackground", 4f);
        Invoke("NovoJogo", 6f);
    }


    public void FadeOutBackground()
    {
       animator.SetBool("FadeOut", true);
    }

    IEnumerator FadeOutMenu()
    {
        while (canvasGroup.alpha>0){
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
    }

    public void NovoJogo()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Intro");
    }

    public void LoadGame()
    {
        GameManager.FirstLoadGame();
    }

    public void SairJogo(){ 
        //Editor Unity
        //UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado
        Application.Quit();
    }

}

