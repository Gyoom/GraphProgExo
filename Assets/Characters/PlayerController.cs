using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{

    [Header("Shield")]
    public GameObject vfxShield;

    ThirdPersonController Controller;
    Animator animator;

    bool shieldActive = false;
    bool canHit = true;

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

        if (Input.GetKeyDown(KeyCode.E) && vfxShield)
        {     
            vfxShield.SetActive(!shieldActive);
            shieldActive = !shieldActive;
            canHit = false;
            StartCoroutine(DelayHit());
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("SwordAttack");
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Shield") &&  canHit)
        {
            Debug.Log("AAA");
            hit.transform.GetComponent<SpawnShieldRipples>().DisplayRipples(hit.point);

            canHit = false;
            StartCoroutine(DelayHit());
        }
    }

    IEnumerator DelayHit() {
        yield return new WaitForSeconds(0.5f);

        canHit = true;
    }
}
