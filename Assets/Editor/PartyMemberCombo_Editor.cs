using UnityEngine;
using UnityEditor;
using Manapotion.Actions;

[CustomEditor(typeof(PartyMemberCombo))]
[CanEditMultipleObjects]
public class PartyMemberCombo_Editor : Editor
{
    private PartyMemberCombo _partyMemberCombo;
    
    SerializedProperty requiredAction;

    SerializedProperty attacksList;
    private bool _attacksListExpanded = true;

    void OnEnable()
    {
        _partyMemberCombo = target as PartyMemberCombo;

        requiredAction = serializedObject.FindProperty("requiredAction");

        attacksList = serializedObject.FindProperty("attacksList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(requiredAction);
        _attacksListExpanded = EditorGUILayout.Foldout(_attacksListExpanded, "Attack List", true);

        // if list is expanded in the inspector, draw the list in a small box
        if (_attacksListExpanded)
        {
            var boxStyle = new GUIStyle("Box")
            {
                padding = new RectOffset(8, 8, 8, 8),
                margin = new RectOffset(8, 8, 8, 8)
            };

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(boxStyle);
            #region Draw the attackslist to the inspector
            for (int i = 0; i < _partyMemberCombo.attacksList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal(boxStyle);
                _partyMemberCombo.attacksList[i] = (PartyMemberMeleeAttack)EditorGUILayout.ObjectField($"Attack {i + 1}", _partyMemberCombo.attacksList[i], typeof(PartyMemberMeleeAttack), true);
                EditorGUILayout.EndHorizontal();
            }
            #endregion
            #region Add and remove elements from the attacksList
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Attack"))
            {
                _partyMemberCombo.attacksList.Add(_partyMemberCombo.attacksList[_partyMemberCombo.attacksList.Count - 1]);
            }
            if (GUILayout.Button("Remove Attack"))
            {
                _partyMemberCombo.attacksList.Remove(_partyMemberCombo.attacksList[_partyMemberCombo.attacksList.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(_partyMemberCombo);
        serializedObject.ApplyModifiedProperties();
    }
}