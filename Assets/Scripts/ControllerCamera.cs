using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourcePhoto;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject flash;
    [SerializeField] private float timeActivateFlash;
    public void ActivateCamera(Canvas panelTvStatic)
    {
        animator.SetBool("Activate",true);
        _audioSource.clip = _audioClip;
        _audioSource.Play();
    }
    public void DesactivateCamera()
    {
        _camera.gameObject.SetActive(false);
        _audioSource.Stop();
    }

    public void PhotoCapture()
    {
        flash.SetActive(true);
        _audioSourcePhoto.Play();
        StartCoroutine(timeFlash());
    }

    IEnumerator timeFlash()
    {
        yield return new WaitForSeconds(timeActivateFlash);
        flash.SetActive(false);
    }
}
