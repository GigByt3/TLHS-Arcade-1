using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    public GameObject camera;

    public GameObject deathTarget;

    RawImage image;
    // Start is called before the first frame update
    void Awake()
    {
        image = this.GetComponent<RawImage>();
    }

    public IEnumerator FadeToBlack(System.Action call)
    {
        int fadeCounter = 200;

        for (int i = 0; i < fadeCounter; i++)
        {
            if (i % 10 == 0)
            {
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("Fade at : " + (int) 100 * (i / fadeCounter) + "%");
            image.color = new Color(
                image.color.r - 0.005f,
                image.color.g - 0.005f,
                image.color.b - 0.005f
                );
        }

        call();
    }

    public IEnumerator FadeUp(System.Action call)
    {
        int fadeCounter = 200;

        for (int i = 0; i < fadeCounter; i++)
        {
            if (i % 10 == 0)
            {
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("Fade at : " + (int)100 * (i / fadeCounter) + "%");
            image.color = new Color(
                image.color.r + 0.005f,
                image.color.g + 0.005f,
                image.color.b + 0.005f
                );
        }

        call();
    }

    public IEnumerator FadeToRed(System.Action call)
    {
        int fadeCounter = 200;

        for (int i = 0; image.color.b > 0.4f; i++)
        {
            if(i % 6 == 0)
            {
                yield return new WaitForSeconds(0.01f);
            }
            Debug.Log("Fade at : " + i + "/" + fadeCounter);
            image.color = new Color(
                image.color.r,
                image.color.g - 0.005f,
                image.color.b - 0.004f
                );
        }

        int fallInterpolation = 20;

        float[] diffs =
            {
                VectorDiff(camera.transform.position.x, deathTarget.transform.position.x, fallInterpolation),
                VectorDiff(camera.transform.position.y, deathTarget.transform.position.y, fallInterpolation),
                VectorDiff(camera.transform.position.z, deathTarget.transform.position.z, fallInterpolation),
                VectorDiff(camera.transform.rotation.x, deathTarget.transform.rotation.x, fallInterpolation),
                VectorDiff(camera.transform.rotation.y, deathTarget.transform.rotation.y, fallInterpolation),
                VectorDiff(camera.transform.rotation.z, deathTarget.transform.rotation.z, fallInterpolation),
                VectorDiff(camera.transform.rotation.w, deathTarget.transform.rotation.w, fallInterpolation)
            };

        for (int i = 0; i < fallInterpolation || camera == null; i++)
        {
            yield return new WaitForSeconds((float) ((Mathf.Log(i, 2.718f) - 1)/16));

            Debug.Log("Diff test (2 steps between 2 and 8 of size): " + VectorDiff(2, 8, 2));
            Debug.Log("Examples: Diff[pos.x] : " + diffs[0] + " Diff[pos.x] : " + diffs[2] + " Diff[rot.x] : " + diffs[3]);
            try
            {
                Vector3 newPos = new Vector3(camera.transform.position.x - diffs[0], camera.transform.position.y - diffs[1], camera.transform.position.z - diffs[2]);
                Quaternion newRot = new Quaternion(camera.transform.rotation.x - diffs[3], camera.transform.rotation.y - diffs[4], camera.transform.rotation.z - diffs[5], camera.transform.rotation.w - diffs[6]);
                camera.transform.SetPositionAndRotation(newPos, newRot);
            }
            catch(Exception e)
            {
                Debug.Log("Camera wierdness: " + e);
            }
        }

        //Final Pause
        yield return new WaitForSeconds(2);

        call();
    }

    private float VectorDiff(float x, float y, int diff)
    {
        float finalDiff = (x - y) / diff;

        return finalDiff;
    }
}
