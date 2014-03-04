
namespace KanbanWpf.Persistence
{
    /// <summary>
    /// Represents the persistency data of a note to store or retrieve.
    /// </summary>
    public struct PersistantNote : INote
    {
        /// <summary>
        /// Gets/sets the X-coordinate of this note.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Gets/sets the Y-coordinate of this note.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Gets/sets the unique id of this note.
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Gets/sets the note text.
        /// </summary>
        public string Text { get; set; }
    }
}
