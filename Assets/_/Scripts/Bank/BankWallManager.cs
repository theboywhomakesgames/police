using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DB.Police
{
    public class BankWallManager : MonoBehaviour
    {
        public void GoIn()
        {
            wall0.DOMoveY(10, 0.5f).SetRelative(true).OnComplete(() =>
            {
                wall0.gameObject.SetActive(false);
            });

            wall1.DOMoveY(10, 0.5f).SetRelative(true).OnComplete(() =>
            {
                wall1.gameObject.SetActive(false);
            });
        }

        public void GoOut()
        {
            wall0.gameObject.SetActive(true);
            wall1.gameObject.SetActive(true);

            wall0.DOMoveY(-10, 0.5f).SetRelative(true);
            wall1.DOMoveY(-10, 0.5f).SetRelative(true);
        }

        [SerializeField] private Transform wall0, wall1;
    }
}