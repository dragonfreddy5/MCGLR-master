using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Vector3 checkpoint;
    public Color[] fadeColors;
    public Image fader;
    public AudioSource BGM;
    public AudioClip Winner;
    public AudioSource VE;
    public AudioClip[] VEClip;

    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public IEnumerator FadeToColor(int targetColor)
    {
        fader.DOColor(fadeColors[targetColor], 1f);
        yield return new WaitForSeconds(1.1f);
    }

    public void PlayVE(int index)
    {
        VE.clip = VEClip[index];
        VE.Play();
    }

    public IEnumerator StartGame()
    {
        yield return StartCoroutine(FadeToColor(0));
        SceneManager.LoadScene("SampleScene2");

    }

    public IEnumerator Win()
    {
        PlayerController.Instance.canMove = false;
        BGM.clip = Winner;
        BGM.Play();
        PlayVE(2);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeToColor(2));
        yield return StartCoroutine(FadeToColor(0));
    }

    public IEnumerator Respawn()
    {
        CameraController.Instance.cameraFree = false;
        PlayerController.Instance.canMove = false;
        PlayVE(0);
        yield return StartCoroutine(FadeToColor(0));

        // Move player back to last checkpoint
        PlayerController.Instance.transform.position = checkpoint;
        CameraController.Instance.cameraFree = true;
        CameraController.Instance.respawn = true;
        yield return StartCoroutine(FadeToColor(1));
        CameraController.Instance.respawn = false;
        PlayerController.Instance.canMove = true;
    }

}
