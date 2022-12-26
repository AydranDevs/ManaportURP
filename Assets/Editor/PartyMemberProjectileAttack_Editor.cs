using UnityEngine;
using UnityEditor;
using Manapotion.Actions;
using Manapotion.Animation;

[CustomEditor(typeof(PartyMemberProjectileAttack))]
[CanEditMultipleObjects]
public class PartyMemberProjectileAttack_Editor : Editor
{
    private PartyMemberProjectileAttack _partyMemberProjectileAttack;

    SerializedProperty requiredAction;
    SerializedProperty projectileHandler;

    SerializedProperty attackMode;
    SerializedProperty comboParent;

    SerializedProperty costPointID;
    SerializedProperty cost;

    SerializedProperty modifierStatID;

    private bool _animationSettingsExpanded;
    SerializedProperty driverHandles;

    private void OnEnable()
    {
        _partyMemberProjectileAttack = target as PartyMemberProjectileAttack;

        requiredAction = serializedObject.FindProperty("requiredAction");
        projectileHandler = serializedObject.FindProperty("projectileHandler");

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
        if (_partyMemberProjectileAttack.attackMode == PartyMemberProjectileAttack.AttackMode.Combo_Attack)
        {
            EditorGUILayout.PropertyField(comboParent);
            EditorGUILayout.PropertyField(projectileHandler);
            EditorGUILayout.BeginHorizontal(boxStyle);
            _partyMemberProjectileAttack.modifierStatID = (Manapotion.Stats.StatID)EditorGUILayout.EnumPopup("Modifier Stat", _partyMemberProjectileAttack.modifierStatID);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.PropertyField(requiredAction);
            EditorGUILayout.PropertyField(projectileHandler);
            EditorGUILayout.BeginHorizontal(boxStyle);
            EditorGUILayout.LabelField("Cost");
            _partyMemberProjectileAttack.costPointID = (Manapotion.Stats.PointID)EditorGUILayout.EnumPopup(_partyMemberProjectileAttack.costPointID);
            EditorGUILayout.IntField(_partyMemberProjectileAttack.cost);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal(boxStyle);
            _partyMemberProjectileAttack.modifierStatID = (Manapotion.Stats.StatID)EditorGUILayout.EnumPopup("Modifier Stat", _partyMemberProjectileAttack.modifierStatID);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginVertical(boxStyle);
        _animationSettingsExpanded = EditorGUILayout.Foldout(_animationSettingsExpanded, "Animation Settings", true);

        if (_animationSettingsExpanded)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < _partyMemberProjectileAttack.driverHandles.Count; i++)
            {
                EditorGUILayout.BeginVertical(boxStyle);
                EditorGUILayout.LabelField($"Driver Handle {i + 1}");
                
                EditorGUILayout.BeginVertical(boxStyle);
                
                _partyMemberProjectileAttack.driverHandles[i].driverName = EditorGUILayout.DelayedTextField("Set", _partyMemberProjectileAttack.driverHandles[i].driverName);
                _partyMemberProjectileAttack.driverHandles[i].driverValue = EditorGUILayout.DelayedIntField("To", _partyMemberProjectileAttack.driverHandles[i].driverValue);
                _partyMemberProjectileAttack.driverHandles[i].conditional = EditorGUILayout.Toggle("Conditional?", _partyMemberProjectileAttack.driverHandles[i].conditional);
                if (_partyMemberProjectileAttack.driverHandles[i].conditional)
                {
                    _partyMemberProjectileAttack.driverHandles[i].conditionalDriverName = EditorGUILayout.DelayedTextField("When", _partyMemberProjectileAttack.driverHandles[i].conditionalDriverName);
                    _partyMemberProjectileAttack.driverHandles[i].conditionalDriverValue = EditorGUILayout.DelayedIntField("Is", _partyMemberProjectileAttack.driverHandles[i].conditionalDriverValue);
                } 
                
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndVertical();
            }

            #region Add and remove elements from the attacksList
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Driver Handle"))
            {
                _partyMemberProjectileAttack.driverHandles.Add(new DriverHandle());
            }
            if (GUILayout.Button("Remove Driver Handle"))
            {
                _partyMemberProjectileAttack.driverHandles.Remove(_partyMemberProjectileAttack.driverHandles[_partyMemberProjectileAttack.driverHandles.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}