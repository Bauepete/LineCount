using LinesCount;
using Mono.TextEditor;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using System;
using System.IO;

namespace LinesCountAddIn
{
    public class LinesCountWriter
    {
        private TextEditorData textEditorData;
        private Document linesCountDocument;

        int linesOfCode, sourceLines, effectiveLines, commentLines;

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

            linesOfCode = sourceLines = effectiveLines = commentLines = 0;
        }

        /// <summary>
        /// Writes the info of selected item.
        /// </summary>
        /// <param name="selectedItem">Selected item.</param>
        public void WriteInfoOfSelectedItem(object selectedItem)
        {
            textEditorData.Document.Text = "";
            Write(String.Format("   {0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", "File", "Lines of code", "Source lines", "Effective lines", "Comment lines"));

            Solution selectedSolution = selectedItem as Solution;
            if (selectedSolution != null)
                ShowSolution(selectedSolution);
            
            Project selectedProject = selectedItem as Project;
            if (selectedProject != null)
                ShowProject(selectedProject);
            
            ProjectFile selectedFile = selectedItem as ProjectFile;
            if (selectedFile != null)
                ShowProjectFile(selectedFile);
            Write(String.Format("   {0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", " ", linesOfCode, sourceLines, effectiveLines, commentLines));
            linesCountDocument.Select();
        }

        private void ShowSolution(Solution solution)
        {
            foreach (SolutionItem si in solution.Items)
            {
                Project project = si as Project;
                if (project != null)
                    ShowProject(project);
                else
                    Write(si.Name);
            }
        }

        private void Write(string info)
        {
            textEditorData.InsertAtCaret(info + "\n");
        }

        private void ShowProject(Project project)
        {
            Write("In project " + project.Name);
            foreach (ProjectItem projectItem in project.Items)
            {
                ProjectFile projectFile = projectItem as ProjectFile;
                ShowProjectFile(projectFile);
            }
        }

        private void ShowProjectFile(ProjectFile projectFile)
        {
            if (projectFile != null)
            {
                if (IsCSharpFile(projectFile))
                    WriteFileInfo(projectFile.FilePath);
            }
        }

        public static bool IsCSharpFile(ProjectFile projectFile)
        {
            return projectFile.Subtype == Subtype.Code && projectFile.FilePath.ToString().Trim().EndsWith(".cs");
        }

        private void WriteFileInfo(FilePath filePath)
        {
            string[] lines = File.ReadAllLines(filePath.ToString());
            SourceFile s = new SourceFile(filePath.ToString(), lines, new CSharpSourceLineAnalyzer());
            string fittingPath = filePath.ToString().Length > 80 ? filePath.ToString().Substring(0, 79) : filePath.ToString();

            linesOfCode += s.LinesOfCode;
            sourceLines += s.SourceLinesOfCode;
            effectiveLines += s.EffectiveLinesOfCode;
            commentLines += s.CommentLines;

            string info = String.Format("{0, -80} {1, 15} {2, 15} {3, 15} {4, 15}", fittingPath, s.LinesOfCode, s.SourceLinesOfCode, s.EffectiveLinesOfCode, s.CommentLines);
            Write("   " + info);
        }

    }
}

