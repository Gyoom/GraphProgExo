using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;

public class PlayerController : MonoBehaviour
{
    ThirdPersonController Controller;
    Animator animator;

    void Start()
    {

        Controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            animator.SetTrigger("Death");
            GetComponent<DissolveController>().StartDeathCoroutine();
            Controller.CanMove = false;

        }
     
    }
}
