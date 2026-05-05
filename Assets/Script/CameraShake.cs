using UnityEngine;
using System.Collections;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class CameraShake : MonoBehaviour
{
    private ShotgunWeapon shotgunWeapon;

    void Awake()
    {
        shotgunWeapon = FindAnyObjectByType<ShotgunWeapon>();
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        Vector2 recoilDir = -shotgunWeapon.GetAimDirection();

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos + (Vector3)(recoilDir * magnitude);
    }
}
