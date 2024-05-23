using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMain : MonoBehaviour
{
    [SpineAnimation]
    public string startAnimation;
    [SpineAnimation]
    public string idleAnimation;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();

        skeletonAnimation.AnimationState.SetAnimation(0, startAnimation, false);
        skeletonAnimation.AnimationState.AddAnimation(0, idleAnimation, true, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
