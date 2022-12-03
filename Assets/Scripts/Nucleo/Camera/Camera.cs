using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private IPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        AcompanharMovPlayer();
    }

    private void AcompanharMovPlayer()
    {
        gameObject.transform.position = new Vector3(player.GetPosicao().x, player.GetPosicao().y, -10);
    }
}
