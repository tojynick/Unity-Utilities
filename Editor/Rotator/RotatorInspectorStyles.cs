using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utilities.Rotator
{
    public static class RotatorInspectorStyles
    {
        public const int HORIZONTAL_TOGGLE_MIN_WIDTH = 40;
        
        public static GUIStyle GetButtonStyle()
        {
            GUIStyle result = EditorStyles.miniButton;
            result.fixedHeight = 25;

            return result;
        }
    }
}
