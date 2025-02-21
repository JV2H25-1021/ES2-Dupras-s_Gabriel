using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SousmarinMouvement : MonoBehaviour
{
    [SerializeField] private float _vitesse;
    private Rigidbody _rb;
    private Vector3 directionInput;

    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnMouvement(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitesse;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }

    private void OnMouvementY(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitesse;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }
    private void FixedUpdate()
    {
        Vector3 mouvement = directionInput;

        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new Vector3(0f, _rb.velocity.y, _rb.velocity.x);
        _animator.SetFloat("vitesse", vitesseSurPlane.magnitude * _modifierAnimTranslation);
        _animator.SetFloat("Deplacement", vitesseSurPlane.magnitude);
    }
}
