# Lines Count

A Xamarin Studio add-in to count source lines in C# sources. Currently it distinguishes between

- Lines of code (all lines)
- Source lines of code (all lines except empty lines and comment lines)
- Effective lines of code (all source lines except lines only having opening and closing braces)
- Comment lines (line comments and block comments)

# Usage
The add-in can be triggered by selecting the menu item *Count Lines* in the context menu or by choosing the same menu item in the menu *Edit*. The add-in behaves as given below, when the following items are selected in the solution pane:

- When a C# source file is selected it counts the lines in the selected file.
- When a project is selected it counts the lines in all C# sources in the project.
- When a solution is selected it counts the lines in all C# sources in the solution.

The result is pasted in a new text document opened in the IDE. If a solution or a project is selected a sum of the different lines counts is additionally shown.

# How to Build
Open the solution in Xamarin Studio and build it. After this the build result has to be packed by using `mdtool`:

    mdtool setup pack LinesCountAddIn.dll
    
Care must be taken that `mdtool` is generally not in the path environment variable so the explicit path has to be given. On MacOS it is found under `/Applications/Xamarin\ Studio.app/Contents/MacOS/`.

# Tests
The library is unit tested. Development was done under MacOS. No tests were done under Linux or Windows.

# Helpful Ressources
... to figure out the MonodDvelop project structure:

http://www.monodevelop.com/developers/articles/how-to-extend-the-project-model/
https://sourcecodebrowser.com/monodevelop/2.4plus-pdfsg/annotated.html
https://sourcecodebrowser.com/monodevelop/2.4plus-pdfsg/class_mono_develop_1_1_projects_1_1_project_item.html
http://www.monodevelop.com/developers/articles/the-command-system/