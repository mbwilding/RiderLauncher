using System.Diagnostics;

StartApplication(FindExe());

string FindExe()
{
    string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    string riderPre = @"JetBrains\Toolbox\apps\Rider\ch-0";
    string riderPreDir = Path.Combine(localAppData, riderPre);
    List<string> dirs = Directory.GetDirectories(riderPreDir)
        .Where(x => !x.Contains("plugins"))
        .ToList();
    string? latestVersion = dirs.Select(x=> new Version(Path.GetFileName(x))).Max()?.ToString();
    if (latestVersion is null)
        Environment.Exit(1);
    return Path.Combine(riderPreDir, latestVersion, @"bin\rider64.exe");
}

void StartApplication(string filePath)
{
    Process.Start(new ProcessStartInfo
    {
        FileName = filePath,
        Arguments = args.FirstOrDefault(),
        UseShellExecute = false
    });
}