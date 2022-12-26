// using UnityEngine;
// using UnityEditor;
// using Manapotion.Actions;

// [CustomEditor(typeof(ActionScriptableObject))]
// [CanEditMultipleObjects]
// public class ActionScriptableObject_Editor : Editor
// {
//     SerializedProperty requiredAction;
//     SerializedProperty projectileHandler;

//     SerializedProperty action_id;

//     SerializedProperty isToggled;

//     SerializedProperty whenPerformedWillRestrictUntilEvent;
//     SerializedProperty driverToWatchToUnrestrictAfterPerformed;

//     SerializedProperty _restrictionsAppliedAfterPerformedUntilDriver;
//     SerializedProperty _restrictionsAppliedWhileActive;
//     SerializedProperty _restrictionsAppliedAfterConcludedUntilDriver;


//     void OnEnable()
//     {
//         requiredAction = serializedObject.FindProperty("requiredAction");
//         projectileHandler = serializedObject.FindProperty("projectileHandler");

//         action_id = serializedObject.FindProperty("action_id");

//         isToggled = serializedObject.FindProperty("isToggled");

//         whenPerformedWillRestrictUntilEvent = serializedObject.FindProperty("whenPerformedWillRestrictUntilEvent");
//         driverToWatchToUnrestrictAfterPerformed = serializedObject.FindProperty("driverToWatchToUnrestrictAfterPerformed");
//     }

//     public override void OnInspectorGUI()
//     {
//         var actionScriptableObject = target as ActionScriptableObject;

//         serializedObject.Update();
//         EditorGUILayout.PropertyField(requiredAction);
//         EditorGUILayout.PropertyField(projectileHandler);
//         EditorGUILayout.PropertyField(action_id);
//         EditorGUILayout.PropertyField(isToggled);
        
//         if (actionScriptableObject.isToggled)
//         {
//             EditorGUILayout.PropertyField(whenPerformedWillRestrictUntilEvent);

//             if (actionScriptableObject.whenPerformedWillRestrictUntilEvent)
//             {
//                 EditorGUILayout.PropertyField(driverToWatchToUnrestrictAfterPerformed);
//             }
//         }
        
//         serializedObject.ApplyModifiedProperties();
//     }
// }