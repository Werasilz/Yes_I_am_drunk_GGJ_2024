using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class UISpeedComic : MonoBehaviour
{
    public GameObject speedComic;

    private void Update()
    {
        if (GameManager.Instance.isStopPlayerMove)
        {
            speedComic.SetActive(false);
            return;
        }

        speedComic.SetActive(StarterAssetsInputs.Instance.sprint);
    }
}
