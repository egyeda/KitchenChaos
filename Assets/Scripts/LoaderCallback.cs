using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFurstUpdate = true;
    void Update()
    {
        if (isFurstUpdate)
        {
            isFurstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
