using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using TMPro;
using UnityEngine;

public class DialogoSkip : MonoBehaviour
{
    private ICutscene cutscene;
    private TextMeshProUGUI textoSkip;

    // Start is called before the first frame update
    void Start()
    {
        cutscene = GameObject.FindGameObjectWithTag(GameObjectsTags.CutsceneTag.Value).GetComponent<ICutscene>();
        textoSkip = gameObject.GetComponent<TextMeshProUGUI>();
        textoSkip.color = new Color(textoSkip.color.r, textoSkip.color.g, textoSkip.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscene.estaNoFinalDaCutscene)
        {
            textoSkip.color = new Color(textoSkip.color.r, textoSkip.color.g, textoSkip.color.b, 1);
        }
    }
}
