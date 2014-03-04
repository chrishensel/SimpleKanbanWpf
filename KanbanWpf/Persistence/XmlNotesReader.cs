using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace KanbanWpf.Persistence
{
    /// <summary>
    /// Reads notes from an XML document.
    /// </summary>
    public class XmlNotesReader : INotesReader
    {
        #region INotesReader Members

        IEnumerable<PersistantNote> INotesReader.Read(Stream stream)
        {
            XDocument document = XDocument.Load(stream);

            foreach (var item in document.Root.Elements("item"))
            {
                yield return new PersistantNote()
                {
                    X = double.Parse(item.Attribute("x").Value, CultureInfo.InvariantCulture),
                    Y = double.Parse(item.Attribute("y").Value, CultureInfo.InvariantCulture),
                    Text = item.Element("text").Value,
                    Uid = item.Attribute("uid").Value,
                };
            }
        }

        #endregion
    }
}
