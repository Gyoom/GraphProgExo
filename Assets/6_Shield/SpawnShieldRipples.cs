using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipples : MonoBehaviour
{
    public GameObject shieldRipples;
    private VisualEffect shieldRipplesVFX;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("AAA");
            var ripples = Instantiate(shieldRipples, transform);

            shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            //shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

            Destroy(ripples, 2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ripples = Instantiate(shieldRipples, transform);

        shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
        shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

        Destroy(ripples, 2);
    }
}
