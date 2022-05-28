using ValheimWorldAnalyzer;
using ValheimWorldAnalyzer.Analyzers;

string worldFileLocation = args[0] ?? "C:/Users/Atticus/Downloads/ComfyEra6.db";

Logger.Log("Reading world file");
DateTimeOffset start = DateTimeOffset.Now;
ZdoManLite zdoMan = new ZdoManLite();
zdoMan.ReadWorld(worldFileLocation);
DateTimeOffset end = DateTimeOffset.Now;
double readWorldFileTime = (end - start).TotalSeconds;

ResourceAnalyzer resourceAnalyzer = new ResourceAnalyzer(zdoMan);
StorageAnalyzer storageAnalyzer = new StorageAnalyzer(zdoMan);
resourceAnalyzer.Run();
storageAnalyzer.Run();

double analyzeTime = (DateTimeOffset.Now - end).TotalSeconds;

Logger.Log($"{readWorldFileTime} seconds to parse world file");
Logger.Log($"{analyzeTime} seconds to analyze ZDOs");