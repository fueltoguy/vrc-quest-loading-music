using loadingmusic;
using MelonLoader;
using UnityEngine;

public class Main : MelonMod
{
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (buildIndex == 3)
        {
           MelonCoroutines.Start(Music.start());
        }
    }
}
