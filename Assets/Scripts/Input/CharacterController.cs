using Manapotion.PartySystem;
using Manapotion.Utilities;
using Manapotion.Actions;
using UnityEngine;

namespace Manapotion.Input
{
    public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
    public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
    public enum FacingState { North, East, South, West }

    [System.Serializable]
    public class CharacterController
    {
        private PartyMember _member;
        [SerializeField]
        private InputProvider _inputProvider;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public MovementState movementState = MovementState.Idle;
        public DirectionState directionState = DirectionState.South;
        public FacingState facingState = FacingState.South;
        public float dashThreshold = 8f; 

        public CharacterControllerRestriction characterControllerRestriction = CharacterControllerRestriction.NoRestrictions;

        private float _movementSp;

        private Vector2 _position;

        private Vector2 _resultPosition;
        private Vector2 _targetPosition;
        private Vector2 _initialPosition;
        private Vector2 _targetDelta;
        private Vector2 _actualDelta;

        private float _angle;
        private Vector2 _reconstructedMovement;

        private float _sprintDuration;

        private bool _noDiagonalMovement;

        public void Init(PartyMember member)
        {
            // not a monobehaviour, sub to the Update event
            ManaBehaviour.OnUpdate += Update;

            _member = member;

            foreach (var action in _member.actionsManagerScriptableObject.possibleActions)
            {
                action.OnActionPerformedRestrictingMovementEvent += OnActionPerformedRestrictingMovementEvent_AddDriverListener;
                action.OnActionConcludedRestrictingMovementEvent += OnActionConcludedRestrictingMovementEvent_AddDriverListener; 
            }

            _inputProvider.OnPrimary += OnPrimary;
            _inputProvider.OnSecondary += OnSecondary;
            _inputProvider.OnAux += OnAux;
        }

        private void Update()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }

            Move(Time.deltaTime);
        }

        #region Controlled by InputProvider
        private void Move(float deltaTime)
        {
            // If not moving, member is idle
            if (_inputProvider.GetState().movementDirection.Equals(Vector2.zero)) 
            {
                movementState = MovementState.Idle;
                _sprintDuration = 0f;
                _member.characterInput.UpdateInputState(
                    new InputState
                    {
                        movementDirection = _inputProvider.GetState().movementDirection,
                        isSprinting = _inputProvider.GetState().isSprinting,
                        isDashing = false,
                        targetPos = _inputProvider.GetState().targetPos
                    }
                );
                return;
            }

            if (!characterControllerRestriction.canWalk)
            {
                return;
            }

            if (_inputProvider.GetState().isSprinting && characterControllerRestriction.canSprint)
            {
                if (_sprintDuration >= dashThreshold && characterControllerRestriction.canDash) 
                {
                    movementState = MovementState.Dash;
                    _member.characterInput.UpdateInputState(
                        new InputState
                        {
                            movementDirection = _inputProvider.GetState().movementDirection,
                            isSprinting = _inputProvider.GetState().isSprinting,
                            isDashing = true,
                            targetPos = _inputProvider.GetState().targetPos
                        }
                    );
                    _movementSp = ManaMath.DexCalc_MoveSp(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue) * ManaMath.DexCalc_DshMod(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                }
                else
                {
                    movementState = MovementState.Sprint;
                    _movementSp = ManaMath.DexCalc_MoveSp(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue) * ManaMath.DexCalc_SprMod(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                    _member.characterInput.UpdateInputState(
                        new InputState
                        {
                            movementDirection = _inputProvider.GetState().movementDirection,
                            isSprinting = _inputProvider.GetState().isSprinting,
                            isDashing = false,
                            targetPos = _inputProvider.GetState().targetPos
                        }
                    );
                }
                _sprintDuration += deltaTime;
            }
            else
            {
                if (characterControllerRestriction.canWalk)
                {
                    movementState = MovementState.Walk;
                    _movementSp = ManaMath.DexCalc_MoveSp(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                    _sprintDuration = 0f;
                }
            }

            _angle = (float)(Mathf.Atan2(_inputProvider.GetState().movementDirection.y, _inputProvider.GetState().movementDirection.x));

            _reconstructedMovement = new Vector2(Mathf.Cos(_angle) * _movementSp, Mathf.Sin(_angle) * _movementSp);
            
            _rigidbody.MovePosition(new Vector2(_member.transform.position.x, _member.transform.position.y) + ((_reconstructedMovement * _movementSp) * deltaTime));
            _resultPosition = _member.transform.position;

            _targetPosition = new Vector2(_member.transform.position.x, _member.transform.position.y) + ((_reconstructedMovement * _movementSp) * deltaTime);

            _targetDelta = _targetPosition - _initialPosition;
            _actualDelta = _resultPosition - _initialPosition;

            _initialPosition = _member.transform.position;
        }

        public void OnPrimary()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            if (!characterControllerRestriction.canUsePrimary)
            {
                return;
            }

            _member.PerformMainAction(0);
        }

        public void OnSecondary()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            if (!characterControllerRestriction.canUseSecondary)
            {
                return;
            }

            _member.PerformMainAction(1);
        }

        public void OnAux()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            if (!characterControllerRestriction.canUseAux)
            {
                return;
            }

            // _abilities.AuxMove();
        }

        public void OnNext()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            
            _member.characterTargeting.NextTarget();
        }
        public void OnPrevious()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            
            _member.characterTargeting.PreviousTarget();
        }
        #endregion

        public void OnActionPerformedRestrictingMovementEvent_AddDriverListener(object sender, ActionScriptableObject.OnActionPerformedRestrictingMovementEventArgs e)
        {
            // when the watch driver event is called, grant control back to the character.
            _member.characterRenderer.GetReanimator().AddListener(e.watchDriver, () =>
                {
                    characterControllerRestriction = e.restrictionsToApply;
                }
            );
        }

        public void OnActionConcludedRestrictingMovementEvent_AddDriverListener(object sender, ActionScriptableObject.OnActionConcludedRestrictingMovementEventArgs e)
        {
            // when the watch driver event is called, grant control back to the character.
            _member.characterRenderer.GetReanimator().AddListener(e.watchDriver, () =>
                {
                    characterControllerRestriction = CharacterControllerRestriction.NoRestrictions;
                }
            );
        }
    }
}