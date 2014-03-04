using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace KanbanWpf.Persistence
{
    /// <summary>
    /// Writes notes to an XML document.
    /// </summary>
    public class XmlNotesWriter : INotesWriter
    {
        #region INotesWriter Members

        void INotesWriter.Write(IEnumerable<PersistantNote> notes, Stream stream)
        {
            XDocument doc = new XDocument();
            doc.Add(new XElement("board"));

            foreach (var item in notes)
            {
                XElement elem = new XElement("item");
                elem.Add(new XAttribute("uid", item.Uid));
                elem.Add(new XAttribute("x", item.X.ToString(CultureInfo.InvariantCulture)));
                elem.Add(new XAttribute("y", item.Y.ToString(CultureInfo.InvariantCulture)));
                elem.Add(new XElement("text", item.Text));

                doc.Root.Add(elem);
            }

            doc.Save(stream);
        }

        #endregion
    }
}
