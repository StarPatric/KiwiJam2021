using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    private static Vector3 getCurvePoint(Vector3 v1, Vector3 v2, Vector3 v3, float delta)
    {
        return Vector3.Lerp(Vector3.Lerp(v1, v2, delta), Vector3.Lerp(v2, v3, delta), delta);
    }
    public static Vector3 getScenePoint(float t, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (getCurvePoint(p1, p2, p3, t));
    }


    public static void animateToPoint(GameObject _target, Vector3 _toPos, Vector3 _fromPos, float _speed)
    {
        GameManager.instance.StartCoroutine(animatePath(_target, _toPos, _fromPos, _speed));
    }


    static IEnumerator animatePath(GameObject _target, Vector3 _toPos, Vector3 _fromPos, float _speed)
    {
        float currentStep = 0;
        // currentStep / (float)lineSteps, transforms[0].position, transforms[1].position, transforms[2].position
        while (currentStep / (float)100 < 1)
        {
            _target.transform.position = getScenePoint((currentStep / (float)100), _toPos, _fromPos, _fromPos);
            currentStep += _speed * Time.deltaTime;
            yield return null;
        }
    }
}
