using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SelfDestruct : MonoBehaviour {

    public float lifeTime = 1f;
    public float duration = 1f;
    public bool weakRumble = false;
    public bool strongRumble = false;

    // Update is called once per frame
    void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);

        if (weakRumble)
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 1);
            StartCoroutine(WeakRumble());
            return;
        }

        if (strongRumble)
        {
            GamePad.SetVibration(PlayerIndex.One, 1, 0);
            StartCoroutine(StrongRumble());
            return;
        }
    }

    IEnumerator WeakRumble()
    {
        yield return new WaitForSeconds(duration);
        weakRumble = false;
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
        
    }

    IEnumerator StrongRumble()
    {
        yield return new WaitForSeconds(duration);
        strongRumble = false;
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
        
    }
}
