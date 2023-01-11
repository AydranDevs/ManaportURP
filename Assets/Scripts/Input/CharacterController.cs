using Manapotion.PartySystem;
using Manapotion.Utilities;
using Manapotion.Actions;
using UnityEngine;

namespace Manapotion.Input
{
    [System.Serializable]
    public class CharacterController
    {
        private PartyMember _member;
        [SerializeField]
        private InputProvider _inputProvider;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public MovementState movementState = MovementState.Idle;
        public DirectionState directionState = DirectionState.None;
        public FacingState facingState = FacingState.South;
        public Vector2 forwardVector = Vector2.down;
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

        public void Init(PartyMember member)
        {
            // not a monobehaviour, sub to the Update event
            ManaBehaviour.OnUpdate += Update;

            _member = member;

            foreach (var action in _member.actionsManagerScriptableObject.possibleActions)
            {
                // action.OnActionPerformedRestrictingMovementEvent += OnActionPerformedRestrictingMovementEvent_AddDriverListener;
                // action.OnActionConcludedRestrictingMovementEvent += OnActionConcludedRestrictingMovementEvent_AddDriverListener; 
            }

            _inputProvider.OnPrimary += OnPrimary;
            _inputProvider.OnSecondary += OnSecondary;
            _inputProvider.OnAux += OnAux;
        }

        private void Update()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                facingState = FacingState.South;
                return;
            }

            Move(Time.deltaTime);

            // Set the correct facing state
            if (_member.characterTargeting.currentlyTargeted != null)
            {
                facingState = _member.characterTargeting.GetFacingStateToTarget();
                switch (facingState)
                {
                    case FacingState.North: forwardVector = Vector2.up; break;
                    case FacingState.East: forwardVector = Vector2.right; break;
                    case FacingState.South: forwardVector = Vector2.down; break;
                    case FacingState.West: forwardVector = Vector2.left; break;
                }
            }
            else
            {
                switch (_inputProvider.GetState().movementDirection)
                {
                    case Vector2 dir when dir.Equals(new Vector2(0, 1)): 
                        facingState = FacingState.North;
                        forwardVector = Vector2.up;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(1, 1)): 
                        facingState = FacingState.North;
                        forwardVector = Vector2.up;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(1, 0)): 
                        facingState = FacingState.East;
                        forwardVector = Vector2.right;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(1, -1)): 
                        facingState = FacingState.East;
                        forwardVector = Vector2.right;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(0, -1)): 
                        facingState = FacingState.South;
                        forwardVector = Vector2.down;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(-1, -1)): 
                        facingState = FacingState.South;
                        forwardVector = Vector2.down;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(-1, 0)): 
                        facingState = FacingState.West;
                        forwardVector = Vector2.left;
                        break;
                    case Vector2 dir when dir.Equals(new Vector2(-1, 1)): 
                        facingState = FacingState.West;
                        forwardVector = Vector2.left;
                        break;
                }
            }

            if (_inputProvider.GetState().movementDirection.normalized == forwardVector)
            {
                directionState = DirectionState.Foward;
            }
            else if (_inputProvider.GetState().movementDirection.normalized == new Vector2(forwardVector.y, -forwardVector.x))
            {
                directionState = DirectionState.Right;
            }
            else if (_inputProvider.GetState().movementDirection.normalized == new Vector2(-forwardVector.y, forwardVector.x))
            {
                directionState = DirectionState.Left;
            }
            else if (Vector2.Dot(forwardVector, _inputProvider.GetState().movementDirection.normalized) == -1)
            {
                directionState = DirectionState.Backward;
            }
            else if (_inputProvider.GetState().movementDirection == Vector2.zero)
            {
                directionState = DirectionState.None;
            }
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
                    if (directionState != DirectionState.Foward && directionState != DirectionState.Backward)
                    {
                        movementState = MovementState.Strafe;
                    }
                    else
                    {
                        movementState = MovementState.Walk;
                    }

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
            
            
        }
        public void OnPrevious()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                return;
            }
            
            
        }
        #endregion

        public void OnActionPerformedRestrictingMovementEvent_AddDriverListener(object sender, PartyMemberAction.OnActionPerformedRestrictingMovementEventArgs e)
        {
            // when the watch driver event is called, grant control back to the character.
            _member.characterRenderer.GetReanimator().AddListener(e.watchDriver, () =>
                {
                    characterControllerRestriction = e.restrictionsToApply;
                }
            );
        }

        public void OnActionConcludedRestrictingMovementEvent_AddDriverListener(object sender, PartyMemberAction.OnActionConcludedRestrictingMovementEventArgs e)
        {
            // when the watch driver event is called, grant control back to the character.
            _member.characterRenderer.GetReanimator().AddListener(e.watchDriver, () =>
                {
                    characterControllerRestriction = CharacterControllerRestriction.NoRestrictions;
                }
            );
        }

        public void SetFacingState(FacingState state)
        {
            this.facingState = state;
        }
    }
}