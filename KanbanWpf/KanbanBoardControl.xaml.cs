using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using KanbanWpf.Persistence;

namespace KanbanWpf
{
    /// <summary>
    /// Interaction logic for KanbanBoardControl.xaml
    /// </summary>
    public partial class KanbanBoardControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KanbanBoardControl"/> class.
        /// </summary>
        public KanbanBoardControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                var dest = e.GetPosition(canvas);
                CreateNoteAt(dest.X, dest.Y);
            }
        }

        private void CreateNoteAt(double x, double y, string text = null, string uid = null)
        {
            StickerNote note = new StickerNote();
            note.Text = text ?? "";
            note.Uid = uid ?? Guid.NewGuid().ToString();
            note.X = x;
            note.Y = y;

            canvas.Children.Add(note);
        }

        private IEnumerable<INote> GetVisualNotes()
        {
            return canvas.Children.OfType<INote>();
        }

        /// <summary>
        /// Saves all current notes into a stream.
        /// </summary>
        /// <param name="writer">The writer to write the notes to.</param>
        /// <param name="stream"></param>
        /// <exception cref="ArgumentNullException"><paramref name="writer"/> or <paramref name="stream"/> were null.</exception>
        public void SaveNotes(INotesWriter writer, Stream stream)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            writer.Write(GetVisualNotes().Select(item =>
            {
                PersistantNote note = new PersistantNote();
                note.Uid = item.Uid;
                note.X = item.X;
                note.Y = item.Y;
                note.Text = item.Text;

                return note;
            }), stream);
        }

        /// <summary>
        /// Loads all notes from the given stream, using the given reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="stream"></param>
        /// <exception cref="ArgumentNullException"><paramref name="reader"/> or <paramref name="stream"/> were null.</exception>
        public void LoadNotes(INotesReader reader, Stream stream)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            foreach (var item in reader.Read(stream))
            {
                CreateNoteAt(item.X, item.Y, item.Text, item.Uid);
            }
        }

        /// <summary>
        /// Determines the amount of tasks for a given task type.
        /// </summary>
        /// <param name="taskType">The type of the task to return the corresponding amount of tasks.</param>
        /// <returns>An integer representing the amount of tasks for a given task type.</returns>
        public int GetTaskCount(TaskType taskType)
        {
            double beginInProgress = overlayGrid.ColumnDefinitions.Take(2).Sum(item => item.ActualWidth);
            double beginDone = overlayGrid.ColumnDefinitions.Take(4).Sum(item => item.ActualWidth);

            return GetVisualNotes().Count(item =>
            {
                if (taskType == TaskType.Todo && item.X < beginInProgress)
                {
                    return true;
                }
                else if (taskType == TaskType.InProgress && item.X >= beginInProgress && item.X < beginDone)
                {
                    return true;
                }
                else if (taskType == TaskType.Done && item.X >= beginDone)
                {
                    return true;
                }
                return false;
            });
        }

        #endregion

    }
}
