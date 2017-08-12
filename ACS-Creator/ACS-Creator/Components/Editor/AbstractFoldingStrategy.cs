
using System;
using ICSharpCode.AvalonEdit.Document;
using System.Collections.Generic;

namespace ICSharpCode.AvalonEdit.Folding
{
    /// <summary>
    /// Base class for folding strategies.
    /// </summary>
    public abstract class AbstractFoldingStrategy
    {
        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document and updates the folding manager with them.
        /// </summary>
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int first_error_offset;
            var foldings = CreateNewFoldings(document, out  first_error_offset);
            manager.UpdateFoldings(foldings, first_error_offset);
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public abstract IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int first_error_offset);

        public static implicit operator AbstractFoldingStrategy(XmlFoldingStrategy v)
        {
            throw new NotImplementedException();
        }
    }
}

