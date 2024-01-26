using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RievelGame
{
    public class UIWindowManager : MonoBehaviour
    {
        public static UIWindowManager Instance;  // Can be only one Instance
        [SerializeField] bool isInstance;

        [SerializeField] string startWindowName;

        [SerializeField] UIWindow[] uiWindows;

        private void Awake()
        {
            if (isInstance)
            {
                Instance = this;
            }

            GetAllChildWindow();

            CloseAllWindow();

            // If you assign start window it will auto open window
            if (!String.IsNullOrEmpty(startWindowName))
            {
                OpenWindow(startWindowName);
            }
        }

        private void GetAllChildWindow()
        {
            int childCount = transform.childCount;
            List<UIWindow> m_uiWindows = new List<UIWindow>();

            for (int i = 0; i < childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<UIWindow>() == null) continue;

                m_uiWindows.Add(transform.GetChild(i).GetComponent<UIWindow>());
            }

            uiWindows = m_uiWindows.ToArray();
        }

        public void OpenWindow(string windowName)
        {
            // Check for Open / Close Menu
            for (int i = 0; i < uiWindows.Length; i++)
            {
                if (uiWindows[i].windowName == windowName)
                {
                    uiWindows[i].Open();
                }
                else if (uiWindows[i].isOpen)
                {
                    CloseWindow(uiWindows[i]);
                }
            }
        }

        public void OpenWindow(UIWindow window)
        {
            // Check again when you use Menu as parameter
            for (int i = 0; i < uiWindows.Length; i++)
            {
                if (uiWindows[i].isOpen)
                {
                    CloseWindow(uiWindows[i]);
                }
            }

            window.Open();
        }

        // Press on Open = set to Close
        // Press on Close = set to Open
        public void SwitchWindow(UIWindow window)
        {
            if (window.isOpen)
            {
                // if press again just close all window
                CloseWindow(window);
            }
            else
            {
                // open select window
                OpenWindow(window);
            }
        }

        public void CloseWindow(UIWindow window)
        {
            window.Close();
        }

        public void CloseAllWindow()
        {
            for (int i = 0; i < uiWindows.Length; i++)
            {
                CloseWindow(uiWindows[i]);
            }
        }
    }
}
