using Manapotion.PartySystem;
using Manapotion.Utilities;
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

            Move(Time.fixedDeltaTime);
        }

        private void Move(float deltaTime)
        {
            // If not moving, member is idle
            if (_inputProvider.GetState().movementDirection.Equals(Vector2.zero)) 
            {
                _member.movementState = MovementState.Idle;
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

            if (_inputProvider.GetState().isSprinting)
            {
                if (_sprintDuration >= _member.dashThreshold) 
                {
                    _member.movementState = MovementState.Dash;
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
                    _member.movementState = MovementState.Sprint;
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
                _member.movementState = MovementState.Walk;
                _movementSp = ManaMath.DexCalc_MoveSp(_member.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                _sprintDuration = 0f;
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

            _member.PerformMainAction(0);
        }

        public void OnSecondary()
        {
            if (GameStateManager.Instance.state != GameState.Main)
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

            // _abilities.AuxMove();
        }
    }
}