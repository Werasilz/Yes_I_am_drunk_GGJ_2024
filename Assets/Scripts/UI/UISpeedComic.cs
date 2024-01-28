using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class UISpeedComic : MonoBehaviour
{
    public GameObject speedComic;

    private void Update()
    {
        speedComic.SetActive(StarterAssetsInputs.Instance.sprint);
    }
}
