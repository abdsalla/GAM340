﻿using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;

[System.Serializable]
    public class WeightedDrops
    {
        [SerializeField, Tooltip("The item drop selected by this choice")]
        public Object value;
        [SerializeField, Tooltip("The chance to select this value")]
        public int chance = 1;
    }

    [CustomPropertyDrawer(typeof(WeightedDrops))]
    public class WeightedDropDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var objectRect = new Rect(position.x, position.y, position.width - 40f, position.height);
            var chanceRect = new Rect(position.x + position.width - 40f, position.y, 40f, position.height);

            EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("value"), GUIContent.none);
            EditorGUI.PropertyField(chanceRect, property.FindPropertyRelative("chance"), GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }