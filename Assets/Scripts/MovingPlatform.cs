using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public Transform target;
    public float duration;
    public float waitTime;
    public float delay;
    public bool childOverride;

    Vector3 start;
    Ease easeType = Ease.Linear;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        
        while(true)
        {
            start = transform.position;

            transform.DOMove(target.position, duration).SetEase(easeType);
            yield return StartCoroutine(Wait());

            transform.DOMove(start, duration).SetEase(easeType);
            yield return StartCoroutine(Wait());

            yield return null;
        }

        yield return null;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(waitTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !childOverride)
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !childOverride)
        {
            collision.transform.parent = null;
        }
    }
}
