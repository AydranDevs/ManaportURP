using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Actions;
using Manapotion.AssetManagement;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    private float _lifeTime;
    [SerializeField]
    private float _lifeTimeSeconds;
    [SerializeField]
    private AnimationCurve _moveXCurve;
    [SerializeField]
    private AnimationCurve _moveYCurve;

    private Vector3 _initialPosition;

    /// <summary>
    /// Create a Damage Number
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damageInstance"></param>
    /// <returns></returns>
    public static DamageNumber Create(Vector3 position, DamageInstance damageInstance)
    {
        var damageNumber = Instantiate(GameAssets.i.PFDamageNumber, position, Quaternion.identity).GetComponent<DamageNumber>();
        damageNumber.Setup(damageInstance);

        return damageNumber;
    }

    private TextMeshPro _textMesh;

    private void Awake() {
        _textMesh = GetComponent<TextMeshPro>();        
    }

    public void Setup(DamageInstance damageInstance)
    {
        _initialPosition = transform.position;
        _textMesh.SetText(damageInstance.damageInstanceAmount.ToString());
        _lifeTime = 0;
    }

    private void Update() {
        _lifeTime += Time.deltaTime;
        if (_lifeTime >= _lifeTimeSeconds)
        {
            return;
        }

        transform.position = new Vector3(
            _initialPosition.x + _moveXCurve.Evaluate(_lifeTime),
            _initialPosition.y + _moveYCurve.Evaluate(_lifeTime),
            0f
        );
    }
}
