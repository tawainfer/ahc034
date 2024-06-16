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
    var sx = new List<int>(){1, -1};
    int ix = 0;
    var ans = new List<string>();

    // while(cy < n) {
    //   if(f[cy][cx] > 0) {
    //     int x = f[cy][cx];
    //     ans.Add($"+{x}");
    //     w += x;
    //     f[cy][cx] = 0;
    //   } else if(f[cy][cx] < 0) {
    //     int x = Math.Min(-f[cy][cx], w);
    //     if(x > 0) ans.Add($"-{x}");
    //     w -= x;
    //     f[cy][cx] += x;
    //   }

    //   int ey = cy;
    //   int ex = cx + sx[ix];
    //   if(0 <= ey && ey < n && 0 <= ex && ex < n) {
    //     cy = ey;
    //     cx = ex;
    //     ans.Add(ix == 0 ? "R" : "L");
    //   } else {
    //     cy++;
    //     if(cy >= n) break;
    //     ix = (ix + 1) % sx.Count;
    //     ans.Add("D");
    //   }
    // }
    // cy = n - 1;

    // foreach(string s in ans) {
    //   WriteLine(s);
    // }

    var my = new List<int>(){-1, 0, 1, 0};
    var mx = new List<int>(){0, -1, 0, 1};

    while(true) {
      var seen = new List<List<bool>>();
      for(int i = 0; i < n; i++) {
        seen.Add(new List<bool>());
        for(int j = 0; j < n; j++) {
          seen[i].Add(false);
        }
      }
      seen[cy][cx] = true;

      var q = new Queue<List<int>>();
      q.Enqueue(new List<int>(){cy, cx});
      var ptn = new List<List<int>>();

      while(q.Count >= 1) {
        int ccy = q.Peek()[0];
        int ccx = q.Peek()[1];
        q.Dequeue();

        for(int i = 0; i < 4; i++) {
          int eey = ccy + my[i];
          int eex = ccx + mx[i];
          if(!(0 <= eey && eey < n && 0 <= eex && eex < n)) continue;
          if(seen[eey][eex]) continue;
          seen[eey][eex] = true;
          q.Enqueue(new List<int>(){eey, eex});

          if(f[eey][eex] > 0) {
            ptn.Add(new List<int>(){eey, eex});
          }
        }
      }

      if(ptn.Count == 0) break;

      int ey = ptn[0][0];
      int ex = ptn[0][1];
      while(cy > ey) {
        WriteLine("U");
        cy--;
      }
      while(cy < ey) {
        WriteLine("D");
        cy++;
      }
      while(cx > ex) {
        WriteLine("L");
        cx--;
      }
      while(cx < ex) {
        WriteLine("R");
        cx++;
      }
      while() {

      }

      w += (f[ey][ex]);
      WriteLine($"+{f[ey][ex]}");
      f[ey][ex] = 0;

      cy = ey;
      cx = ex;
    }

    while(true) {
      var seen = new List<List<bool>>();
      for(int i = 0; i < n; i++) {
        seen.Add(new List<bool>());
        for(int j = 0; j < n; j++) {
          seen[i].Add(false);
        }
      }
      seen[cy][cx] = true;

      var q = new Queue<List<int>>();
      q.Enqueue(new List<int>(){cy, cx});
      var ptn = new List<List<int>>();

      while(q.Count >= 1) {
        int ccy = q.Peek()[0];
        int ccx = q.Peek()[1];
        q.Dequeue();

        for(int i = 0; i < 4; i++) {
          int eey = ccy + my[i];
          int eex = ccx + mx[i];
          if(!(0 <= eey && eey < n && 0 <= eex && eex < n)) continue;
          if(seen[eey][eex]) continue;
          seen[eey][eex] = true;
          q.Enqueue(new List<int>(){eey, eex});

          if(f[eey][eex] < 0) {
            ptn.Add(new List<int>(){eey, eex});
          }
        }
      }

      if(ptn.Count == 0) break;

      int ey = ptn[0][0];
      int ex = ptn[0][1];
      while(cy > ey) {
        WriteLine("U");
        cy--;
      }
      while(cy < ey) {
        WriteLine("D");
        cy++;
      }
      while(cx > ex) {
        WriteLine("L");
        cx--;
      }
      while(cx < ex) {
        WriteLine("R");
        cx++;
      }

      w -= (-f[ey][ex]);
      WriteLine($"-{(-f[ey][ex])}");
      f[ey][ex] = 0;

      cy = ey;
      cx = ex;
    }

    // cy = n - 1;
    // while(true) {
    //   if(cy < 0) break;

    //   if(f[cy][cx] < 0) {
    //     w -= (-f[cy][cx]);
    //     WriteLine($"-{(-f[cy][cx])}");
    //     f[cy][cx] = 0;
    //   }

    //   if(ans.Count == 0) break;

    //   string s = ans.Last();
    //   ans.RemoveAt(ans.Count - 1);
    //   if(s.Length >= 2) continue;

    //   string t = "";
    //   switch(s) {
    //     case "U":
    //       t = "D";
    //       cy++;
    //       break;
    //     case "D":
    //       t = "U";
    //       cy--;
    //       break;
    //     case "L":
    //       t = "R";
    //       cx++;
    //       break;
    //     case "R":
    //       t = "L";
    //       cx--;
    //       break;
    //     default:
    //       new Exception($"移動方向を表す文字列がおかしい");
    //       break;
    //   }
    //   if(t.Length != 0) WriteLine(t);
    // }

    return 0;
  }
}
