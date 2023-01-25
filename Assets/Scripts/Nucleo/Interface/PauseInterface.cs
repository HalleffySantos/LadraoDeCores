using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PauseInterface;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;

public class PauseInterface : MonoBehaviour, IPauseInterface
{
    // Start is called before the first frame update
    public bool isPaused { get; set; }

    [Header("Interface de Pause")]
    public GameObject pausePanel;
    //public string scene;

    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScreen();
        }
        
    }

    void PauseScreen()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            pausePanel.SetActive(false);

        }   else
            {
                isPaused = true;
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Interface");
    }

    public void SaveGame()
    { 
        var player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        Debug.Log("Awake:" + player.sceneUltimoObjetoEmContato);

        SaveData.current.profile.ultimaSala = player.sceneUltimoObjetoEmContato;
    
    }
}
