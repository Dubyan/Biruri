using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInput : MonoBehaviour
{
    private Rigidbody2D rb;
    private enum StateBox { normal, destroyed }
    private Animator anim;
    private StateBox State
    {
        get { return (StateBox)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.S))
        {
            State = StateBox.normal;
            StartCoroutine(DestroyBox());
        }

    }


    public void Destroying()
    {
            State = StateBox.normal;
            StartCoroutine(DestroyBox());
    }

    IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(0.1f);

        // Disable all BoxColliders2D on the object
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        State = StateBox.destroyed;
        rb.simulated = false;
        foreach (BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(1f);

        // Destroy the object
        Destroy(gameObject);
    }

}