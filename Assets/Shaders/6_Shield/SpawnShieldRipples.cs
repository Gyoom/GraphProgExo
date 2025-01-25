using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class SpawnShieldRipples : MonoBehaviour
{
    [SerializeField] private GameObject shieldRipples;
    
    [SerializeField] GameObject himself;

    private VisualEffect shieldRipplesVFX;
    private VisualEffect shield;


    private void Start()
    {
        shield = GetComponent<VisualEffect>();
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Weapon") && player.attack)
        {
            himself.GetComponent<EnemyController>().Hit(collision.contacts[0].point);
        }
    }

    public void DisplayRipples(Vector3 contactPoint) {

        Debug.Log("Hit");
        var ripples = Instantiate(shieldRipples, transform);

        shieldRipplesVFX = ripples.GetComponent<VisualEffect>();
        shieldRipplesVFX.SetVector3("SphereCenter", contactPoint);

        Destroy(ripples, 2);
    } 
}
