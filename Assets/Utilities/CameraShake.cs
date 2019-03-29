using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    private float duration;
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        duration = shakeDuration;
    }

    public void Ini()
    {
        shakeDuration = duration;
        originalPos = camTransform.localPosition;
        InvokeRepeating("Play", 0, 0.1f);
    }

    public void Ini(float _ShakeDuration)
    {
        shakeDuration = _ShakeDuration;
        originalPos = camTransform.localPosition;
        InvokeRepeating("Play", 0, 0.1f);
    }

    public void Ini(float _ShakeDuration, float _ShakeAmout, float _DecreaseFactor)
    {
        shakeDuration = duration;
        shakeAmount = _ShakeAmout;
        decreaseFactor = _DecreaseFactor;
        originalPos = camTransform.localPosition;
        InvokeRepeating("Play", 0, 0.1f);
    }

    public void IncremetalShake(float _ShakeDuration, float _ShakeAmout)
    {
        StartCoroutine(StartIncrementalShake(_ShakeDuration, _ShakeAmout));
    }
    IEnumerator StartIncrementalShake(float _ShakeDuration, float _ShakeAmout)
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / _ShakeDuration)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * (shakeAmount * t);
            yield return null;
        }
        shakeDuration = 0f;
        camTransform.localPosition = originalPos;
    }

    private void Play ()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            CancelInvoke("Play");
        }
    }

    /*void OnEnable()
    {
        shakeDuration = duration;
        originalPos = camTransform.localPosition;
    }*/



    void Update()
    {
        /*if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            //shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            this.enabled = false;
        }*/
    }
}
