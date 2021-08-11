using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVC;

namespace MVC.Example.UI
{
    public class WindowControllerBase<TView> : ControllerBase<TView>, IWindowController
        where TView : Window
    {
        public bool HasView => _view != null;

        public event EventHandler Closed;
        public event EventHandler Hided;

        public virtual void Close()
        {
            _view.Close();

            DisconnectView();

            OnClosed();
        }

        public virtual void Show()
        {
            _view.Show();
        }

        public virtual void Hide()
        {
            _view.Hide();

            Hided?.Invoke(this, EventArgs.Empty);
        }

        private void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
    }
}