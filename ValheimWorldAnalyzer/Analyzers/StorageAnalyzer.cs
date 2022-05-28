using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimWorldAnalyzer.Analyzers {
  internal class StorageAnalyzer : Analyzer {
    public StorageAnalyzer(ZdoManLite zdoMan) : base(zdoMan) {
    }

    public override void Run() {
      List<ZDO> storageZdos = zdoMan.GetAllZDOsWithPrefab("piece_chest_wood")
        .Concat(zdoMan.GetAllZDOsWithPrefab("piece_chest"))
        .Concat(zdoMan.GetAllZDOsWithPrefab("piece_chest_blackmetal"))
        .Concat(zdoMan.GetAllZDOsWithPrefab("piece_chest_private"))
        .Concat(zdoMan.GetAllZDOsWithPrefab("Player_tombstone"))
        .ToList();

      int count = 0;
      foreach (ZDO zdo in storageZdos) {
        string @string = zdo.GetString("items", "");
        if (string.IsNullOrEmpty(@string)) {
          continue;
        }

        ZPackage pkg = new ZPackage(@string);
        int num = pkg.ReadInt();
        int num2 = pkg.ReadInt();
        for (int i = 0; i < num2; i++) {
          string text = pkg.ReadString();
          int stack = pkg.ReadInt();
          float durability = pkg.ReadSingle();
          Vector2i pos = pkg.ReadVector2i();
          bool equipped = pkg.ReadBool();
          int quality = 1;
          if (num >= 101) {
            quality = pkg.ReadInt();
          }
          int variant = 0;
          if (num >= 102) {
            variant = pkg.ReadInt();
          }
          long crafterId = 0L;
          string crafterName = "";
          if (num >= 103) {
            crafterId = pkg.ReadLong();
            crafterName = pkg.ReadString();
          }

          if (isContraband(text, crafterId, quality)) {
            count++;
          }
        }
      }

      Logger.Log($"Found {count} suspicious items");
    }


    static List<string> emptyCrafterTagItems = new List<string>() {
      "ArrowNeedle",
      "ArrowFrost",
      "ArrowWood",
      "BloodPudding",
      "SerpentStew",
      "FishWraps",
      "Eyescream",
      "Sausages",
      "TurnipStew",
      "OnionSoup",
      "ShocklateSmoothie"
    };

    bool isContraband(string prefabName, long crafterId, int quality) {
      if (string.IsNullOrEmpty(prefabName)) {
        return false;
      }

      if (prefabName == "Cultivator" && crafterId == 2340194039L) {
        return true;
      }

      if (prefabName == "FishingRod" && crafterId == 522048064L) {
        return true;
      }

      if (emptyCrafterTagItems.Contains(prefabName) && crafterId == 0L) {
        return true;
      }

      if (prefabName.Contains("SwordIronFire") || prefabName.Contains("Cheat")) {
        return true;
      }

      if ((prefabName.Contains("Armor")
        || prefabName.StartsWith("Sword")
        || prefabName.StartsWith("Knife")
        || prefabName.Contains("Axe")
        || prefabName.StartsWith("Bow")
        || prefabName.StartsWith("Spear")
        || prefabName.StartsWith("Atgeir")
        || prefabName.StartsWith("Mace")
        || prefabName.StartsWith("Club")
        || prefabName.Contains("FistFenrirClaw")
        || prefabName.StartsWith("Helmet")
        ) && (quality > 18 || crafterId == 0L)) {
        return true;
      }

      return false;
    }
  }
}
