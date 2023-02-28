using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Espeto : Terreno
{
    //Audio clip referente ao som de quendo o player ataca um espeto.
    public AudioClip somHit;
    private AudioSource audioSource;
    private IPlayer player;
    private IGameManager gameManager;
    private bool amareloPrimeiroEncontro;
    
    private Color cinza;
    private Color amarelo;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        amareloPrimeiroEncontro = false;

        cinza = gameManager.Cinza;
        amarelo = gameManager.Amarelo;
    }

    public override void Update()
    {
        TrocaCorEspeto();
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            tObject.GetComponent<IPlayer>().Morte();
            return;
        }

        if(tObject.GetComponent<IArma>() != null)
        {
            audioSource.PlayOneShot(somHit);
            player.RecuoVertical();
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {

    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
        if(tObject.GetComponent<IArma>() != null)
        {
            player.RecuoVertical();
        }
    }

    private void TrocaCorEspeto()
    {
        if (gameManager != null && gameManager.AmareloEncontrado && !amareloPrimeiroEncontro)
        {
            amareloPrimeiroEncontro = true;

            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                if (Random.Range(0, 10000)%2 == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = amarelo;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = cinza;
                }
            }
        } 
    }
}
