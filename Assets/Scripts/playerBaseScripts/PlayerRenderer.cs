using Aarthificial.Reanimation;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    private static class Drivers {
        public const string STATE = "state";
        public const string MOVEMENT_STATE = "movementState";
        public const string FACING_STATE = "facingState";
    }

    private Reanimator _reanimator;
    private Player _player;
    private PlayerController _playerController;

    private int facingState;

    void Start() {
        _reanimator = GetComponent<Reanimator>();
        _player = GetComponentInParent<Player>();
        _playerController = GetComponentInParent<PlayerController>();

        facingState = 2;
    }

    void Update() {
        if (_playerController._movementDirection.Equals(new Vector2(0, 1))) { // north 
            facingState = 0; // north
        }else if (_playerController._movementDirection.Equals(new Vector2(1, 1))) { // northeast
            facingState = 0; 
        }else if (_playerController._movementDirection.Equals(new Vector2(1, 0))) { // east
            facingState = 1; // east
        }else if (_playerController._movementDirection.Equals(new Vector2(1, -1))) { // southeast
            facingState = 1;
        }else if (_playerController._movementDirection.Equals(new Vector2(0, -1))) { // south
            facingState = 2; // south
        }else if (_playerController._movementDirection.Equals(new Vector2(-1, -1))) { // southwest
            facingState = 2;
        }else if (_playerController._movementDirection.Equals(new Vector2(-1, 0))) { // west
            facingState = 3; // west
        }else if (_playerController._movementDirection.Equals(new Vector2(-1, 1))) { // northwest
            facingState = 3;
        }

        _reanimator.Set(Drivers.STATE, (int)_player.state);
        _reanimator.Set(Drivers.FACING_STATE, facingState);
        _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_player.movementType);
    }
}
