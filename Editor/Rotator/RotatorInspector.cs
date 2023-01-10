using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utilities.Rotator
{
    
    [CustomEditor(typeof(Rotator))]
    public class RotatorInspector : Editor
    {
        #region Serialized Properties

        private SerializedObject _so;
        private SerializedProperty _spaceProperty;
        private SerializedProperty _axisProperty;
        private SerializedProperty _angularVelocityProperty;
        private SerializedProperty _angularVelocityTypeProperty;
        private SerializedProperty _startRotatingOnStartProperty;

        #endregion

        #region Icons

        private Texture _playIcon;
        private Texture _stopIcon;

        #endregion

        private Rotator _rotator;


        private void OnEnable()
        {
            InitializeProps();
            LoadIcons();
        }

        private void InitializeProps()
        {
            _so = new SerializedObject(target);
            _spaceProperty = _so.FindProperty("space");
            _axisProperty = _so.FindProperty("axis");
            _angularVelocityProperty = _so.FindProperty("angularVelocity");
            _angularVelocityTypeProperty = _so.FindProperty("angularVelocityType");
            _startRotatingOnStartProperty = _so.FindProperty("startRotatingOnStart");
        }

        private void LoadIcons()
        {
            _playIcon = AssetDatabase.LoadAssetAtPath<Texture>(RotatorIconPaths.PLAY_ICON_PATH);
            _stopIcon = AssetDatabase.LoadAssetAtPath<Texture>(RotatorIconPaths.STOP_ICON_PATH);
        }

        public override void OnInspectorGUI()
        {
            _rotator = target as Rotator;

            _so.Update();
            
            DrawSpaceToggle();
            DrawAxisToggle();
            DrawAngularVelocitySettings();

            _startRotatingOnStartProperty.boolValue = EditorGUILayout.Toggle("Start Rotating On Start?", _startRotatingOnStartProperty.boolValue);

            _so.ApplyModifiedProperties();

            DrawPlayStopButtons();
        }

        private void DrawSpaceToggle()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Space");
                EditorGUI.BeginChangeCheck();
                
                if (GUILayout.Toggle(_spaceProperty.enumValueIndex == (int)Space.Self, new GUIContent("Self"),
                        EditorStyles.miniButtonLeft, GUILayout.MinWidth(RotatorInspectorStyles.HORIZONTAL_TOGGLE_MIN_WIDTH)))
                {
                    _spaceProperty.enumValueIndex = (int)Space.Self;
                }

                if (GUILayout.Toggle(_spaceProperty.enumValueIndex == (int)Space.World, new GUIContent("World"),
                        EditorStyles.miniButtonRight, GUILayout.MinWidth(RotatorInspectorStyles.HORIZONTAL_TOGGLE_MIN_WIDTH)))
                {
                    _spaceProperty.enumValueIndex = (int)Space.World;
                }
            }
        }

        private void DrawAxisToggle()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Axis");
                EditorGUI.BeginChangeCheck();
                
                if (GUILayout.Toggle(_axisProperty.enumValueIndex == (int)RotationAxis.X, new GUIContent("X"),
                        EditorStyles.miniButtonLeft, GUILayout.MinWidth(RotatorInspectorStyles.HORIZONTAL_TOGGLE_MIN_WIDTH)))
                {
                    _axisProperty.enumValueIndex = (int)RotationAxis.X;
                }

                if (GUILayout.Toggle(_axisProperty.enumValueIndex == (int)RotationAxis.Y, new GUIContent("Y"), 
                        EditorStyles.miniButtonMid, GUILayout.MinWidth(RotatorInspectorStyles.HORIZONTAL_TOGGLE_MIN_WIDTH)))
                {
                    _axisProperty.enumValueIndex = (int)RotationAxis.Y;
                }

                if (GUILayout.Toggle(_axisProperty.enumValueIndex == (int)RotationAxis.Z, new GUIContent("Z"), 
                        EditorStyles.miniButtonRight, GUILayout.MinWidth(RotatorInspectorStyles.HORIZONTAL_TOGGLE_MIN_WIDTH)))
                {
                    _axisProperty.enumValueIndex = (int)RotationAxis.Z;
                }
                
                if(EditorGUI.EndChangeCheck())
                    _rotator.UpdateAxis((RotationAxis)_axisProperty.enumValueIndex);
            }
        }

        private void DrawAngularVelocitySettings()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(_angularVelocityProperty);
                _angularVelocityTypeProperty.enumValueIndex = (int)(AngularVelocityType)EditorGUILayout.EnumPopup((AngularVelocityType) _angularVelocityTypeProperty.enumValueIndex,
                    GUILayout.Width(80));
                GUILayout.Label("per second.");
            }
        }

        private void DrawPlayStopButtons()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if(GUILayout.Button(new GUIContent("Start Rotation", _playIcon), RotatorInspectorStyles.GetButtonStyle()))
                    _rotator.StartRotation();

                if(GUILayout.Button(new GUIContent("Stop Rotation", _stopIcon), RotatorInspectorStyles.GetButtonStyle()))
                    _rotator.StopRotation();
            }
        }
    }
}
