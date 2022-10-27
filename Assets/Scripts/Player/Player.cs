using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interacao;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer
{
    public bool estaNoChao { get; set; }

    private List<Collider2D> colisoesDoPlayer;

    private Rigidbody2D playerRigidbody;

    private float velocidadeMaxPlayer;

    private float forcaMovimento;

    private float forcaPulo;

    private float limiteInferior;

    // Start is called before the first frame update
    void Start()
    {
        colisoesDoPlayer = new List<Collider2D>();
        estaNoChao = false;

        velocidadeMaxPlayer = 1.5f;
        forcaPulo = 7f;
        forcaMovimento = 100f;
        limiteInferior = -5f;

        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoPular();
    }

    void FixedUpdate()
    {
        colisoesDoPlayer.Clear();
        MovimentoHorizontal();
        ForaDosLimites();
    }

    public void Morte()
    {
        SceneManager.LoadScene("Fase 0", LoadSceneMode.Single);
    }

    public Vector3 GetPosicao()
    {
        return gameObject.transform.position;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("A");
        if (!colisoesDoPlayer.Contains(col.collider))
        {
            Debug.Log("B");
            colisoesDoPlayer.Add(col.collider);
        }

        var interacao = col.gameObject.GetComponent<IInteracao>();
        if (interacao != null)
        {
            interacao.Acao(gameObject);
        }
    }

    private void MovimentoHorizontal()
    {
        //Debug.Log(estaNoChao.ToString() + " " + colisoesDoPlayer.Count().ToString());
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && estaNoChao)
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;
            
            playerRigidbody.AddForce(direction * forcaMovimento);
            playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, velocidadeMaxPlayer);
        }
    }

    private void MovimentoPular()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * forcaPulo, ForceMode2D.Impulse);
            estaNoChao = false;
        }
    }

    private void ForaDosLimites()
    {
        if (gameObject.transform.position.y < limiteInferior)
        {
            Morte();
        }
    }

}
