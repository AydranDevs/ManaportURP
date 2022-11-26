using Aarthificial.Reanimation;
using Manapotion.PartySystem;
using Manapotion.Actions;
using UnityEngine;

namespace Manapotion.Rendering
{
    [System.Serializable]
    public class CharacterRenderer
    {
        private PartyMember _member;

        [UnityEngine.SerializeField]
        private Reanimator _reanimator;

        // drivers that are modified by this class
        private static class Drivers 
        {
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
        }

        public void Init(PartyMember member)
        {
            // not a monobehaviour, sub to the Update event
            ManaBehaviour.OnUpdate += Update;   
            
            _member = member;

            // subscribe to every action's state event
            foreach (var action in _member.actionsManagerScriptableObject.possibleActions)
            {
                action.OnActionPerformedEvent += OnActionPerformedEvent_UpdateDriver;
                action.OnActionConcludedEvent += OnActionConcludedEvent_UpdateDriver;
            }
        }

        private void Update()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                _member.facingState = FacingState.South;
            }

            if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, 1))) 
            { // north 
                _member.facingState = FacingState.North; // north
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                _member.facingState = FacingState.North; 
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 0)))
            { // east
                _member.facingState = FacingState.East; // east
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                _member.facingState = FacingState.East;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, -1)))
            { // south
                _member.facingState = FacingState.South; // south
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, -1))) 
            { // southwest
                _member.facingState = FacingState.South;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                _member.facingState = FacingState.West; // west
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 1))) 
            { // northwest
                _member.facingState = FacingState.West;
            }

            _reanimator.Set(Drivers.FACING_STATE, ((int)_member.facingState));
            _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_member.movementState);
        }

        public void OnActionPerformedEvent_UpdateDriver(object sender, ActionScriptableObject.OnActionPerformedEventArgs e)
        {
           for (int i = 0; i < e.actionPerformed_driverSetsArray.Length; i++)
           {
                _reanimator.Set(e.actionPerformed_driverSetsArray[i].driverName, e.actionPerformed_driverSetsArray[i].set);
                if (e.actionPerformed_driverSetsArray[i].watchDriver != "")
                {
                    Debug.Log(e.actionPerformed_driverSetsArray[i].watchDriver);
                    _reanimator.AddListener(
                        e.actionPerformed_driverSetsArray[i].watchDriver,
                        () => {
                            Debug.Log(e.actionPerformed_driverSetsArray[i].watchDriver);
                        }
                    );
                }
           }
        }

        public void OnActionConcludedEvent_UpdateDriver(object sender, ActionScriptableObject.OnActionConcludedEventArgs e)
        {
            for (int i = 0; i < e.actionConcluded_driverSetsArray.Length; i++)
            {
                _reanimator.Set(e.actionConcluded_driverSetsArray[i].driverName, e.actionConcluded_driverSetsArray[i].set);
            }
        }
    }
}