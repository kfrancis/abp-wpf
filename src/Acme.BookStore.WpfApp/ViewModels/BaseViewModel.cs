using CommunityToolkit.Mvvm.ComponentModel;

namespace Acme.BookStore.WpfApp.ViewModels
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _canLoadMore = true;

        [ObservableProperty]
        private string? _footer = string.Empty;

        [ObservableProperty]
        private string? _header = string.Empty;

        [ObservableProperty]
        private string? _icon = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        [ObservableProperty]
        private string? _subtitle = string.Empty;

        [ObservableProperty]
        private string? _title = string.Empty;

        public bool IsNotBusy => !_isBusy;
    }
}
