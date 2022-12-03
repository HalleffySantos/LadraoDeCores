using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

// Efeito em que bolas de luz saem do player.
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