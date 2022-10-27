using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interacao;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer
{
    public bool estaNoChao { get; set; }

    private Rigidbody2D playerRigidbody;

    private float velocidadeMaxPlayer;

    private float forcaMovimento;

    private float forcaPulo;

    private float limiteInferior;

    // Start is called before the first frame update
    void Start()
    {
        estaNoChao = false;

        velocidadeMaxPlayer = 4.5f;
        forcaPulo = 7f;
        forcaMovimento = 75f;
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
        //MovimentoHorizontal01();
        MovimentoHorizontal02();
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
        var interacao = col.gameObject.GetComponent<IInteracao>();
        if (interacao != null)
        {
            interacao.AcaoEntrada(gameObject);
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        var interacao = col.gameObject.GetComponent<IInteracao>();
        if (interacao != null)
        {
            interacao.AcaoSaida(gameObject);
        }
    }

    private void MovimentoHorizontal01()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && estaNoChao)
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);

            Debug.Log( playerRigidbody.velocity.x.ToString() + " " + (forcaMovimento * Time.fixedDeltaTime).ToString() + " " +
            (playerRigidbody.velocity.x + forcaMovimento * Time.fixedDeltaTime).ToString() );

            if (direction.x > 0 && (playerRigidbody.velocity.x + forcaMovimento * Time.fixedDeltaTime) < velocidadeMaxPlayer)
            {
                playerRigidbody.AddForce(direction * forcaMovimento);
            }

            if (direction.x < 0 && (playerRigidbody.velocity.x - forcaMovimento * Time.fixedDeltaTime) > -velocidadeMaxPlayer)
            {
                playerRigidbody.AddForce(direction * forcaMovimento);
            }
        }
    }

    private void MovimentoHorizontal02()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            var direction = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);

            gameObject.transform.position += direction * Time.fixedDeltaTime * velocidadeMaxPlayer; 
        }
    }

    private void MovimentoPular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            playerRigidbody.AddForce(Vector3.up * forcaPulo, ForceMode2D.Impulse);
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
