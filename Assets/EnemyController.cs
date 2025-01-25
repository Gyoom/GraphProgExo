using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Life = 1;
    [SerializeField] private GameObject Shield;
    [SerializeField] private float ShieldLife = 3;
    [SerializeField] private bool ShieldActive = true;

   bool canDealDammage = true;


    private void Start()
    {
        Shield.SetActive(ShieldActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && !ShieldActive) {
            Hit(new Vector3(0, 0, 0));
        }
    }


    public void Hit(Vector3 hitPoint) {
        if (Life <= 0 || !canDealDammage) {
            return;
        }

        canDealDammage = false;
        StartCoroutine(CanDealDammage());


        if (ShieldActive) {
            Shield.GetComponent<SpawnShieldRipples>().DisplayRipples(hitPoint);
            ShieldLife--;

            if (ShieldLife <= 0) { 
                Shield.SetActive (false);
                ShieldActive = false;
            }
            return;
        }

        Life--;
        if (Life <= 0)
        {
            GetComponent<DissolveController>().StartDeathCoroutine();
        }
    }

    // to prevent to hit shiel and mob on the same strike
    IEnumerator CanDealDammage() {
        Shield.GetComponent<VisualEffect>().SetFloat("RotationSpeed", 0);
        
        yield return new WaitForSeconds(0.5f);

        Shield.GetComponent<VisualEffect>().SetFloat("RotationSpeed", 1);

        yield return new WaitForSeconds(0.5f);
        canDealDammage = true;
    }
}
