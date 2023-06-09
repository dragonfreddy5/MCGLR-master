using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("Player died!!");
            yield return StartCoroutine(GameManager.Instance.Respawn());
        }
    }
}
