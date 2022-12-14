using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

// Script base de um inimigo.
public class Inimigo : MonoBehaviour, IInteracao
{
    private Rigidbody2D enemyRigibody;
    
    private Vector3 direction;

    private float forcaMovimento;

    private float velocidadeMaxInimigo;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigibody = gameObject.GetComponent<Rigidbody2D>();
        direction = new Vector3(-1, 0, 0).normalized;
        forcaMovimento = 100f;
        velocidadeMaxInimigo = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
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

        if(tObject.GetComponent<IArma>() != null)
        {
            Morte();
        }
    }

    public void AcaoSaida(GameObject tObject)
    {
        
    }

    public void AcaoStay(GameObject tObject)
    {

    }

    private void Movimento()
    {
        enemyRigibody.AddForce(direction * forcaMovimento);
        enemyRigibody.velocity = Vector3.ClampMagnitude(enemyRigibody.velocity, velocidadeMaxInimigo);
    }

    private void TrocaDeDirecao()
    {
        if (gameObject.transform.position.x <= 13)
        {
            direction = new Vector3(1, 0, 0);
        }

        if (gameObject.transform.position.x >= 20)
        {
            direction = new Vector3(-1, 0, 0);
        }
    }

    private void Morte()
    {
        Destroy(gameObject);
    }

}
