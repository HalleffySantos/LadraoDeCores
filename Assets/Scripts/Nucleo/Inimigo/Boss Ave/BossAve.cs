using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Infraestrutura.Pair;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class BossAve : MonoBehaviour, IInteracao, IBossAve
{
    public float tempoDeAguardo;

    private float tempoInicioBusca;

    private bool podePerseguir;

    private bool momentoAnterior;

    private Vector3 direcaoInicial;

    private IList<Vector3> posParaSeguir;

    private IPlayer player;

    private Rigidbody2D enemyRigidbody;

    private IGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value) != null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();
        }

        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }

        enemyRigidbody = GetComponent<Rigidbody2D>();

        enemyRigidbody.gravityScale = 0;
        momentoAnterior = false;
        podePerseguir = false;
        posParaSeguir = new List<Vector3>();   
    }

    // Update is called once per frame
    void Update()
    {
        EstaVivo();
        FollowPlayer();
    }

    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            tObject.GetComponent<IPlayer>().Morte();
            return;
        }
    }

    public void AcaoSaida(GameObject tObject)
    {

    }

    public void AcaoStay(GameObject tObject)
    {

    }

    public void ComecaAPerseguir()
    {
        podePerseguir = true;
    }

    private void FollowPlayer()
    {
        if (!podePerseguir || !player.movimentoHabilitado)
        {
            return;
        }

        posParaSeguir.Add(player.GetPosicao());

        if (!momentoAnterior)
        {
            momentoAnterior = true;
            tempoInicioBusca = Time.time;
            direcaoInicial = gameObject.transform.position - posParaSeguir[0];
        }

        if (Time.time >= tempoInicioBusca + tempoDeAguardo && player.movimentoHabilitado)
        {
            gameObject.transform.position = posParaSeguir[0];
            posParaSeguir.Remove(posParaSeguir[0]);
        }

        else 
        {
            Vector3 direcao = gameObject.transform.position - posParaSeguir[0];
            gameObject.transform.position -= direcaoInicial * Time.deltaTime * 1/tempoDeAguardo;
        }
    }

    private void EstaVivo()
    {
        if (gameObject.transform.position.x < -5.5 && gameObject.transform.position.y < 5.5)
        {
            Destroy(gameObject);
        }
    }

}
