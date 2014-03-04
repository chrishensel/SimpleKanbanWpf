using System.Collections.Generic;
using System.IO;

namespace KanbanWpf.Persistence
{
    /// <summary>
    /// Defines a method that is used to unpersist notes.
    /// </summary>
    public interface INotesReader
    {
        /// <summary>
        /// Reads all notes from the given stream.
        /// </summary>
        /// <param name="stream">The stream to load the notes from.</param>
        /// <returns>All notes from the given stream.</returns>
        IEnumerable<PersistantNote> Read(Stream stream);
    }
}
