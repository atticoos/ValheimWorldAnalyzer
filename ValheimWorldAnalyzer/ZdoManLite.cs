using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ValheimWorldAnalyzer {
  internal class ZdoManLite {
    const int zdoSectorWidth = 512;
    public List<ZDO> zdos = new List<ZDO>();
    public static ZdoManLite instance;

    public ZdoManLite() {
      ZdoManLite.instance = this;
    }
    public void ReadWorld(string worldFilePath) {
      FileStream fileStream;
      try {
        fileStream = File.OpenRead(worldFilePath);
      } catch {
        Logger.Log($"Unable to open DB path: {worldFilePath}");
        return;
      }

      BinaryReader reader = new BinaryReader(fileStream);
      try {
        int version = reader.ReadInt32();
        double netTime;
        if (version >= 4) {
          netTime = reader.ReadDouble();
        }
        reader.ReadInt64();
        uint num = reader.ReadUInt32();
        int totalZdoCount = reader.ReadInt32();
        Logger.Log($"Loading {totalZdoCount} zdos");
        ZPackage zpackage = new ZPackage();
        for (int i = 0; i < totalZdoCount; i++) {
          ZDO zdo = new ZDO();
          zdo.m_uid = new ZDOID(reader);
          int count = reader.ReadInt32();
          byte[] data = reader.ReadBytes(count);
          zpackage.Load(data);
          zdo.Load(zpackage, version);
          zdos.Add(zdo);
          // TODO: Handle dead ZDOs
        }
      } catch {
        Logger.Log($"Failed to parse world file");
        return;
      }
      Logger.Log($"Read {zdos.Count} zdos");
    }

    public List<ZDO> GetAllZDOsWithPrefab(string prefab) {
      int stableHashCode = prefab.GetStableHashCode();
      return zdos.FindAll(zdo => zdo.GetPrefab() == stableHashCode);
    }
  }
}
