using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using KanbanWpf.Persistence;

namespace KanbanWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constants

        private const string TemporaryFileName = "kanban.xml";

        #endregion

        #region Fields

        private DispatcherTimer _timer;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overridden to persist all notes to the state XML file.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _timer.Stop();

            try
            {
                using (Stream stream = new FileStream(TemporaryFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    kanban.SaveNotes(new XmlNotesWriter(), stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.SaveFileError, ex.Message), Properties.Resources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(TemporaryFileName))
            {
                try
                {
                    using (Stream stream = File.OpenRead(TemporaryFileName))
                    {
                        kanban.LoadNotes(new XmlNotesReader(), stream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Properties.Resources.LoadFileError, ex.Message), Properties.Resources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(2d), DispatcherPriority.Background, _timer_Callback, Dispatcher);
            _timer.Start();
        }

        private void _timer_Callback(object sender, EventArgs e)
        {
            tasksAmount.Text = GetTaskNotesText();
        }

        private string GetTaskNotesText()
        {
            StringBuilder sb = new StringBuilder();

            int numTodo = kanban.GetTaskCount(TaskType.Todo);
            int numInProgress = kanban.GetTaskCount(TaskType.InProgress);
            int numDone = kanban.GetTaskCount(TaskType.Done);

            sb.AppendFormat("To Do: {0}", numTodo).AppendLine();
            sb.AppendFormat("In Progress: {0}", numInProgress).AppendLine();
            sb.AppendFormat("Done: {0}", numDone).AppendLine();

            return sb.ToString();
        }

        #endregion
    }
}
