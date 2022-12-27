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

    private void OnEnable()
    {
        _partyMemberMeleeAttack = target as PartyMemberMeleeAttack;

        requiredAction = serializedObject.FindProperty("requiredAction");

        attackMode = serializedObject.FindProperty("attackMode");
        comboParent = serializedObject.FindProperty("comboParent");

        costPointID = serializedObject.FindProperty("costPointID");
        cost = serializedObject.FindProperty("cost");

        modifierStatID = serializedObject.FindProperty("modifierStatID");
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
            #region Performed Driver Handles
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Performed Driver Handles", "The drivers that will be set in the member Reanimtor when this attack is performed."));
            
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

            #region Add and remove elements
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
            #endregion
            
            #region Attack End Driver Handle
            EditorGUILayout.LabelField(new GUIContent("Attack End Driver Handle", "This is the driver that when passed in the reanimator state, signifies the attack ending. This will likely be the control driver in whatever animation this attack plays last."));
            
            EditorGUILayout.BeginVertical(boxStyle);
                    
            _partyMemberMeleeAttack.attackEndDriverHandle.driverName = EditorGUILayout.DelayedTextField("Check", _partyMemberMeleeAttack.attackEndDriverHandle.driverName);
            _partyMemberMeleeAttack.attackEndDriverHandle.driverValue = EditorGUILayout.DelayedIntField("For", _partyMemberMeleeAttack.attackEndDriverHandle.driverValue);
            _partyMemberMeleeAttack.attackEndDriverHandle.conditional = false;
            
            EditorGUILayout.EndVertical();
            #endregion
            #region Attack End Driver Handles
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(new GUIContent("Attack End Driver Handles", "The drivers that will be set in the member Reanimtor when this attack ends."));

            for (int i = 0; i < _partyMemberMeleeAttack.endDriverHandles.Count; i++)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                EditorGUILayout.LabelField($"Driver Handle {i + 1}");
                
                EditorGUILayout.BeginVertical(boxStyle);
                
                _partyMemberMeleeAttack.endDriverHandles[i].driverName = EditorGUILayout.DelayedTextField("Set", _partyMemberMeleeAttack.endDriverHandles[i].driverName);
                _partyMemberMeleeAttack.endDriverHandles[i].driverValue = EditorGUILayout.DelayedIntField("To", _partyMemberMeleeAttack.endDriverHandles[i].driverValue);
                _partyMemberMeleeAttack.endDriverHandles[i].conditional = EditorGUILayout.Toggle("Conditional?", _partyMemberMeleeAttack.endDriverHandles[i].conditional);
                if (_partyMemberMeleeAttack.endDriverHandles[i].conditional)
                {
                    _partyMemberMeleeAttack.endDriverHandles[i].conditionalDriverName = EditorGUILayout.DelayedTextField("When", _partyMemberMeleeAttack.endDriverHandles[i].conditionalDriverName);
                    _partyMemberMeleeAttack.endDriverHandles[i].conditionalDriverValue = EditorGUILayout.DelayedIntField("Is", _partyMemberMeleeAttack.endDriverHandles[i].conditionalDriverValue);
                } 
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            #region Add and remove elements
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Driver Handle"))
            {
                _partyMemberMeleeAttack.endDriverHandles.Add(new DriverHandle());
            }
            if (GUILayout.Button("Remove Driver Handle"))
            {
                _partyMemberMeleeAttack.endDriverHandles.Remove(_partyMemberMeleeAttack.endDriverHandles[_partyMemberMeleeAttack.endDriverHandles.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.EndVertical();
            #endregion
        }

        EditorGUILayout.EndVertical();

        EditorUtility.SetDirty(_partyMemberMeleeAttack);
        serializedObject.ApplyModifiedProperties();
    }
}