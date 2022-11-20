using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer
{
    public bool estaNoChao { get; set; }

    private Animator animatorPlayer;

    private Rigidbody2D playerRigidbody;

    private float velocidadeMaxPlayer;

    private float forcaPulo;

    private float limiteInferior;

    public Vector3 direcaoMovimento { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        estaNoChao = false;

        velocidadeMaxPlayer = 4.5f;
        forcaPulo = 7f;
        limiteInferior = -5f;
        direcaoMovimento = new Vector3(0, 0, 0);

        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        animatorPlayer = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoPular();
    }

    void FixedUpdate()
    {
        MovimentoHorizontal();
        ForaDosLimites();

        ConfiguracaoAnimacaoPlayer();
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

    private void MovimentoHorizontal()
    {
        direcaoMovimento = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {   
            gameObject.transform.rotation = new Quaternion(0, direcaoMovimento.x < 0 ? 180 : 0, 0, 0);

            gameObject.transform.position += direcaoMovimento * Time.fixedDeltaTime * velocidadeMaxPlayer;
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

    private void ConfiguracaoAnimacaoPlayer()
    {
        AnimacaoAndar();
        AnimacaoParada();
        
        //AnimacaoPular();
        //AnimacaoFala();
        
    }

    private void AnimacaoAndar()
    {
        if (EstaAndando())
        {
            animatorPlayer.SetBool("EstaAndando", true);
        }
    }

    private void AnimacaoParada()
    {
        if (!EstaAndando() && animatorPlayer.GetBool("EstaAndando"))
        {
            animatorPlayer.SetBool("EstaAndando", false);
        }
    }

    private bool EstaAndando()
    {
        return direcaoMovimento.x > 0 || direcaoMovimento.x < 0 ? true : false;
    }

}
