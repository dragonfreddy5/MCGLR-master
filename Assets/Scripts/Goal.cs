using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Animator anim;
    private IEnumerator OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("Player won!!");
            anim.Play("WinParticle");
            yield return StartCoroutine(GameManager.Instance.Win());
        }
    }
}
