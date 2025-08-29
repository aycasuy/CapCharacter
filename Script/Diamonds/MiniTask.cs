
using UnityEngine;

public class MiniTask : MonoBehaviour
{
    public Diamond diamondToUnlock;

    public void CompleteMiniTask()
    {
        if (diamondToUnlock != null)
        {
            diamondToUnlock.UnlockDiamond();
        }
    }
}


