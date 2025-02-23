using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    private void OnAcceleration(InputValue context)
    {
        //Sur appuis de la touche "Shift" augmenter l'acceleration du Sousmarin
        if (context.isPressed) 
        {
            _acceleration+=2;
        }
        else 
        {
            _acceleration--;
        }        
    }

    private void OnMouvementY(InputValue directionBase)
    {
        //Application des données recueillis pour faire monter ou descendre le Sousmarin
        Vector2 directionAvecVitesse = (directionBase.Get<Vector2>() * _vitesseY)*_acceleration;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);
    }

    private void OnMouvementZ(InputValue directionBase)
    {
        //Application des données recueillis pour faire avancer ou reculer le Sousmarin
        Vector2 directionAvecVitesse = (directionBase.Get<Vector2>() * _vitesseX)*_acceleration;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.x);

    }

    private void FixedUpdate()
    {
        //Application de la force au Sousmarin
        Vector3 mouvement = directionInput;
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        //Obtenir les données de force et les transférer en Vector3
        Vector3 vitesseSurPlane = new (0f, _rb.velocity.y, _rb.velocity.z);
        
        //Gestion des animations

        //Appliquer une transition sur les animations selon leurs axes 
        _animator.SetFloat("VitesseZ", vitesseSurPlane.z * _modifierAnimTranslation);
        _animator.SetFloat("DeplacementZ", vitesseSurPlane.z * _modifierAnimTranslation);

        _animator.SetFloat("VitesseY", vitesseSurPlane.y * _modifierAnimTranslation);
        _animator.SetFloat("AltitudeY", vitesseSurPlane.y * _modifierAnimTranslation);

    }
}
