using System;
using System.Collections.Generic;
using Manapotion.PartySystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Manapotion.AStarPathfinding;
using Manapotion.Utilities;

namespace Manapotion.Input
{
    [System.Serializable]
    public class CharacterInput
    {
        public enum ControlType { AI, Player }
        public ControlType controlType = ControlType.AI;
        public enum AIControlMode { Idle, FollowTheLeader, Patrol }
        public AIControlMode aIControlMode = AIControlMode.FollowTheLeader;
        private Transform _leader;
        [SerializeField]
        private int _patrolRange = 7;
        [field: SerializeField]
        private int MAX_FRAMES_TO_CALC_PATH_TO_PATROL_POINT;
        [field: SerializeField]
        private int MIN_FRAMES_TO_CALC_PATH_TO_PATROL_POINT;

        private PartyMember _member;

        [SerializeField]
        private InputProvider _inputProvider;
        [SerializeField]
        private InputActionAsset _inputActionAsset;
        private InputActionMap _inputActionMap;

        private InputAction _movement;
        private InputAction _mouseMovement;
        private InputAction _sprint;
        private InputAction _primary;
        private InputAction _secondary;
        private InputAction _aux;
        private InputAction _next;
        private InputAction _previous;

        // Calculate a new path every 50 frames
        private const int FRAMES_TO_CALC_PATH_TO_LEADER = 50;
        private int _currentFrameTimerToCalcPathToPatrolPoint;
        private Vector3 _patrolPoint;
    
        private int _frames = 0;

        private WorldGrid _grid;
        private List<WorldTile> _path;
        private bool _goalReached = false;

