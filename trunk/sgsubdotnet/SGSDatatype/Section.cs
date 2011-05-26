using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SGSDatatype
{
    interface ISection
    {
        /// <summary>
        /// Parse a line in a section.
        /// </summary>
        /// <param name="line">Line</param>
        void AddLine(string line);

        /// <summary>
        /// Write the content to a stream including section descriptor.
        /// </summary>
        /// <param name="streamWriter">Stream</param>
        void WriteTo(StreamWriter streamWriter);
        string SectionName { get; }
        SSAVersion Version { get; set; }
    }
}
