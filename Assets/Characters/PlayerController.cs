using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{

    [Header("Shield")]
    public GameObject vfxShield;

    [Header("Slash")]
    [SerializeField]
    private GameObject Slashs;
    [SerializeField]
    private float SlashDelay;
    [SerializeField]
    private Transform SlashPos;

    ThirdPersonController Controller;
    Animator animator;

    bool shieldActive = false;

    [HideInInspector]
    public bool attack = false;

    void Start()
    {

        Controller = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && vfxShield)
        {     
            vfxShield.SetActive(!shieldActive);
            shieldActive = !shieldActive;
        }

        if (Input.GetMouseButtonDown(0) && !shieldActive && !attack)
        {
            animator.SetTrigger("SwordAttack");
            StartCoroutine(SwordVfx());
            attack = true;
        }

    }

    // Hit shield by body contact
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Shield") && canHit)
        {
            hit.transform.GetComponent<SpawnShieldRipples>().DisplayRipples(hit.point);

            canHit = false;
            StartCoroutine(DelayHit());
        }
    }*/

    IEnumerator SwordVfx() {
        Controller.CanMove = false;

        yield return new WaitForSeconds(SlashDelay);

        Slashs.transform.position = SlashPos.transform.position;
        VisualEffect effect = Slashs.transform.GetChild(0).GetComponent<VisualEffect>();
        if (effect != null)
        {
            effect.SetFloat("AlphaSecondaryColor", Random.Range(0.0f, 8.0f));
            effect.Play();
        }
        yield return new WaitForSeconds(1f);

        Controller.CanMove = true;
        attack = false;
    }
}
