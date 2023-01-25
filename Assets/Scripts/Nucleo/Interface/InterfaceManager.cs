using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    //[SerializeField] private string faseJogo;
    public void NovoJogo(){
        //SceneManager.LoadScene(faseJogo);
        
        SceneManager.LoadScene("Fase 0");
        SceneManager.LoadSceneAsync("sala 1", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 2", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 3", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 4", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
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
