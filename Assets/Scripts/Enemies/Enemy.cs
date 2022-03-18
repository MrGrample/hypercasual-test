using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject waypointAttached;

    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject healthBarUI;


    [SerializeField] float maxHealth;
    float currentHealth;

    [SerializeField] float bodyDestructionTime = 10.0f;

    [SerializeField] WaypointManager waypointManager;

    [SerializeField] float animationChangeTime = 5.0f;
    Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthSlider.value = CalculateHealth();
        setRigidbodyState(true);
        setColliderState(false);
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeAnimation());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && waypointManager.GetCurrentWaypoint().gameObject == waypointAttached)
        {
            healthBarUI.SetActive(true);

            currentHealth -= collision.gameObject.GetComponent<Bullet>().Damage;

            if (currentHealth <= 0)
            {
                Die();
            }

            healthSlider.value = CalculateHealth();
        }      
    }

    private float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    private void Die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false) ;
        setColliderState(true);
        healthBarUI.SetActive(false);
        waypointAttached.GetComponent<Waypoint>().EnemyCount--;
        Invoke("DestroyObject", bodyDestructionTime);
    }

    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }

    private void setRigidbodyState(bool state)
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }

    private void setColliderState(bool state)
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;

    }

    IEnumerator ChangeAnimation()
    {
        while (true)
        {
            animator.SetInteger("AnimationSelect", Random.Range(0, 4));
            yield return new WaitForSeconds(animationChangeTime);
        }
    }

}
