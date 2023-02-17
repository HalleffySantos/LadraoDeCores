using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores.Animacoes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour, ICutscene
{
    public bool estaNoFinalDaCutscene { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        estaNoFinalDaCutscene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estaNoFinalDaCutscene || Input.GetKeyDown(KeyCode.E))
        {
            GameManager.FirstLoadGame();
        }
    }

    private void EndCutscene()
    {
        estaNoFinalDaCutscene = true;
    }

}
