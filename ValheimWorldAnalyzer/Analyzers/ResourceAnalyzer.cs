using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimWorldAnalyzer.Analyzers {
  internal class ResourceAnalyzer : Analyzer {
    public ResourceAnalyzer(ZdoManLite zdoMan) : base(zdoMan) {
    }

    public override void Run() {
      List<ZDO> copper = zdoMan.GetAllZDOsWithPrefab("rock4_copper");
      List<ZDO> silver = zdoMan.GetAllZDOsWithPrefab("silvervein")
        .Concat(zdoMan.GetAllZDOsWithPrefab("rock3_silver"))
        .ToList();
      List<ZDO> tin = zdoMan.GetAllZDOsWithPrefab("MineRock_Tin");
      List<ZDO> obsidian = zdoMan.GetAllZDOsWithPrefab("MineRock_Obsidian");

      Logger.Log($"Found {copper.Count} copper");
      Logger.Log($"Found {silver.Count} silver");
      Logger.Log($"Found {tin.Count} tin");
      Logger.Log($"Found {obsidian.Count} obsidian");
    }
  }
}
