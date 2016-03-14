using LinesCount;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System;
using System.Collections.Generic;

namespace LinesCountAddIn
{
    public class CountLinesHandler : CommandHandler
    {
        private const string LINES_COUNT_DOCUMENT_NAME = "Lines Count Statistics";

        protected override void Update(CommandInfo info)
        {
            object selectedItem = GetSelectedItem();

            if (selectedItem == null)
            {
                info.Enabled = false;
                return;
            }

            if (selectedItem is Project || selectedItem is Solution)
                info.Enabled = true;
            else
            {
                ProjectFile selectedFile = selectedItem as ProjectFile;
                info.Enabled = selectedFile != null && SourceFileExtractor.IsCSharpFile(selectedFile);
            }
        }

        private static object GetSelectedItem()
        {
            return IdeApp.ProjectOperations.CurrentSelectedItem;
        }

        protected override void Run()
        {
            SourceFileExtractor sourceFileExtractor = new SourceFileExtractor(GetSelectedItem());
            LinesCounter linesCounter = new LinesCounter(new CSharpSourceLineAnalyzer());
            linesCounter.Count(sourceFileExtractor.SourceFiles);
            LinesCountWriter w = new LinesCountWriter(LINES_COUNT_DOCUMENT_NAME);
            w.WriteInTextDocument(linesCounter.Results);
//            sfa.SourceFiles;
//            if (selectedItem != null)
//                w.WriteInfoOfSelectedItem(selectedItem);
        }
    }
}

