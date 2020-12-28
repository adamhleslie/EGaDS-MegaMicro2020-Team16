using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team16
{
    public class EnteringTextAnimator : MonoBehaviour
    {
        [SerializeField]
        private bool _crazyText = true;

        void Awake()
        {
            Animation animation = GetComponent<Animation>();
            animation.Play("Text_Entering");

            if (_crazyText)
            {
                animation.Blend("CrazyText");
            }
        }
    }
}
