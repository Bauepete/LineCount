using LinesCount;
using MonoDevelop.Core;
using MonoDevelop.Projects;
using System;
using System.Collections.Generic;
using System.IO;

namespace LinesCountAddIn
{
    public class SourceFileExtractor
    {
        /// <summary>
        /// Gets the source files.
        /// </summary>
        /// <value>The source files.</value>
        public List<SourceFile> SourceFiles { get; private set; }

        public SourceFileExtractor(object solutionItem)
        {
            if (solutionItem == null)
                throw new ArgumentNullException();

            SourceFiles = new List<SourceFile>();
            ExtractFromSolutionItem(solutionItem);
        }

        private void ExtractFromSolutionItem(object solutionItem)
        {
            Solution selectedSolution = solutionItem as Solution;
            if (selectedSolution != null)
                ExtractFromSolution(selectedSolution);
            Project selectedProject = solutionItem as Project;
            if (selectedProject != null)
                ExtractFromProject(selectedProject);
            ProjectFile selectedFile = solutionItem as ProjectFile;
            if (selectedFile != null)
                ExtractProjectFile(selectedFile);
        }

        private void ExtractFromSolution(Solution solution)
        {
            foreach (SolutionItem si in solution.Items)
            {
                Project project = si as Project;
                if (project != null)
                    ExtractFromProject(project);
                else
                    throw new ArgumentNullException();
            }
        }

        private void ExtractFromProject(Project project)
        {
            foreach (ProjectItem projectItem in project.Items)
            {
                ProjectFile projectFile = projectItem as ProjectFile;
                ExtractProjectFile(projectFile);
            }
        }

        private void ExtractProjectFile(ProjectFile projectFile)
        {
            if (projectFile != null && IsCSharpFile(projectFile))
            {
                string[] lines = File.ReadAllLines(projectFile.FilePath);
                SourceFiles.Add(new SourceFile(projectFile.FilePath.ToString(), lines));
            }
        }

        public static bool IsCSharpFile(ProjectFile projectFile)
        {
            return projectFile.Subtype == Subtype.Code && projectFile.FilePath.ToString().Trim().EndsWith(".cs");
        }
    }
}

