using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp.Model
{
    public class RecordBook : ObservableObject
    {
        // 리스트
        public List<RecordEntry> List { get; private set; }

        // 현재 페이지
        public int CurrentNumber    { get; set; }

        // 총 페이지
        public int TotalNumber      { get; set; }

        public static int PAGE_PER_NUMBER = 10;

        public RecordBook()
        {
            List = new List<RecordEntry>();
            CurrentNumber = 1;
            TotalNumber = 1;
        }
        public void Update(List<RecordEntry> list)
        {
            List = list;
            CurrentNumber = 1;
            TotalNumber = (List.Count / PAGE_PER_NUMBER);

            var capacity = (PAGE_PER_NUMBER * TotalNumber);
            if (capacity > 0) { // DivideByZero
                if (List.Count % capacity > 0) {
                    TotalNumber = TotalNumber + 1;
                }
            }
            else {
                TotalNumber = 1;
            }
        }
        public bool Prev()
        {
            int number = CurrentNumber - 1;
            if (number > 0) {
                CurrentNumber = number;
                return true;
            }
            return false;
        }
        public bool Next()
        {
            int number = CurrentNumber + 1;
            if (number <= TotalNumber) {
                CurrentNumber = number;
                return true;
            }
            return false;
        }
        public ObservableCollection<RecordEntry> GetCurrentPage()
        {
            var number = CurrentNumber;

            var items = new ObservableCollection<RecordEntry>();

            var index = PAGE_PER_NUMBER * (number - 1);
            var count = PAGE_PER_NUMBER;
            if (List.Count < (index + count)) {
                count = List.Count - (index);
            }

            if (index >= 0) {
                foreach (var record in List.GetRange(index, count)) {
                    items.Add(record);
                }
            }

            return items;
        }
    }
}
