using System.Collections;
using MegaManBattleNetwork;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    private readonly float _interval = 0.07f;
    private Battler _battler;
    private float _duration;
    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;
    private Coroutine _flashRoutine;

    private void Start()
    {
        _battler = GetComponent<Battler>();
        _duration = _battler.DamageTakenCooldown;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    public void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        StartCoroutine(SwapMaterialRoutine());
        yield return new WaitForSeconds(_duration);
        _spriteRenderer.material = _originalMaterial;
        _flashRoutine = null;
        StopAllCoroutines();
    }

    private IEnumerator SwapMaterialRoutine()
    {
        if (_spriteRenderer.material == _originalMaterial)
        {
            _spriteRenderer.material = _flashMaterial;
        }
        else
        {
            _spriteRenderer.material = _originalMaterial;
        }

        yield return new WaitForSeconds(_interval);
        StartCoroutine(SwapMaterialRoutine());
    }
}
