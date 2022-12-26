using UnityEngine;
using UnityEditor;
using Manapotion.Actions;
using Manapotion.Animation;

[CustomEditor(typeof(PartyMemberLockOn))]
[CanEditMultipleObjects]
public class PartyMemberLockOn_Editor : Editor
{
    private PartyMemberLockOn _partyMemberLockOn;

    SerializedProperty performedRestriction;
    SerializedProperty activeRestriction;

    private bool _performedDriverHandlesExpanded;
    private bool _concludedDriverHandlesExpanded;
    
    SerializedProperty performedDriverHandles;
    SerializedProperty concludedDriverHandles;

    private void OnEnable()
    {
        _partyMemberLockOn = target as PartyMemberLockOn;

        performedRestriction = serializedObject.FindProperty("performedRestriction");
        activeRestriction = serializedObject.FindProperty("activeRestriction");

        performedDriverHandles = serializedObject.FindProperty("performedDriverHandles");
        concludedDriverHandles = serializedObject.FindProperty("concludedDriverHandles");
    }

    public override void OnInspectorGUI()
    {
        var boxStyle = new GUIStyle("Box")
        {
            padding = new RectOffset(8, 8, 8, 8),
            margin = new RectOffset(8, 8, 8, 8)
        };

        serializedObject.Update();

        EditorGUILayout.BeginVertical(boxStyle);

        #region Animation Settings
        EditorGUILayout.LabelField("Animation Settings");
        _performedDriverHandlesExpanded = EditorGUILayout.Foldout(_performedDriverHandlesExpanded, "Performed Driver Handles", true);
        
        if (_performedDriverHandlesExpanded)
        {
            EditorGUILayout.BeginVertical();
            
            for (int i = 0; i < _partyMemberLockOn.performedDriverHandles.Count; i++)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                EditorGUILayout.LabelField($"Driver Handle {i + 1}");
                
                EditorGUILayout.BeginVertical(boxStyle);
                
                _partyMemberLockOn.performedDriverHandles[i].driverName = EditorGUILayout.DelayedTextField("Set", _partyMemberLockOn.performedDriverHandles[i].driverName);
                _partyMemberLockOn.performedDriverHandles[i].driverValue = EditorGUILayout.DelayedIntField("To", _partyMemberLockOn.performedDriverHandles[i].driverValue);
                _partyMemberLockOn.performedDriverHandles[i].conditional = EditorGUILayout.Toggle("Conditional?", _partyMemberLockOn.performedDriverHandles[i].conditional);
                if (_partyMemberLockOn.performedDriverHandles[i].conditional)
                {
                    _partyMemberLockOn.performedDriverHandles[i].conditionalDriverName = EditorGUILayout.DelayedTextField("When", _partyMemberLockOn.performedDriverHandles[i].conditionalDriverName);
                    _partyMemberLockOn.performedDriverHandles[i].conditionalDriverValue = EditorGUILayout.DelayedIntField("Is", _partyMemberLockOn.performedDriverHandles[i].conditionalDriverValue);
                } 
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            #region Add and remove elements from the attacksList
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Performed Driver Handle"))
            {
                _partyMemberLockOn.performedDriverHandles.Add(new DriverHandle());
            }
            if (GUILayout.Button("Remove Performed Driver Handle"))
            {
                _partyMemberLockOn.performedDriverHandles.Remove(_partyMemberLockOn.performedDriverHandles[_partyMemberLockOn.performedDriverHandles.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion

            EditorGUILayout.EndVertical();
        }

        _concludedDriverHandlesExpanded = EditorGUILayout.Foldout(_concludedDriverHandlesExpanded, "Concluded Driver Handles", true);

        if (_concludedDriverHandlesExpanded)
        {
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < _partyMemberLockOn.concludedDriverHandles.Count; i++)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                EditorGUILayout.LabelField($"Driver Handle {i + 1}");
                
                EditorGUILayout.BeginVertical(boxStyle);
                
                _partyMemberLockOn.concludedDriverHandles[i].driverName = EditorGUILayout.DelayedTextField("Set", _partyMemberLockOn.concludedDriverHandles[i].driverName);
                _partyMemberLockOn.concludedDriverHandles[i].driverValue = EditorGUILayout.DelayedIntField("To", _partyMemberLockOn.concludedDriverHandles[i].driverValue);
                _partyMemberLockOn.concludedDriverHandles[i].conditional = EditorGUILayout.Toggle("Conditional?", _partyMemberLockOn.concludedDriverHandles[i].conditional);
                if (_partyMemberLockOn.concludedDriverHandles[i].conditional)
                {
                    _partyMemberLockOn.concludedDriverHandles[i].conditionalDriverName = EditorGUILayout.DelayedTextField("When", _partyMemberLockOn.concludedDriverHandles[i].conditionalDriverName);
                    _partyMemberLockOn.concludedDriverHandles[i].conditionalDriverValue = EditorGUILayout.DelayedIntField("Is", _partyMemberLockOn.concludedDriverHandles[i].conditionalDriverValue);
                } 
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            #region Add and remove elements from the attacksList
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Concluded Driver Handle"))
            {
                _partyMemberLockOn.concludedDriverHandles.Add(new DriverHandle());
            }
            if (GUILayout.Button("Remove Concluded Driver Handle"))
            {
                _partyMemberLockOn.concludedDriverHandles.Remove(_partyMemberLockOn.concludedDriverHandles[_partyMemberLockOn.concludedDriverHandles.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            
            EditorGUILayout.EndVertical();
        }
        
        EditorGUILayout.EndVertical();
        #endregion
        #region Restriction Settings
        EditorGUILayout.BeginVertical(boxStyle);
        EditorGUILayout.LabelField("Restriction Settings");

        EditorGUILayout.PropertyField(performedRestriction);
        EditorGUILayout.PropertyField(activeRestriction);
        EditorGUILayout.EndVertical();
        #endregion
        
        serializedObject.ApplyModifiedProperties();
    }
}