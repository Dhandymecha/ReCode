using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfApp.Model;

namespace WpfApp.ViewModel
{
    public class UIContext : ObservableObject
    {
        // 레코드 - 현재 페이지
        private int _current;
        public int CurrentPage
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        // 레코드 - 총 페이지
        private int _last;
        public int LastPage
        {
            get { return _last; }
            set { SetProperty(ref _last, value); }
        }

        // 레코드 - 표시 레코드
        private ObservableCollection<RecordEntry> _recordItems;
        public ObservableCollection<RecordEntry> RecordItems
        {
            get { return _recordItems; }
            set { SetProperty(ref _recordItems, value); }
        }

        public IRelayCommand DatabaseLoadCommand    { get; set; }
        public IRelayCommand PlayVideoCommand       { get; set; }
    }
}
