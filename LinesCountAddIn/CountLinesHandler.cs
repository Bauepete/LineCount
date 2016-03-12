using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System;

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
                info.Enabled = selectedFile != null && LinesCountWriter.IsCSharpFile(selectedFile);
            }
        }

        private static object GetSelectedItem()
        {
            return IdeApp.ProjectOperations.CurrentSelectedItem;
        }

        protected override void Run()
        {
            LinesCountWriter w = new LinesCountWriter(LINES_COUNT_DOCUMENT_NAME);
            object selectedItem = GetSelectedItem();
            if (selectedItem != null)
                w.WriteInfoOfSelectedItem(selectedItem);
        }
    }
}

