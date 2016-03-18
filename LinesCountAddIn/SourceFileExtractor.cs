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
        private Solution currentSolution;
        private Project currentProject;

        /// <summary>
        /// Gets the source files.
        /// </summary>
        /// <value>The source files.</value>
        public List<SourceFile> SourceFiles { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinesCountAddIn.SourceFileExtractor"/> class.
        /// </summary>
        /// <param name="solutionItem">Solution item.</param>
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
            currentSolution = solution;
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
            currentProject = project;
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
                string solutionName = currentSolution == null ? "" : currentSolution.Name;
                string projectName = currentProject == null ? "" : currentProject.Name;
                SourceFiles.Add(new SourceFile(solutionName, projectName, projectFile.FilePath.ToString(), lines));
            }
        }

        /// <summary>
        /// Determines if the specified projectFile is a C# file.
        /// </summary>
        /// <returns><c>true</c> if the specified projectFile is a C# file; otherwise, <c>false</c>.</returns>
        /// <param name="projectFile">Project file.</param>
        public static bool IsCSharpFile(ProjectFile projectFile)
        {
            return projectFile.Subtype == Subtype.Code && projectFile.FilePath.ToString().Trim().EndsWith(".cs");
        }
    }
}