        public void Init(PartyMember member)
        {
            // not a monobehaviour, sub to the Update event
            ManaBehaviour.OnUpdate += Update;

            _member = member;

            // Set up for Player control
            _inputActionMap = _inputActionAsset.FindActionMap("Player");

            _grid = GameObject.FindGameObjectWithTag("WorldGrid").GetComponent<WorldGrid>();

            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerMove, _movement, "Movement");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerMouseMove, _mouseMovement, "MousePosition");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerSprint, _sprint, "Sprint");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerPrimary, _primary, "PrimaryAction");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerSecondary, _secondary, "SecondaryAction");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerAuxillary, _aux, "AuxilaryAction");

            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerNext, _next, "Next");
            UtilitiesClass.CreateInputAction(_inputActionMap, PlayerPrevious, _previous, "Previous");
        }

        public void Update()
        {
            if (controlType == ControlType.Player)
            {
                return;
            }

            AIUpdate(aIControlMode);
        }
        #region Player Input
        // Update input provider movement direction
        public void PlayerMove(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }

            UpdateInputState(
                new InputState
                {
                    movementDirection = context.ReadValue<Vector2>(),
                    isSprinting = _inputProvider.GetState().isSprinting,
                    isDashing = _inputProvider.GetState().isDashing,
                    targetPos = _inputProvider.GetState().targetPos
                }
            );
        }
        // Update input provider target position
        public void PlayerMouseMove(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }

            var simpleTargetPos = context.ReadValue<Vector2>();

            UpdateInputState(
                new InputState
                {
                    movementDirection = _inputProvider.GetState().movementDirection,
                    isSprinting = _inputProvider.GetState().isSprinting,
                    isDashing = _inputProvider.GetState().isDashing,
                    targetPos = Camera.main.ScreenToWorldPoint(simpleTargetPos)
                }
            );
        }
        public void PlayerSprint(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }

            var sprint = false;
            // as long as sprint button is held, sprint = true
            if (!context.canceled)
            {
                sprint = true;
            }
            
            UpdateInputState(
                new InputState
                {
                    movementDirection = _inputProvider.GetState().movementDirection,
                    isSprinting = sprint,
                    isDashing = _inputProvider.GetState().isDashing,
                    targetPos =_inputProvider.GetState().targetPos
                }
            );
        }
        public void PlayerPrimary(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            _inputProvider.InvokePrimary();
        }
        public void PlayerSecondary(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            _inputProvider.InvokeSecondary();
        }
        public void PlayerAuxillary(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            _inputProvider.InvokeAux();
        }
        
        public void PlayerNext(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            _inputProvider.InvokeNext();
        }
        public void PlayerPrevious(InputAction.CallbackContext context)
        {
            if (controlType == ControlType.AI)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            _inputProvider.InvokePrevious();
        }
        #endregion
        
        #region AI Input
        int pathIndex = 0;
        private void AIUpdate(AIControlMode aIControlMode)
        {
            _frames++;
            if (aIControlMode == AIControlMode.Idle)
            {
                UpdateInputState(
                    new InputState
                    {
                        movementDirection = Vector2.zero,
                        isSprinting = false,
                        isDashing = false,
                        targetPos = _inputProvider.GetState().targetPos
                    }
                );
                return;
            }
            if (_frames >= FRAMES_TO_CALC_PATH_TO_LEADER && aIControlMode == AIControlMode.FollowTheLeader)
            {
                RecalculatePath(_leader.position);
                _frames = 0;
            }
            else if (_frames >= _currentFrameTimerToCalcPathToPatrolPoint && aIControlMode == AIControlMode.Patrol)
            {
                Patrol();
                RecalculatePath(_patrolPoint);
                _frames = 0;
            }

            // as long as there is a path, follow it
            if (_path != null && _path.Count > 0)
            {
                if (_goalReached)
                {
                    return;
                }

                // if too far away from leader, sprint.
                if (aIControlMode == AIControlMode.FollowTheLeader)
                {
                    if (Vector2.Distance(_member.transform.position, _leader.position) > Party.Instance.maxDistance)
                    {
                        UpdateInputState(
                            new InputState
                            {
                                movementDirection = _inputProvider.GetState().movementDirection,
                                isSprinting = true,
                                isDashing = _inputProvider.GetState().isDashing,
                                targetPos = _inputProvider.GetState().targetPos
                            }
                        );
                    }
                    else
                    {
                        UpdateInputState(
                            new InputState
                            {
                                movementDirection = _inputProvider.GetState().movementDirection,
                                isSprinting = false,
                                isDashing = _inputProvider.GetState().isDashing,
                                targetPos = _inputProvider.GetState().targetPos
                            }
                        );
                    }
                }

                // moving to tiles
                if (_grid.WorldPositionToTile(_member.transform.position) != _path[pathIndex])
                {
                    MoveToTile(_path[pathIndex]);
                    return;
                }
                if (pathIndex == _path.Count - 1)
                {
                    _inputProvider.inputState.movementDirection = Vector2.zero;
                    _goalReached = true;
                }
                else
                {
                    pathIndex++;
                    _goalReached = false;
                }
            }

        }

        private void RecalculatePath(Vector3 targetPos)
        {   
            _path = Pathfinding.FindPath(_grid, _member.transform.position, targetPos);
            
            _goalReached = false;
            pathIndex = 0;

            if (_path != null && _path.Count > 0)
            {
                if (_goalReached)
                {
                    return;
                }
                
                if (Vector2.Distance(_member.transform.position, _leader.position) > Party.Instance.maxDistance)
                {
                    UpdateInputState(
                        new InputState
                        {
                            movementDirection = _inputProvider.GetState().movementDirection,
                            isSprinting = true,
                            isDashing = _inputProvider.GetState().isDashing,
                            targetPos = _inputProvider.GetState().targetPos
                        }
                    );
                }
                else
                {
                    UpdateInputState(
                        new InputState
                        {
                            movementDirection = _inputProvider.GetState().movementDirection,
                            isSprinting = false,
                            isDashing = _inputProvider.GetState().isDashing,
                            targetPos = _inputProvider.GetState().targetPos
                        }
                    );
                }



                // runs while the char is not at the target
                if (_grid.WorldPositionToTile(_member.transform.position) != _path[pathIndex])
                {
                    MoveToTile(_path[pathIndex]);
                    return;
                }

                if (pathIndex == _path.Count - 1)
                {
                    _inputProvider.inputState.movementDirection = Vector2.zero;
                    _goalReached = true;
                }
                else
                {
                    pathIndex++;
                    _goalReached = false;
                }
            }
        }

        private void MoveToTile(WorldTile tile)
        {
            WorldTile currentTile = _grid.WorldPositionToTile(_member.transform.position);

            if (currentTile == tile)
            {
                return;
            }

            int x = 0, y = 0;

            if (currentTile.gridX > tile.gridX)
            {
                x = -1;
            }
            else if (currentTile.gridX < tile.gridX) 
            {
                x = 1;
            }
            // else
            // {
            //     _inputProvider.inputState.movementDirection.x = 0;
            // }

            if (currentTile.gridY > tile.gridY) 
            {
                y = -1;
            }
            else if (currentTile.gridY < tile.gridY) 
            {
                y = 1;
            }
            // else
            // {
            //     _inputProvider.inputState.movementDirection.y = 0;
            // }

            UpdateInputState(
                new InputState
                {
                    movementDirection = new Vector2(x, y),
                    isSprinting = _inputProvider.GetState().isSprinting,
                    isDashing = _inputProvider.GetState().isDashing,
                    targetPos = _inputProvider.GetState().targetPos
                }
            );
        }
        #endregion

        public InputState UpdateInputState(InputState inputState)
        {
            _inputProvider.inputState = inputState;
            return _inputProvider.GetState();
        }

        public void Patrol()
        {
            _currentFrameTimerToCalcPathToPatrolPoint = UnityEngine.Random.Range(MIN_FRAMES_TO_CALC_PATH_TO_PATROL_POINT, MAX_FRAMES_TO_CALC_PATH_TO_PATROL_POINT);
            _patrolPoint = GetNewPatrolPoint();
        }

        public Vector3 GetNewPatrolPoint()
        {
            int x = (int)UnityEngine.Random.Range(((int)_grid.WorldPositionToTile(_member.transform.position).worldPos.x - _patrolRange), ((int)_grid.WorldPositionToTile(_member.transform.position).worldPos.x + _patrolRange));
            int y = (int)UnityEngine.Random.Range(((int)_grid.WorldPositionToTile(_member.transform.position).worldPos.y - _patrolRange), ((int)_grid.WorldPositionToTile(_member.transform.position).worldPos.y + _patrolRange));
            return _grid.WorldPositionToTile(new Vector3(x, y, 0)).worldPos;
        }
    
        public void SetLeader(Transform leader)
        {
            _leader = leader;
        }
    
        public InputProvider GetInputProvider()
        {
            return _inputProvider;
        }
    }
}
