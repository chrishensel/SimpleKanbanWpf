using System.Windows;
using System.Windows.Controls;

namespace KanbanWpf
{
    /// <summary>
    /// Interaction logic for StickerNote.xaml
    /// </summary>
    public partial class StickerNote : UserControl, INote
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StickerNote"/> class.
        /// </summary>
        public StickerNote()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Canvas canvas = LogicalTreeHelper.GetParent(this) as Canvas;
            if (canvas != null)
            {
                var result = MessageBox.Show(Properties.Resources.ConfirmDeleteNoteMessage, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    canvas.Children.Remove(this);
                }
            }
        }

        #endregion

        #region INote Members

        /// <summary>
        /// Gets/sets the X-coordinate of this note.
        /// </summary>
        public double X
        {
            get { return Canvas.GetLeft(this); }
            set { Canvas.SetLeft(this, value); }
        }

        /// <summary>
        /// Gets/sets the Y-coordinate of this note.
        /// </summary>
        public double Y
        {
            get { return Canvas.GetTop(this); }
            set { Canvas.SetTop(this, value); }
        }

        /// <summary>
        /// Gets/sets the note text.
        /// </summary>
        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        #endregion
    }
}
