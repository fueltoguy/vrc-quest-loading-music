using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace loadingmusic
{
    internal class stuffFromRemod
    {
        private static GameObject _getUserInterface;
        public static GameObject userInterface
        {
            get
            {
                if (_getUserInterface == null)
                {
                    ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
                    MelonCoroutines.Start(WaitForUI());
                }

                return _getUserInterface;
                 IEnumerator WaitForUI()
                {
                    while ((object)VRCUiManager.field_Private_Static_VRCUiManager_0 == null)
                    {
                        yield return null;
                    }

                    foreach (GameObject GameObjects in Resources.FindObjectsOfTypeAll<GameObject>())
                    {
                        if (GameObjects.name.Contains("UserInterface"))
                        {
                            _getUserInterface = GameObjects;
                        }
                    }

                    while ((object)_getUserInterface == null)
                    {
                        yield return null;
                    }
                }
            }
        }
    }

    [RegisterTypeInIl2Cpp]
        public class EnableDisableListener : MonoBehaviour
        {
            private static bool _registered;

            [method: HideFromIl2Cpp]
            public event Action OnEnableEvent;

            [method: HideFromIl2Cpp]
            public event Action OnDisableEvent;

            public EnableDisableListener(IntPtr obj)
                : base(obj)
            {
            }

            public void OnEnable()
            {
                this.OnEnableEvent?.Invoke();
            }

            public void OnDisable()
            {
                this.OnDisableEvent?.Invoke();
            }

            [HideFromIl2Cpp]
            public static void RegisterSafe()
            {
                if (!_registered)
                {
                    try
                    {
                        ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
                        _registered = true;
                    }
                    catch (Exception)
                    {
                        _registered = true;
                    }
                }
            }
        }
}
