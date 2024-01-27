using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

[System.Serializable]
public class ProfileAnimate
{
    public float target;
    public float duration;
}

public class PostProcessManager : SceneSingleton<PostProcessManager>
{
    [Header("Volume")]
    [SerializeField] private Volume _postProcessVolume;

    [Header("Profile Animate")]
    [SerializeField] private ProfileAnimate _chromaticAberrationIntensity;
    [SerializeField] private ProfileAnimate _lensDistortionIntensity;
    [SerializeField] private ProfileAnimate _vignetteIntensity;
    [SerializeField] private ProfileAnimate _depthOfFieldEnd;

    private ChromaticAberration _chromaticAberration;
    private LensDistortion _lensDistortion;
    private Vignette _vignette;
    private DepthOfField _depthOfField;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (_postProcessVolume.profile.TryGet<ChromaticAberration>(out _chromaticAberration))
        {
        }

        if (_postProcessVolume.profile.TryGet<LensDistortion>(out _lensDistortion))
        {
        }

        if (_postProcessVolume.profile.TryGet<Vignette>(out _vignette))
        {
        }

        if (_postProcessVolume.profile.TryGet<DepthOfField>(out _depthOfField))
        {
        }
    }

    [ContextMenu("SetChromaticAberration")]
    public void SetChromaticAberration()
    {
        float value = _chromaticAberration.intensity.value;
        DOTween.To(() => value, x => value = x, _chromaticAberrationIntensity.target, _chromaticAberrationIntensity.duration).OnUpdate(() =>
        {
            _chromaticAberration.intensity.value = value;
        }).OnComplete(() =>
        {
            _chromaticAberration.intensity.value = _chromaticAberrationIntensity.target;
        });
    }

    [ContextMenu("SetLensDistortion")]
    public void SetLensDistortion()
    {
        float value = _lensDistortion.intensity.value;
        DOTween.To(() => value, x => value = x, _lensDistortionIntensity.target, _lensDistortionIntensity.duration).OnUpdate(() =>
        {
            _lensDistortion.intensity.value = value;
        }).OnComplete(() =>
        {
            _lensDistortion.intensity.value = _lensDistortionIntensity.target;
        });
    }

    [ContextMenu("SetVignette")]
    public void SetVignette()
    {
        float value = _vignette.intensity.value;
        DOTween.To(() => value, x => value = x, _vignetteIntensity.target, _vignetteIntensity.duration).OnUpdate(() =>
        {
            _vignette.intensity.value = value;
        }).OnComplete(() =>
        {
            _vignette.intensity.value = _vignetteIntensity.target;
        });
    }

    [ContextMenu("SetDepthOfField")]
    public void SetDepthOfField()
    {
        float value = _depthOfField.gaussianEnd.value;
        DOTween.To(() => value, x => value = x, _depthOfFieldEnd.target, _depthOfFieldEnd.duration).OnUpdate(() =>
        {
            _depthOfField.gaussianEnd.value = value;
        }).OnComplete(() =>
        {
            _depthOfField.gaussianEnd.value = _depthOfFieldEnd.target;
        });
    }
}
