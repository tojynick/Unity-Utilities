using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RotationAxis
{
    X,Y,Z
}

public class Rotator : MonoBehaviour
{
    [SerializeField] private Space defaultSpace;
    [SerializeField] private RotationAxis defaultAxis;
    [SerializeField] private float speed;
    [SerializeField] private bool startRotatingOnStart = true;

    private bool _isRotating;
    private Vector3 _axis;
    
    public float Speed => speed;


    public void StartRotation()
    {
        _isRotating = true;
    }
    
    public void StopRotation()
    {
        _isRotating = false;
    }

    public void UpdateAxis(RotationAxis newAxis)
    {
        defaultAxis = newAxis;
        
        switch (defaultAxis)
        {
            case RotationAxis.X:
                _axis = Vector3.right;
                break;
                    
            case RotationAxis.Y:
                _axis = Vector3.up;
                break;
                    
            case RotationAxis.Z:
                _axis = Vector3.forward;
                break;
        }
    }

    private void Start()
    {
        UpdateAxis(defaultAxis);
        
        if(startRotatingOnStart)
            StartRotation();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!_isRotating)
            return;
        
        transform.Rotate(_axis, speed * Time.deltaTime, defaultSpace);
    }
}
