using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float bobDelayDown, bobDelayUp, bobDistance, startOffset;
   
    private bool up;

    private Coroutine isBobbing;
    private bool bobbing;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        StartCoroutine(StartBobbingWithDelay());
    }

    private IEnumerator DoBobbing()
    {
        float bobDelay = 0;

        if (bobbing)
        {
            if (up)
            {
                transform.Translate(Vector3.down * bobDistance);
                bobDelay = bobDelayDown;
            }
            else
            {
                transform.Translate(Vector3.up * bobDistance);
                bobDelay = bobDelayUp;
            }

            up = !up;
            yield return new WaitForSeconds(bobDelay);
            isBobbing = StartCoroutine(DoBobbing());
        }  
    }

    public void StopBobbing()
    {
        bobbing = false;
        if (isBobbing != null)
            StopCoroutine(isBobbing);
        transform.localPosition = startPosition;
    }

    public void StartBobbing()
    {
			StartCoroutine(StartBobbingWithDelay());
    }

    private IEnumerator StartBobbingWithDelay()
    {
			//StopAllCoroutines();
			yield return new WaitForSeconds(startOffset);
			up = true;
			bobbing = true;
			if (isBobbing != null)
				StopCoroutine(isBobbing);

			isBobbing = StartCoroutine(DoBobbing());
    }
}
