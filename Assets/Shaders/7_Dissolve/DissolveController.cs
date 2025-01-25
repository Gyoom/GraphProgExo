using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolveController : MonoBehaviour
{
    [Header("Materials")]
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public float delayBeforeDissolve = 1f;
    public float refreshRate = 0.025f;
    public float dissolveRate = 0.0125f;
    [Header("VFX")]
    public VisualEffect vfxDissolve;

    Material[] skinnedMaterials;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        if (SkinnedMeshRenderer != null) { 
            skinnedMaterials = SkinnedMeshRenderer.materials;
        }
    }

    public void StartDeathCoroutine() {
        animator.SetTrigger("Death");
        StartCoroutine(DissolveMesh());
    }

    IEnumerator DissolveMesh() {
        if(skinnedMaterials.Length > 0) {
            yield return new WaitForSeconds(delayBeforeDissolve);

            float counter = 0;

            if (vfxDissolve) { 
                vfxDissolve.Play();
            }

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1) {
                counter += dissolveRate;

                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        
    
    }
}
