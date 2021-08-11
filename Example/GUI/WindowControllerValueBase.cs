using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MVC;

namespace MVC.Example.UI
{
    public abstract class WindowControllerValueBase<TModel, TView> : ControllerValueBase<TModel, TView>, IWindowController
        where TModel : struct
        where TView : Window
    {
        public bool HasView => _view != null;

        public event EventHandler Closed;
        public event EventHandler Hided;

        private bool _interactable = true;

        public bool Interactable
        {
            get => _interactable;
            set
            {               
                _interactable = value;
                UpdateInteractable(_view);
            }
        }

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

        protected override void InternalConnectView(TView view)
        {
            base.InternalConnectView(view);

            UpdateInteractable(view);
        }

        private void UpdateInteractable(TView view)
        {
            if (view != null)
            {
                view.Interactable = _interactable;
            }
        }
    }
}