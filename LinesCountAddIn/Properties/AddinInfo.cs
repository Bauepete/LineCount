using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly:Addin(
    "LinesCountAddIn", 
    Namespace = "LinesCountAddIn",
    Version = "1.0"
)]

[assembly:AddinName("LinesCountAddIn")]
[assembly:AddinCategory("IDE extensions")]
[assembly:AddinDescription("LinesCountAddIn")]
[assembly:AddinAuthor("peter")]

