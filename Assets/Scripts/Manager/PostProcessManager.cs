using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

[System.Serializable]
public class ProfileAnimate
{
    public float startValue;
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

    public void Execute(bool isReset)
    {
        print("Execute Post Process");
        SetChromaticAberration(isReset);
        SetLensDistortion(isReset);
        SetVignette(isReset);
        SetDepthOfField(isReset);
    }


    [ContextMenu("SetChromaticAberration")]
    public void SetChromaticAberration(bool isReset)
    {
        float value = _chromaticAberration.intensity.value;
        float target = isReset == true ? _chromaticAberrationIntensity.startValue : _chromaticAberrationIntensity.target;

        DOTween.To(() => value, x => value = x, target, _chromaticAberrationIntensity.duration).OnUpdate(() =>
        {
            _chromaticAberration.intensity.value = value;
        }).OnComplete(() =>
        {
            _chromaticAberration.intensity.value = target;
        });
    }

    [ContextMenu("SetLensDistortion")]
    public void SetLensDistortion(bool isReset)
    {
        float value = _lensDistortion.intensity.value;
        float target = isReset == true ? _lensDistortionIntensity.startValue : _lensDistortionIntensity.target;

        DOTween.To(() => value, x => value = x, target, _lensDistortionIntensity.duration).OnUpdate(() =>
        {
            _lensDistortion.intensity.value = value;
        }).OnComplete(() =>
        {
            _lensDistortion.intensity.value = target;
        });
    }

    [ContextMenu("SetVignette")]
    public void SetVignette(bool isReset)
    {
        float value = _vignette.intensity.value;
        float target = isReset == true ? _vignetteIntensity.startValue : _vignetteIntensity.target;

        DOTween.To(() => value, x => value = x, target, _vignetteIntensity.duration).OnUpdate(() =>
        {
            _vignette.intensity.value = value;
        }).OnComplete(() =>
        {
            _vignette.intensity.value = target;
        });
    }

    [ContextMenu("SetDepthOfField")]
    public void SetDepthOfField(bool isReset)
    {
        float value = _depthOfField.gaussianEnd.value;
        float target = isReset == true ? _depthOfFieldEnd.startValue : _depthOfFieldEnd.target;

        DOTween.To(() => value, x => value = x, target, _depthOfFieldEnd.duration).OnUpdate(() =>
        {
            _depthOfField.gaussianEnd.value = value;
        }).OnComplete(() =>
        {
            _depthOfField.gaussianEnd.value = target;
        });
    }
}
