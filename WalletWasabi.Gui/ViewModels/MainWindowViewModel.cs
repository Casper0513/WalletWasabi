﻿using Avalonia;
using Avalonia.Controls;
using AvalonStudio.Extensibility;
using AvalonStudio.Extensibility.Dialogs;
using AvalonStudio.Shell;
using NBitcoin;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletWasabi.Gui.ViewModels
{
	public class MainWindowViewModel : ViewModelBase, IDisposable
	{
		private ModalDialogViewModelBase _modalDialog;
		private bool _canClose = true;

		private string _title = "Wasabi Wallet";

		public string Title
		{
			get { return _title; }
			internal set { this.RaiseAndSetIfChanged(ref _title, value); }
		}

		private double _height;

		public double Height
		{
			get { return _height; }
			internal set { this.RaiseAndSetIfChanged(ref _height, value); }
		}

		private double _width;

		public double Width
		{
			get { return _width; }
			internal set { this.RaiseAndSetIfChanged(ref _width, value); }
		}

		private WindowState _windowState;

		public WindowState WindowState
		{
			get { return _windowState; }
			internal set { this.RaiseAndSetIfChanged(ref _windowState, value); }
		}

		private StatusBarViewModel _statusBar;

		public StatusBarViewModel StatusBar
		{
			get { return _statusBar; }
			internal set { this.RaiseAndSetIfChanged(ref _statusBar, value); }
		}

		public MainWindowViewModel()
		{
			Shell = IoC.Get<IShell>();
		}

		public IShell Shell { get; }

		public static MainWindowViewModel Instance { get; internal set; }

		public async Task<bool> ShowDialogAsync(ModalDialogViewModelBase dialog)
		{
			ModalDialog = dialog;

			bool res = await ModalDialog.ShowDialogAsync();

			ModalDialog = null;

			return res;
		}

		public ModalDialogViewModelBase ModalDialog
		{
			get { return _modalDialog; }
			private set { this.RaiseAndSetIfChanged(ref _modalDialog, value); }
		}

		public bool CanClose
		{
			get { return _canClose; }
			set { this.RaiseAndSetIfChanged(ref _canClose, value); }
		}

		#region IDisposable Support

		private volatile bool _disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					foreach (var tab in Shell?.Documents?.OfType<IDisposable>())
					{
						tab.Dispose();
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				_disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}

		#endregion IDisposable Support
	}
}
