using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Manapotion.Actions;

[CustomEditor(typeof(PartyMemberActionsManager))]
[CanEditMultipleObjects]
public class ActionsManagerScriptableObject_Editor : Editor
{
    private PartyMemberActionsManager _actionsManagerScriptableObject;

    private void OnEnable()
    {
        _actionsManagerScriptableObject = target as PartyMemberActionsManager;
    }

    public override void OnInspectorGUI()
    {
        var boxStyle = new GUIStyle("Box")
        {
            padding = new RectOffset(8, 8, 8, 8),
            margin = new RectOffset(8, 8, 8, 8)
        };

        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(boxStyle);
        EditorGUILayout.LabelField("Possible Actions");
        for (int i = 0; i < _actionsManagerScriptableObject.possibleActions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal(boxStyle);
            _actionsManagerScriptableObject.possibleActions[i] = (APartyMemberAction)EditorGUILayout.ObjectField($"{_actionsManagerScriptableObject.possibleActions[i]?.GetActionID()}", _actionsManagerScriptableObject.possibleActions[i], typeof(APartyMemberAction), true);
            EditorGUILayout.EndHorizontal();
        }
        #region Add and remove elements from possibleActions
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Action"))
        {
            _actionsManagerScriptableObject.possibleActions.Add(_actionsManagerScriptableObject.possibleActions[_actionsManagerScriptableObject.possibleActions.Count - 1]);
        }
        if (GUILayout.Button("Remove Action"))
        {
            _actionsManagerScriptableObject.possibleActions.Remove(_actionsManagerScriptableObject.possibleActions[_actionsManagerScriptableObject.possibleActions.Count - 1]);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}