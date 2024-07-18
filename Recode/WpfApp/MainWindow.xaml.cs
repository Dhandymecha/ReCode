using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Util;
using WpfApp.DB;
using WpfApp.Model;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger Logger = Logger.Instance;
        private SQDatabase SQDatabase = SQDatabase.Instance;

        private UIContext UIContext;

        // 녹화 리스트
        public RecordBook RecordBook { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            UIContext = new UIContext();

            UIContext.DatabaseLoadCommand = new RelayCommand(DatabaseLoad);
            UIContext.PlayVideoCommand = new RelayCommand<RecordEntry>(PlayVideo);

            this.DataContext = UIContext;

            RecordBook = new RecordBook();

        }
        private void DatabaseLoad()
        {
            UpdateRecordBook(DateTime.Now.AddDays(-7), DateTime.Now);
        }
        public void UpdateRecordBook(DateTime from, DateTime to)
        {
            try {

                var data = SQDatabase.Select(from, to);

                RecordBook.Update(data);

                RecordBook.CurrentNumber = 1;
                UIContext.RecordItems = RecordBook.GetCurrentPage();
            }
            catch (Exception ex) {
                Logger.Write(LEVEL.ERROR, ex.ToString());
                throw;
            }
        }
        private void PlayVideo(RecordEntry item)
        {
            Debug.WriteLine($"PlayVideo : {item.Name}");
        }
    }
}