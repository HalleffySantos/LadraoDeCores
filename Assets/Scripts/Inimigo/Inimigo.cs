using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using UnityEngine;

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

    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.CompareTag(GameObjectsTags.PlayerTag.Value))
        {
            tObject.GetComponent<IPlayer>().Morte();
        }
    }

    public void AcaoSaida(GameObject tObject)
    {
        throw new System.NotImplementedException();
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
}
