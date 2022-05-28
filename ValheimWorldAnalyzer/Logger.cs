using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimWorldAnalyzer {
  internal class Logger {
    string tag;
    public static void Log(string message) {
      System.Console.WriteLine($"[ValheimWorldAnalyzer] {message}");
    }
  }
}
