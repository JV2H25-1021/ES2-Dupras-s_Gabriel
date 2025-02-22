using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SousmarinMouvement : MonoBehaviour
{
    [SerializeField] private float _vitesseX;
    [SerializeField] private float _vitesseY;
    private float _acceleration = 1f;
    private Rigidbody _rb;
    private Vector3 directionInput;

    [SerializeField] private GameObject _sousmarin;

    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    
    }

    private void OnAcceleration( )
    {
        
    }

    private void OnMouvementY(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = (directionBase.Get<Vector2>() * _vitesseY)*_acceleration;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }

    private void OnMouvementZ(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = (directionBase.Get<Vector2>() * _vitesseX)*_acceleration;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);

    }

    private void FixedUpdate()
    {
        Vector3 mouvement = directionInput;

        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new (0f, _rb.velocity.y, _rb.velocity.z);
        _animator.SetFloat("VitesseX", vitesseSurPlane.z * _modifierAnimTranslation);
        _animator.SetFloat("VitesseY", vitesseSurPlane.y * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementX", vitesseSurPlane.z * _modifierAnimTranslation);
        _animator.SetFloat("AltitudeY", vitesseSurPlane.y * _modifierAnimTranslation);

    }
}
