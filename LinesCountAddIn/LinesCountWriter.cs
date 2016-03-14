using LinesCount;
using Mono.TextEditor;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System;
using System.Collections.Generic;

namespace LinesCountAddIn
{
    public class LinesCountWriter
    {
        private TextEditorData textEditorData;
        private Document linesCountDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateInserter.LinesCountWriter"/> class.
        /// </summary>
        /// <param name="linesCountDocumentName">Lines count document name.</param>
        public LinesCountWriter(string linesCountDocumentName)
        {
            linesCountDocument = IdeApp.Workbench.GetDocument(linesCountDocumentName);
            if (linesCountDocument == null)
                linesCountDocument = IdeApp.Workbench.NewDocument(linesCountDocumentName, "text/plain", "");
            textEditorData = linesCountDocument.GetContent<ITextEditorDataProvider>().GetTextEditorData();

        }

        /// <summary>
        /// Writes the info of selected item.
        /// </summary>
        /// <param name="selectedItem">Selected item.</param>
        public void WriteInTextDocument(LinesCounter.Result result)
        {
            EmptyDocument();
            WriteHead();
            WriteFileInfos(result.Details);
            WriteFoot(result.Overall);
            BringDocumentToFront();
        }

        private void EmptyDocument()
        {
            textEditorData.Document.Text = "";
        }

        private void WriteHead()
        {
            Write(String.Format("   {0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", "File", "Lines of code", "Source lines", "Effective lines", "Comment lines"));
        }

        private void Write(string info)
        {
            textEditorData.InsertAtCaret(info + "\n");
        }

        private void WriteFileInfos(List<SourceFile> sourceFiles)
        {
            foreach (SourceFile f in sourceFiles)
            {
                WriteFileInfo(f);
            }
        }

        private void WriteFoot(LinesCounter.Result.OverallResult overall)
        {
            Write(String.Format("   {0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", " ", overall.TotalLines, overall.SourceLines, overall.EffectiveLines, overall.CommentLines));
        }

        void BringDocumentToFront()
        {
            linesCountDocument.Select();
        }


        private void WriteFileInfo(SourceFile sourceFile)
        {
            string fittingPath = sourceFile.FilePath.Length > 80 ? sourceFile.FilePath.Substring(0, 79) : sourceFile.FilePath;
            Write(String.Format("   {0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", fittingPath, sourceFile.LinesOfCode, sourceFile.SourceLinesOfCode, sourceFile.EffectiveLinesOfCode, sourceFile.CommentLines));
        }

    }
}

