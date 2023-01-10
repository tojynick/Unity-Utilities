using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Rotator
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Space space;
        [SerializeField] private RotationAxis axis;
        [SerializeField] private float angularVelocity;
        [SerializeField] private AngularVelocityType angularVelocityType;
        [SerializeField] private bool startRotatingOnStart = true;

        private bool _isRotating;
        private Vector3 _axis;

        public float AngularVelocity => angularVelocity;


        public void StartRotation()
        {
            _isRotating = true;
        }

        public void StopRotation()
        {
            _isRotating = false;
        }

        public void UpdateSpace(Space newSpace)
        {
            space = newSpace;
        }

        public void UpdateAxis(RotationAxis newAxis)
        {
            axis = newAxis;

            switch (axis)
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

        public void UpdateAngularVelocity(float newVelocity)
        {
            angularVelocity = newVelocity;
        }

        public void UpdateAngularVelocityType(AngularVelocityType newType)
        {
            angularVelocityType = newType;
        }

        private void Start()
        {
            UpdateAxis(axis);

            if (startRotatingOnStart)
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

            switch (angularVelocityType)
            {
                case AngularVelocityType.Turns:
                    transform.Rotate(_axis, angularVelocity * 360 * Time.deltaTime, space);
                    break;
                case AngularVelocityType.Radians:
                    transform.Rotate(_axis, angularVelocity * Mathf.Rad2Deg * Time.deltaTime, space);
                    break;
                case AngularVelocityType.Degrees:
                    transform.Rotate(_axis, angularVelocity * Time.deltaTime, space);
                    break;
                
                default:
                    transform.Rotate(_axis, angularVelocity * Time.deltaTime, space);
                    break;
            }
        }
    }
}