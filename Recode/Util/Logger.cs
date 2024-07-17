using System.Diagnostics;
using System.Globalization;

namespace Util
{
    public enum LEVEL
    {
        NONE,
        DEBUG,
        INFO,
        WARN,
        ERROR,
    }

    public class MesssageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MesssageEventArgs(string message)
        {
            Message = message;
        }
    }

    public class Logger
    {
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance { get { return lazy.Value; } }

        private readonly object _syncLock = new object();

        private static string DATE_FORMAT = "yyyyMMdd";
        private static string FILE_EXT = "log";

        private DateTime StartTime;

        private string BaseDirectory;
        private string FilePath;

        public event EventHandler<MesssageEventArgs> RaiseEvent;

        public void Initialize()
        {
            StartTime = DateTime.Now.AddDays(-1);

            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Today(DateTime.Now);

            CleanUp();
        }
        public void CleanUp()
        {
            try {

                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(FilePath));
                FileInfo[] fi = di.GetFiles($"*.{FILE_EXT}");

                // 6 개월 이전 로그 삭제
                DateTime from = DateTime.Now.AddMonths(-6);

                for (int i = 0; i < fi.Length; i++) {

                    string name = Path.GetFileNameWithoutExtension(fi[i].Name);

                    if (DateTime.TryParseExact(name, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime)) {

                        if (dateTime.CompareTo(from) < 0) {
                            fi[i].Delete();
                        }
                    }
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }

        }
        public void Today(DateTime dateTime)
        {
            if (StartTime.Day != dateTime.Day) {

                var logDirectory = BaseDirectory + "Logs";
                // 디렉토리
                if (!Directory.Exists(logDirectory)) {
                    Directory.CreateDirectory(logDirectory);
                }
                // 파일경로
                FilePath = Path.Combine(logDirectory, $"{dateTime.ToString(DATE_FORMAT)}.{FILE_EXT}");
            }
        }
        public void Write(LEVEL level, string message)
        {
            try {

                Debug.WriteLine(message);

                DateTime current = DateTime.Now;

                Today(current);

                lock (_syncLock) {

                    var text = $"{current.ToString("yyyy-MM-dd HH:mm:ss.fff")} [{level}] {message}";

                    using (var sw = new StreamWriter(File.Open(FilePath, FileMode.Append))) {
                        sw.WriteLine(text);
                    }

                    RaiseEvent?.Invoke(this, new MesssageEventArgs(text));
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }

        }
    }
}
