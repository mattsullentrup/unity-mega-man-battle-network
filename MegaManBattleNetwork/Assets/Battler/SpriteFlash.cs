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
        _duration = _battler.InvulnerableCooldown;
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
        _spriteRenderer.color = Color.white;
        _flashRoutine = null;
        StopAllCoroutines();
    }

    private IEnumerator SwapMaterialRoutine()
    {
        if (!_battler.CanMove)
        {
            ChangeFlashMaterial();
        }
        else
        {
            if (_spriteRenderer.material != _originalMaterial)
                _spriteRenderer.material = _originalMaterial;

            ChangeAlpha();
        }

        yield return new WaitForSeconds(_interval);
        StartCoroutine(SwapMaterialRoutine());
    }

    private void ChangeFlashMaterial()
    {
        if (_spriteRenderer.material == _originalMaterial)
        {
            _spriteRenderer.material = _flashMaterial;
        }
        else
        {
            _spriteRenderer.material = _originalMaterial;
        }
    }

    private void ChangeAlpha()
    {
        if (_spriteRenderer.color == Color.white)
        {
            _spriteRenderer.color = new(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0.5f);
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }
}
