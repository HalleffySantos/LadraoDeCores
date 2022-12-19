using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Ave : MonoBehaviour, IInteracao
{
    public float limiteDireita;

    public float limiteEsquerda;

    public float velocidadeInimigo;

    private Rigidbody2D enemyRigibody;
    
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
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
        }

        if (gameObject.transform.position.x >= limiteDireita)
        {
            direction = new Vector3(-1, 0, 0);
        }
    }

    private void Morte()
    {
        Destroy(gameObject);
    }
}
