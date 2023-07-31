using System;
using System.IO;
using MelonLoader;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace loadingmusic
{
    public class Music
    {
        public static AudioClip clip;
        public static IEnumerator start()
        {
            string path = Path.Combine(Directory.CreateDirectory("LoadingMusic/").FullName, "Loading.ogg");
            if (!File.Exists(path))
            {
                var download = new UnityWebRequest("https://cdn.discordapp.com/attachments/592057771566825491/1113210084676345856/custommenu.ogg", UnityWebRequest.kHttpVerbGET);
                download.downloadHandler = new DownloadHandlerFile(path);
                yield return download.SendWebRequest();
            }
            UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
            yield return localfile.SendWebRequest();
            clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
            AudioSource musicObj = stuffFromRemod.userInterface.transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound").GetComponent<AudioSource>();
            MelonLogger.Msg("before if");
            while (stuffFromRemod.userInterface.transform.Find("MenuContent/Popups/LoadingPopup/LoadingSound") == null) yield return new WaitForEndOfFrame();
                musicObj.clip = clip;
                musicObj.volume = 100;
                musicObj.Play();
                MelonLogger.Msg("music should be playing");
            localfile = null;
            yield break;
        }
    }
}
