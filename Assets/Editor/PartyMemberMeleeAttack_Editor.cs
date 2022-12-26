using UnityEngine;
using UnityEditor;
using Manapotion.Actions;
using Manapotion.Animation;

[CustomEditor(typeof(PartyMemberMeleeAttack))]
[CanEditMultipleObjects]
public class PartyMemberMeleeAttack_Editor : Editor
{
    private PartyMemberMeleeAttack _partyMemberMeleeAttack;

    SerializedProperty requiredAction;

    SerializedProperty attackMode;
    SerializedProperty comboParent;

    SerializedProperty costPointID;
    SerializedProperty cost;

    SerializedProperty modifierStatID;

    private bool _animationSettingsExpanded;
    SerializedProperty driverHandles;

    private void OnEnable()
    {
        _partyMemberMeleeAttack = target as PartyMemberMeleeAttack;

        requiredAction = serializedObject.FindProperty("requiredAction");

        attackMode = serializedObject.FindProperty("attackMode");
        comboParent = serializedObject.FindProperty("comboParent");

        costPointID = serializedObject.FindProperty("costPointID");
        cost = serializedObject.FindProperty("cost");

        modifierStatID = serializedObject.FindProperty("modifierStatID");

        driverHandles = serializedObject.FindProperty("driverHandles");
    }

    public override void OnInspectorGUI()
    {
        var boxStyle = new GUIStyle("Box")
        {
            padding = new RectOffset(8, 8, 8, 8),
            margin = new RectOffset(8, 8, 8, 8)
        };

        serializedObject.Update();

        EditorGUILayout.PropertyField(attackMode);
        if (_partyMemberMeleeAttack.attackMode == PartyMemberMeleeAttack.AttackMode.Combo_Attack)
        {
            EditorGUILayout.PropertyField(comboParent);
            EditorGUILayout.BeginHorizontal(boxStyle);
            _partyMemberMeleeAttack.modifierStatID = (Manapotion.Stats.StatID)EditorGUILayout.EnumPopup("Modifier Stat", _partyMemberMeleeAttack.modifierStatID);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.PropertyField(requiredAction);
            EditorGUILayout.BeginHorizontal(boxStyle);
            EditorGUILayout.LabelField("Cost");
            _partyMemberMeleeAttack.costPointID = (Manapotion.Stats.PointID)EditorGUILayout.EnumPopup(_partyMemberMeleeAttack.costPointID);
            EditorGUILayout.IntField(_partyMemberMeleeAttack.cost);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal(boxStyle);
            _partyMemberMeleeAttack.modifierStatID = (Manapotion.Stats.StatID)EditorGUILayout.EnumPopup("Modifier Stat", _partyMemberMeleeAttack.modifierStatID);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginVertical(boxStyle);
        _animationSettingsExpanded = EditorGUILayout.Foldout(_animationSettingsExpanded, "Animation Settings", true);

        if (_animationSettingsExpanded)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < _partyMemberMeleeAttack.performedDriverHandles.Count; i++)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                EditorGUILayout.LabelField($"Driver Handle {i + 1}");
                
                EditorGUILayout.BeginVertical(boxStyle);
                
                _partyMemberMeleeAttack.performedDriverHandles[i].driverName = EditorGUILayout.DelayedTextField("Set", _partyMemberMeleeAttack.performedDriverHandles[i].driverName);
                _partyMemberMeleeAttack.performedDriverHandles[i].driverValue = EditorGUILayout.DelayedIntField("To", _partyMemberMeleeAttack.performedDriverHandles[i].driverValue);
                _partyMemberMeleeAttack.performedDriverHandles[i].conditional = EditorGUILayout.Toggle("Conditional?", _partyMemberMeleeAttack.performedDriverHandles[i].conditional);
                if (_partyMemberMeleeAttack.performedDriverHandles[i].conditional)
                {
                    _partyMemberMeleeAttack.performedDriverHandles[i].conditionalDriverName = EditorGUILayout.DelayedTextField("When", _partyMemberMeleeAttack.performedDriverHandles[i].conditionalDriverName);
                    _partyMemberMeleeAttack.performedDriverHandles[i].conditionalDriverValue = EditorGUILayout.DelayedIntField("Is", _partyMemberMeleeAttack.performedDriverHandles[i].conditionalDriverValue);
                } 
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            #region Add and remove elements from the attacksList
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Driver Handle"))
            {
                _partyMemberMeleeAttack.performedDriverHandles.Add(new DriverHandle());
            }
            if (GUILayout.Button("Remove Driver Handle"))
            {
                _partyMemberMeleeAttack.performedDriverHandles.Remove(_partyMemberMeleeAttack.performedDriverHandles[_partyMemberMeleeAttack.performedDriverHandles.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}