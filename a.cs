using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using static System.Console;

public class MainClass {
  public static void Show(List<List<int>> f) {
    for(int i = 0; i < f.Count; i++) {
      for(int j = 0; j < f[i].Count; j++) {
        Write($"{f[i][j]} ".PadLeft(4));
      }
      WriteLine();
    }
  }

  public static int Main(string[] args) {
    var stopwatch = Stopwatch.StartNew();
    var timeout = TimeSpan.FromSeconds(2.9);

    int n = int.Parse(ReadLine());
    var f = new List<List<int>>();
    for(int i = 0; i < n; i++) {
      f.Add(new List<int>());
      var tmp = ReadLine().Split().Select(int.Parse).ToArray();
      foreach(int x in tmp) {
        f[i].Add(x);
      }
    }

    int cy = 0;
    int cx = 0;
    int w = 0;
    var mx = new List<int>(){1, -1};
    int ix = 0;
    var ans = new List<string>();

    while(cy < n) {
      if(f[cy][cx] > 0) {
        int x = f[cy][cx];
        ans.Add($"+{x}");
        w += x;
        f[cy][cx] = 0;
      } else if(f[cy][cx] < 0) {
        int x = Math.Min(-f[cy][cx], w);
        if(x > 0) ans.Add($"-{x}");
        w -= x;
        f[cy][cx] += x;
      }

      int ey = cy;
      int ex = cx + mx[ix];
      if(0 <= ey && ey < n && 0 <= ex && ex < n) {
        cy = ey;
        cx = ex;
        ans.Add(ix == 0 ? "R" : "L");
      } else {
        cy++;
        if(cy >= n) break;
        ix = (ix + 1) % mx.Count;
        ans.Add("D");
      }
    }

    foreach(string s in ans) {
      WriteLine(s);
    }

    cy = n - 1;
    while(true) {
      if(cy < 0) break;

      if(f[cy][cx] < 0) {
        w -= (-f[cy][cx]);
        WriteLine($"-{(-f[cy][cx])}");
        f[cy][cx] = 0;
      }

      if(ans.Count == 0) break;

      string s = ans.Last();
      ans.RemoveAt(ans.Count - 1);
      if(s.Length >= 2) continue;

      string t = "";
      switch(s) {
        case "U":
          t = "D";
          cy++;
          break;
        case "D":
          t = "U";
          cy--;
          break;
        case "L":
          t = "R";
          cx++;
          break;
        case "R":
          t = "L";
          cx--;
          break;
        default:
          new Exception($"移動方向を表す文字列がおかしい");
          break;
      }
      if(t.Length != 0) WriteLine(t);
    }

    return 0;
  }
}
