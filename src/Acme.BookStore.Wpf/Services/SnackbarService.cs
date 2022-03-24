using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using MaterialDesignThemes.Wpf;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf.Services
{
    public interface ISnackbarService
    {
        //ISnackbarMessageQueue TheSnackbarMessageQueue { get; }

        void Enqueue(object content, object actionContent, Action actionHandler, bool promote = false);

        void Enqueue(
            object content,
            object actionContent,
            Action<object?> actionHandler,
            object actionArgument,
            bool promote,
            bool neverConsiderToBeDuplicate);

        void Enqueue(object content);

        void EnqueueWithOk(object content);
    }

    public class SnackbarService : ISnackbarService, IDisposable, ISingletonDependency
    {
        private bool _disposedValue;

        //public ISnackbarMessageQueue TheSnackbarMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromSeconds(4));

        public void Enqueue(object content, object actionContent, Action actionHandler, bool promote = false)
        {
            //TheSnackbarMessageQueue.Enqueue(content, actionContent, actionHandler, promote);
        }

        public void Enqueue(
            object content,
            object actionContent,
            Action<object?> actionHandler,
            object actionArgument,
            bool promote,
            bool neverConsiderToBeDuplicate)
        {
            //TheSnackbarMessageQueue.Enqueue(
            //    content,
            //    actionContent,
            //    actionHandler,
            //    actionArgument,
            //    promote,
            //    neverConsiderToBeDuplicate);
        }

        public void Enqueue(object content)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Application.Current.MainWindow?.WindowState != WindowState.Minimized)
                {
                    //TheSnackbarMessageQueue.Enqueue(content);
                }
            }));
        }

        public void EnqueueWithOk(object content)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Application.Current.MainWindow?.WindowState != WindowState.Minimized)
                {
                    //TheSnackbarMessageQueue.Enqueue(content, "OK", (obj) => { }, () => { }, true, true, TimeSpan.FromSeconds(20));
                }
            }));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    //((SnackbarMessageQueue)TheSnackbarMessageQueue)?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SnackbarService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
