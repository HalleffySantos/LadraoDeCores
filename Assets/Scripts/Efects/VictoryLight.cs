using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class VictoryLight : MonoBehaviour
{
    private IPlayer player;
    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        particle = gameObject.GetComponent<ParticleSystem>();

        var emission = particle.emission;  
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        EfeitoAtivo();
    }

    private void EfeitoAtivo()
    {
        var emission = particle.emission;
        if (player.GetPosicao().x >= 24 && player.GetPosicao().x <= 26 && player.GetPosicao().y >= -1)
        {
            emission.enabled = true;
        }
        else if (emission.enabled)
        {
            emission.enabled = false;
        }
    }
}
