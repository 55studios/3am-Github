using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Camera _camera;

    public void ActivateCamera(Canvas panelTvStatic )
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
}
