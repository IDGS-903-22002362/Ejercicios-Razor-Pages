using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

public class BinomioModel : PageModel
{
    [BindProperty] public double A { get; set; }
    [BindProperty] public double B { get; set; }
    [BindProperty] public double X { get; set; }
    [BindProperty] public double Y { get; set; }
    [BindProperty] public int N { get; set; } = 0;

    public double ValorNumerico { get; private set; }
    public string ExpresionHtml { get; private set; } = string.Empty;
    public bool Calculado => N >= 0 && !double.IsNaN(ValorNumerico);

    public void OnGet() { }

    public void OnPost()
    {
        if (N < 0)
        {
            ModelState.AddModelError(nameof(N), "n debe ser un entero no negativo.");
            return;
        }

        ValorNumerico = 0;
        StringBuilder sb = new();

        
        for (int k = 0; k <= N; k++)
        {
            double coefBinom = Comb(N, k);
            double termNum = coefBinom * Math.Pow(A * X, N - k) * Math.Pow(B * Y, k);
            ValorNumerico += termNum;


            double coefSimb = coefBinom * Math.Pow(A, N - k) * Math.Pow(B, k);
            string parte = $"{FormatoCoef(coefSimb)}x<sup>{N - k}</sup>y<sup>{k}</sup>";

            if (k == 0)
                sb.Append(parte);
            else
                sb.Append(" + " + parte);
        }

        ExpresionHtml = sb.ToString()
            .Replace("x<sup>0</sup>", "")
            .Replace("y<sup>0</sup>", "")
            .Replace("<sup>1</sup>", "");
    }


    private static double Comb(int n, int k)           
        => Factorial(n) / (Factorial(k) * Factorial(n - k));

    private static double Factorial(int n)
    {
        double f = 1;
        for (int i = 2; i <= n; i++) f *= i;
        return f;
    }

    private static string FormatoCoef(double c)
    {
        if (Math.Abs(c - 1) < 1e-9) return "";             
        if (Math.Abs(c + 1) < 1e-9) return "-";            
        return c % 1 == 0 ? ((int)c).ToString() : c.ToString("G");
    }
}
