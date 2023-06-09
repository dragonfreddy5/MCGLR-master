using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true)
        {
            if (Input.anyKey)
            {
                yield return StartCoroutine(GameManager.Instance.StartGame());
            }
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
