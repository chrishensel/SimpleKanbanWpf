using System.Collections.Generic;
using System.IO;

namespace KanbanWpf.Persistence
{
    /// <summary>
    /// Defines a method that is used to persist notes.
    /// </summary>
    public interface INotesWriter
    {
        /// <summary>
        /// Writes all notes to the given stream.
        /// </summary>
        /// <param name="notes">The notes to write.</param>
        /// <param name="stream">The stream to write to.</param>
        void Write(IEnumerable<PersistantNote> notes, Stream stream);
    }
}
