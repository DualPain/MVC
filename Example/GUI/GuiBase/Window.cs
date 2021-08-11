using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gui
{
    [RequireComponent(typeof(Canvas))]
    public class Window : MonoBehaviour
    {
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private IWindowCloser _windowCloser;

        public event EventHandler Showed;
        public event EventHandler Hided;
        public event EventHandler Closed;

        public Canvas Canvas => _canvas ?? (_canvas = GetComponent<Canvas>());
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();

                    if (_canvasGroup == null)
                    {
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    }
                }

                return _canvasGroup;
            }
        }

        public bool Interactable
        {
            get => CanvasGroup.interactable;
            set
            {
                SetInteractable(value);
            }
        }

        internal void Init(IWindowCloser windowCloser)
        {
            _windowCloser = windowCloser;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Showed?.Invoke(this, EventArgs.Empty);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Hided?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            _windowCloser.CloseWindow(this);

            Closed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void SetInteractable(bool value)
        {
            CanvasGroup.interactable = value;
        }
    }
}