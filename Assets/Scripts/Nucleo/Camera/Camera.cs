using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente a camera.
public class Camera : MonoBehaviour, ICamera
{
    public float variacaoYCameraPlayer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AcompanharMovPlayer();
    }

    // Vincula a camera ao player
    private void AcompanharMovPlayer()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            var player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
            gameObject.transform.position = new Vector3(player.GetPosicao().x, player.GetPosicao().y + variacaoYCameraPlayer, -10);
        }

    }
}
