using Assets.Scripts.Enumeradores.Animacoes;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Ave : MonoBehaviour, IInteracao, IInimigo
{
    // Limite a direita de onde a ave pode ir.
    public float limiteDireita;

    // Limite a esquerda de onde a ave pode ir.
    public float limiteEsquerda;

    // Velocidade de movimentação da ave.
    public float velocidadeInimigo;

    private Rigidbody2D enemyRigibody;
    
    private Vector3 direction;

    private Animator aveAnimator;


    // Start is called before the first frame update
    void Start()
    {
        aveAnimator = gameObject.GetComponent<Animator>();
        enemyRigibody = gameObject.GetComponent<Rigidbody2D>();
        direction = new Vector3(-1, 0, 0).normalized;

        enemyRigibody.gravityScale = 0;
    }

    void Update()
    {
        Movimento();
        TrocaDeDirecao();
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            tObject.GetComponent<IPlayer>().Morte();
            return;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public void AcaoSaida(GameObject tObject)
    {
        
    }

    // Método para realização da ação chamada pelo OnTriggerStay ou OnCollisionStay.
    public void AcaoStay(GameObject tObject)
    {
        
    }

    private void Movimento()
    {
        enemyRigibody.velocity = direction * velocidadeInimigo;
    }

    private void TrocaDeDirecao()
    {
        if (gameObject.transform.position.x <= limiteEsquerda)
        {
            direction = new Vector3(1, 0, 0);
            aveAnimator.SetBool(TriggersAnimacaoAve.MovDir.Value, true);
        }

        if (gameObject.transform.position.x >= limiteDireita)
        {
            direction = new Vector3(-1, 0, 0);
            aveAnimator.SetBool(TriggersAnimacaoAve.MovDir.Value, false);
        }
    }

    private void Morte()
    {
        Destroy(gameObject);
    }
}
