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

  public static bool Check(List<List<int>> f) {
    for(int i = 0; i < f.Count; i++) {
      for(int j = 0; j < f[i].Count; j++) {
        if(f[i][j] != 0) return false;
      }
    }
    return true;
  }

  // public static int Score(int abs_sum, int cost, int diff) {
  //   return Math.Round(1000000000 * (double)abs_sum / (cost + diff));
  // }

  public static List<string> Tidy(int base_cnt, int n, List<List<int>> e) {
    var res = new List<string>();
    var f = new List<List<int>>();
    for(int i = 0; i < e.Count; i++) {
      f.Add(new List<int>());
      for(int j = 0; j < e[i].Count; j++) {
        f[i].Add(e[i][j]);
      }
    }


    var my = new List<int>(){-1, 0, 1, 0};
    var mx = new List<int>(){0, -1, 0, 1};
    int cy = 0;
    int cx = 0;
    int w = 0;

    int cnt = base_cnt;
    while(!Check(f)) {
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

          if((cnt == 0 && f[eey][eex] < 0) || (cnt >= 1 && f[eey][eex] > 0)) {
            ptn.Add(new List<int>(){eey, eex});
          }
        }
      }

      if(ptn.Count == 0) {
        cnt = 0;
        continue;
      }

      int ey = ptn[0][0];
      int ex = ptn[0][1];
      while(cy > ey) {
        res.Add("U");
        cy--;
      }
      while(cy < ey) {
        res.Add("D");
        cy++;
      }
      while(cx > ex) {
        res.Add("L");
        cx--;
      }
      while(cx < ex) {
        res.Add("R");
        cx++;
      }

      if(cnt >= 1) {
        int x = f[cy][cx];
        res.Add($"+{x}");
        w += x;
        f[cy][cx] = 0;
        cnt--;
      } else {
        int x = Math.Min(-f[cy][cx], w);
        if(x > 0) res.Add($"-{x}");
        w -= x;
        f[cy][cx] += x;

        if(w == 0) {
          cnt = base_cnt;
        }
      }

      cy = ey;
      cx = ex;
    }

    return res;
  }

  public static void Main(string[] args) {
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

    List<string> res = Tidy(10, n, f);
    foreach(string s in res) {
      WriteLine(s);
    }
  }
}
