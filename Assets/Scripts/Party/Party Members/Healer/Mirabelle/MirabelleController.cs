using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public class MirabelleController : CharacterController
    {
        private Mirabelle _mirabelle;

        // private MirabellePartyInput _partyInput;
        // private MirabellePlayerInput _playerInput;

        public MirabelleController(Mirabelle mirabelle)
        {
            _mirabelle = mirabelle;
            
            rb = _mirabelle.GetComponent<Rigidbody2D>();
            gameManager = GameStateManager.Instance;

            // _partyInput = new MirabellePartyInput(_mirabelle);
            // _playerInput = new MirabellePlayerInput(_mirabelle);

            provider = _mirabelle.inputProvider;

            provider.OnPrimary += OnPrimary_PrimaryCast;
            provider.OnAux += OnAuxMove_AuxillaryMovement;

            Party.OnPartyLeaderChanged += () =>
            {
                if (Party.Instance.previousLeader == _mirabelle.gameObject)
                {
                    movementDirection = Vector2.zero; 
                }
            };
        }

        public void Update()
        {
            // _partyInput.Update();
            movementDirection = provider.inputState.movementDirection;
            _isSprinting = provider.inputState.isSprinting;

            if (_mirabelle.umbrellaState == UmbrellaState.OpeningUmbrella || _mirabelle.umbrellaState == UmbrellaState.ClosingUmbrella) { movementDirection = new Vector2(0, 0); }
            if (_mirabelle.umbrellaState == UmbrellaState.UmbrellaOpened) 
            {
                _isSprinting = false;
            }

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
                _mirabelle.movementState = MovementState.Idle;
                sprintDuration = 0f;
                isDashing = false;
                return;
            }

            if (_isSprinting)
            {
                if (sprintDuration >= _mirabelle.dashThreshold)
                {
                    _mirabelle.movementState = MovementState.Dash;
                    isDashing = true;
                    movementSp = ManaMath.DexCalc_MoveSp(_mirabelle.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue) * ManaMath.DexCalc_DshMod(_mirabelle.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                }
                else
                {
                    _mirabelle.movementState = MovementState.Sprint;
                    movementSp = ManaMath.DexCalc_MoveSp(_mirabelle.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue) * ManaMath.DexCalc_SprMod(_mirabelle.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);;
                    isDashing = false;
                }
                sprintDuration += Time.deltaTime;
            }
            else
            {
                _mirabelle.movementState = MovementState.Walk;
                movementSp = ManaMath.DexCalc_MoveSp(_mirabelle.statsManagerScriptableObject.GetStat(Stats.StatID.DEX).value.modifiedValue);
                sprintDuration = 0f;
            }

            position = _mirabelle.gameObject.transform.position;

            angle = (float)(Mathf.Atan2(movementDirection.y, movementDirection.x));

            reconstructedMovement = new Vector2(Mathf.Cos(angle) * movementSp, Mathf.Sin(angle) * movementSp);
            
            rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d));
            resultPosition = _mirabelle.transform.position;

            targetPosition = new Vector2(position.x, position.y) + ((reconstructedMovement * movementSp) * d);

            targetDelta = targetPosition - initialPosition;
            actualDelta = resultPosition - initialPosition;

            initialPosition = _mirabelle.transform.position;
        }

        public void OnPrimary_PrimaryCast()
        {
            _mirabelle.mirabelleHealing.Heal();
        }

        public void OnAuxMove_AuxillaryMovement()
        {
            //abilities.AuxMove();
        }
    }
}

