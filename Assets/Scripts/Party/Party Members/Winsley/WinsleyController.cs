using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public class WinsleyController : CharacterController
    {
        private Winsley _winsley;

        private WinsleyPartyInput _partyInput;
        private WinsleyPlayerInput _playerInput;

        public WinsleyController(Winsley winsley)
        {
            _winsley = winsley;
            
            rb = _winsley.GetComponent<Rigidbody2D>();
            gameManager = GameStateManager.Instance;

            _partyInput = new WinsleyPartyInput(_winsley);
            _playerInput = new WinsleyPlayerInput(_winsley);

            provider = _winsley.inputProvider;

            provider.OnPrimary += OnPrimary_PrimaryCast;
            provider.OnSecondary += OnSecondary_SecondaryCast;
            provider.OnAuxMove += OnAuxMove_AuxillaryMovement;

            Party.OnPartyLeaderChanged += () =>
            {
                if (Party.Instance.previousLeader == _winsley.gameObject)
                {
                    movementDirection = Vector2.zero; 
                }
            };
        }

        public void Update()
        {
            movementDirection = provider.inputState.movementDirection;
            _isSprinting = provider.inputState.isSprinting;
            _partyInput.Update();
            
            Move(Time.fixedDeltaTime);
        }

        private void Move(float d)
        {
            if (gameManager.state != GameState.Main)
            {
                return;
            }

            if (movementDirection.Equals(new Vector2(0, 0)))
            {
                _winsley.movementState = MovementState.Idle;
                sprintDuration = 0f;
                isDashing = false;
                return;
            }

            if (_isSprinting)
            {
                if (sprintDuration >= _winsley.dashThreshold)
                {
                    _winsley.movementState = MovementState.Dash;
                    isDashing = true;
                    movementSp = _winsley.stats.manaport_stat_base_walk_speed.value * _winsley.stats.manaport_stat_base_dash_modifier.value;
                }
                else
                {
                    _winsley.movementState = MovementState.Sprint;
                    movementSp = _winsley.stats.manaport_stat_base_walk_speed.value * _winsley.stats.manaport_stat_base_sprint_modifier.value;
                    isDashing = false;
                }
                sprintDuration += Time.deltaTime;
            }
            else
            {
                _winsley.movementState = MovementState.Walk;
                movementSp = _winsley.stats.manaport_stat_base_walk_speed.value;
                sprintDuration = 0f;
            }

            position = _winsley.transform.position;

            angle = (float)(Mathf.Atan2(movementDirection.y, movementDirection.x));

            reconstructedMovement = new Vector2(Mathf.Cos(angle) * movementSp, Mathf.Sin(angle) * movementSp);
            
            rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d));
            resultPosition = _winsley.transform.position;

            targetPosition = new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d);

            targetDelta = targetPosition - initialPosition;
            actualDelta = resultPosition - initialPosition;

            initialPosition = _winsley.transform.position;
        }

        public void OnPrimary_PrimaryCast()
        {
            //casting.PrimaryCast();
        }

        public void OnSecondary_SecondaryCast()
        {
            //casting.SecondaryCast();
        }

        public void OnAuxMove_AuxillaryMovement()
        {
            //abilities.AuxMove();
        }
    }
}

