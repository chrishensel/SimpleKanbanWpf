
namespace KanbanWpf
{
    /// <summary>
    /// Defines properties that represent a note.
    /// </summary>
    public interface INote
    {
        /// <summary>
        /// Gets/sets the X-coordinate of this note.
        /// </summary>
        double X { get; set; }
        /// <summary>
        /// Gets/sets the Y-coordinate of this note.
        /// </summary>
        double Y { get; set; }
        /// <summary>
        /// Gets/sets the unique id of this note.
        /// </summary>
        string Uid { get; set; }
        /// <summary>
        /// Gets/sets the note text.
        /// </summary>
        string Text { get; set; }
    }
}
