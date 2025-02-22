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

    [SerializeField] private GameObject _sousmarin;

    [SerializeField] private float _modifierAnimTranslation;
    private Animator _animator;
    private Vector3 _vitesseSurPlane;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Deplacement", false);
        _animator.SetBool("Altitude", false);
    }

    private void OnPressAcceleration() 
    {
        _vitesse += 0.5f;
    }

    private void OnReleaseAcceleration()
    {
        _vitesse -= 0.5f;
    }

    private void OnMouvementY(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitesse;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);

        if (directionAvecVitesse.y != 0f)
        {
            _animator.SetBool("Altitude", true);
        }
        else
        {
            _animator.SetBool("Altitude", false);
        }
    }

    private void OnMouvementZ(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitesse;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);

        if (directionAvecVitesse.x != 0f)
        {
            _animator.SetBool("Deplacement", true);
        }
        else
        {
            _animator.SetBool("Deplacement", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 mouvement = directionInput;

        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        _vitesseSurPlane = new Vector3(0f, _rb.velocity.y, _rb.velocity.z);
        _animator.SetFloat("Vitesse", _vitesseSurPlane.magnitude * _modifierAnimTranslation);

    }
}
