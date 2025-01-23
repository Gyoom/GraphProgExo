using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnShieldRipples : MonoBehaviour
{
    public GameObject shieldRipples;
    
    [SerializeField]
    private GameObject himself;

    private VisualEffect shieldRipplesVFX;


    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            var ripples = Instantiate(shieldRipples, transform);

            shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            //shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

            Destroy(ripples, 2);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != himself)
        {
            Debug.Log(collision.gameObject.ToString());
            var ripples = Instantiate(shieldRipples, transform);

            shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
            shieldRipplesVFX.SetVector3("SphereCenter", collision.contacts[0].point);

            Destroy(ripples, 2);
        }
    }

    public void DisplayRipples(Vector3 contactPoint) {
        var ripples = Instantiate(shieldRipples, transform);

        shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
        shieldRipplesVFX.SetVector3("SphereCenter", contactPoint);

        Destroy(ripples, 2);
    }
}
